using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Teams.Domain;

namespace Teams.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<TestRun> TakenTests { get; set; }
    }
}