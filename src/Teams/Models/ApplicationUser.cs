using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Teams.Domain;

namespace Teams.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid TestsTakenId { get; set; }
        public IEnumerable<TestRun> TestsTaken { get; set; }
    }
}