using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Teams.Data.CodeTester;
using Xunit;

namespace Teams.Tests.CodeTesterTests
{
    public class CodeTesterTests
    {
        [Fact]
        public void CompilTest()
        {
            string code = @"
                using System;

                namespace TestNS
                {
                    public class Test
                    {
                        public static void Main(string[] args)
                        {                           
                        }
                   
                        public int Calc(int firstOperand )
                        {
                            return firstOperand+2;
                        }
                    }
                }";

            var compiler = new Compiler();
            var compileResult = compiler.СompileUsingStandartReferencesAsync(code).Result;
            if (compileResult.Success)
            {
                var type = Assembly.LoadFrom(compileResult.Path)
                    .GetType("TestNS.Test");
                var compileInstace = Activator.CreateInstance(type);
                var res = type.GetMethod("Calc").Invoke(compileInstace, new[] { (object)3 });
                Assert.Equal(5, res);
            }
            else throw new ArgumentException(string.Join(Environment.NewLine, compileResult.Diagnostics));
        }
        [Fact]
        public void FileStartsTest()
        {
            var testString = "this is test text";
            string code = @$"
                using System;

                namespace RoslynCompileSample
                {{
                    public class Writer
                    {{
                        static void Main(string[] args)
                        {{                              
                              Console.WriteLine(""{testString}"");
                        }}
                    }}
                }}";
            Compiler Compiler = new Compiler();
            var compileResult = Compiler.СompileUsingStandartReferencesAsync(code).Result;
            if (compileResult.Success)
            {
                StringBuilder outputData = new StringBuilder();
                using (var process = new Process())
                {
                    compileResult = Compiler.СompileUsingStandartReferencesAsync(code).Result;
                    process.OutputDataReceived += (sender, data) => outputData.Append(data.Data);
                    process.StartInfo.FileName = "dotnet";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.Arguments = compileResult.Path;
                    if (process.Start())
                    {
                        process.BeginOutputReadLine();
                        process.WaitForExit();
                        compileResult.Dispose();
                    }
                }
                Assert.Equal(testString, outputData.ToString());
            }
            else throw new ArgumentException(string.Join(Environment.NewLine, compileResult.Diagnostics));
        }
        [Fact]
        public void RuntimeLimiterTest()
        {
            var codeWorker = new CodeTester();
            var code = @"using System;
                using System.Threading;

                namespace testedcode
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Thread.Sleep(50);
                        }
                    }
                }";
            var test = new Test("", "", new TimeSpan(0, 0, 0, 0, 25));
            var res = codeWorker.RunTestsAsync(new List<Test> { test }, code).Result;
            if (res.First().Value.Success) throw new ArgumentException("the code must be timed out");
            //test = new Test("", "", new TimeSpan(0, 0, 0, 0, 500));
            //res = codeWorker.RunTestsAsync(new List<Test> { test }, code).Result;
            //if (!res.First().Value.Success) throw new ArgumentException("the code must have time to execute");
        }
        [Fact]
        public void StdInStdOutStdErrorTest()
        {
            string code = @"
                using System;

                namespace Test
                {
                    public class Writer
                    {
                        static void Main(string[] args)
                        {                            
                              var str= Console.ReadLine();
                              Console.WriteLine(str);
                        }
                    }
                }";
            var tests = new List<Test> { new Test("hello", "hello", new TimeSpan(0, 0, 0, 10, 0)) };
            var cw = new CodeTester();
            var res = cw.RunTestsAsync(tests, code).Result;
            if (!res.First().Value.Success)
            {
                throw new ArgumentException("std in or std out error");
            }
            tests = new List<Test> { new Test("hellU", "hello", new TimeSpan(0, 0, 0, 10, 0)) };
            res = cw.RunTestsAsync(tests, code).Result;
            if (res.First().Value.Success)
            {
                throw new ArgumentException("std in or std out error");
            }

            string Errorcode = @"
                                   using System;

                    namespace testedcode
                    {
                        class Program
                        {
                            static void Main(string[] args)
                            {
                                throw new Exception(""Test exception"");
                            }
                        }
                    }
                    ";
            tests = new List<Test> { new Test("", "", new TimeSpan(0, 0, 0, 10, 0)) };
            res = cw.RunTestsAsync(tests, Errorcode).Result;
            if (res.First().Value.Success && !res.First().Value.RuntimeErrors.Contains("Test exception"))
            {
                throw new ArgumentException("std err error");
            }
        }
        [Fact]
        public void MultithreadingTest()
        {
            var code = @"using System;
                        using System.Threading;

                        namespace testedcode
                        {
                            class Program
                            {
                                static void Main(string[] args)
                                {
                                    long result = 1;
                                    var x= int.Parse(Console.ReadLine());

                                    for (int i = 1; i <= x; i++)
                                    {
                                        result *= i;
                                    }
                                    Console.WriteLine(result);
                                }
                            }
                        }";
            var tests = new List<Test>();
            for (int i = 0; i < 30; i++)
            {
                var p = i % 20;
                tests.Add(new Test(p.ToString(), Factorial(p).ToString(), new TimeSpan(0, 0, 10)));
            }
            var codeTester = new CodeTester();
            var res = codeTester.RunTestsAsync(tests, code).Result;

            if (!res.Any(t => t.Value.Output != Factorial(int.Parse(t.Key.IncomingData)).ToString() + Environment.NewLine))
            {
                throw new ArgumentException("different execution result in one or multiple threads");
            }

             int Factorial(int x)
            {
                int result = 1;

                for (int i = 1; i <= x; i++)
                {
                    result *= i;
                }
                return result;
            }
        }
    }
}
