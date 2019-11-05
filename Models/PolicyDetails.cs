using System;
using System.Collections.Generic;

namespace PersonalPropertyApp.Models
{
    public partial class PolicyDetails
    {
        public PolicyDetails()
        {
            ClaimReport = new HashSet<ClaimReport>();
            PolicyItems = new HashSet<PolicyItems>();
        }

        public int Policyid { get; set; }
        public int Userid { get; set; }
        public string Policynumber { get; set; }
        public int? Inscompanyid { get; set; }
        public string Inscompanyname { get; set; }
        public string Inscontactname { get; set; }
        public string Inscompanywebsite { get; set; }
        public string Inscontactphone { get; set; }
        public string Inscontactemail { get; set; }
        public string Policynickname { get; set; }
        public string Policytype { get; set; }

        public virtual PolicyHolder User { get; set; }
        public virtual ICollection<ClaimReport> ClaimReport { get; set; }
        public virtual ICollection<PolicyItems> PolicyItems { get; set; }
    }
}
