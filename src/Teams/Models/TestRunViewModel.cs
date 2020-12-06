using System.Collections.Generic;
using Teams.Domain;
using System;


namespace Teams.Models
{
    public class TestRunViewModel
    {
        public List<Guid> TakenTestsIds {get; set;}
        public ApplicationUser ApplicationUser {get; set;}
        public List<Test> Tests {get; set;}
    }
}