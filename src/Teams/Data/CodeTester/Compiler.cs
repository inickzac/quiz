using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Teams.Data.CodeTester
{
    public class Compiler
    {
        private readonly IEnumerable<PortableExecutableReference> allTrustedPlatformAssemblies;
        private string codeToCompile;
        public string DirectoryPathToCompile { get; set; } = "TempCompileFiles";
        private string fileFullPath;
        public Compiler()
        {
            var trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES"))
                .Split(Path.PathSeparator);
            allTrustedPlatformAssemblies = trustedAssembliesPaths
               .Select(p => MetadataReference.CreateFromFile(p))
               .ToList();
        }
        public Task<CompileResult> СompileAsync(string codeToCompile, IEnumerable<PortableExecutableReference> references)
        {
            this.codeToCompile = codeToCompile;
            return Task.Run(() => СompilationCsharp(references));
        }
        public Task<CompileResult> СompileUsingStandartReferencesAsync(string codeToCompile)
        {
            this.codeToCompile = codeToCompile;
            return Task.Run(() => СompilationCsharp(allTrustedPlatformAssemblies));
        }
        private CompileResult СompilationCsharp(IEnumerable<PortableExecutableReference> references)
        {
            string fileName = Path.GetRandomFileName().Split(".").First() + ".exe";
            fileFullPath = Path.Combine(DirectoryPathToCompile, fileName);
            if (!Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(DirectoryPathToCompile);
            }
            var compilation = CSharpCompilation.Create(
              fileName,
              new[] { CSharpSyntaxTree.ParseText(codeToCompile) },
              references,
              new CSharpCompilationOptions(OutputKind.ConsoleApplication));
            EmitResult result = compilation.Emit(fileFullPath);
            return GenerateCompilResult(result);
        }
        private CompileResult GenerateCompilResult(EmitResult result)
        {
            if (result.Success)
            {
                var configFileFullPath = Path.ChangeExtension(fileFullPath, "runtimeconfig.json");
                File.WriteAllText(configFileFullPath, GenerateRuntimeConfig());
                return new CompileResult(result.Diagnostics.ToList(), fileFullPath, true, configFileFullPath);
            }
            else
            {
                return new CompileResult(result.Diagnostics.ToList(), null, false, null);
            }
        }
        private string GenerateRuntimeConfig()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(
                    stream,
                    new JsonWriterOptions() { Indented = true }
                ))
                {
                    writer.WriteStartObject();
                    writer.WriteStartObject("runtimeOptions");
                    writer.WriteStartObject("framework");
                    writer.WriteString("name", "Microsoft.NETCore.App");
                    writer.WriteString(
                        "version",
                        RuntimeInformation.FrameworkDescription.Replace(".NET Core ", "")
                    );
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
        public class CompileResult : IDisposable
        {
            private readonly IEnumerable<Diagnostic> diagnostics;
            private string path;
            private string configFilePath;
            private bool success;
            public CompileResult(IEnumerable<Diagnostic> diagnostics, string path, bool success, string configPath)
            {
                this.diagnostics = diagnostics;
                this.path = path;
                this.success = success;
                this.configFilePath = configPath;
            }
            public IEnumerable<Diagnostic> Diagnostics { get => diagnostics; }
            public string Path { get => path; }
            public bool Success { get => success; }
            public void Dispose()
            {
                DeleteFile(path);
                path = null;
                DeleteFile(configFilePath);
                configFilePath = null;
                success = false;
            }
            private void DeleteFile(string path)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
    }
}
