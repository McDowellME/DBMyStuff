using System;
using System.Collections.Generic;

namespace PersonalPropertyApp.Models
{
    public partial class PolicyHolder
    {
        public PolicyHolder()
        {
            ClaimReport = new HashSet<ClaimReport>();
            PolicyDetails = new HashSet<PolicyDetails>();
            PolicyItems = new HashSet<PolicyItems>();
        }

        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ClaimReport> ClaimReport { get; set; }
        public virtual ICollection<PolicyDetails> PolicyDetails { get; set; }
        public virtual ICollection<PolicyItems> PolicyItems { get; set; }
    }
}
