using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Teams.Domain;

namespace Teams.Models
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public Guid TestsTakenId { get; set; }
        public IEnumerable<TestRun> TestsTaken { get; set; }
    }
}