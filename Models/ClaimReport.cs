using System;
using System.Collections.Generic;

namespace PersonalPropertyApp.Models
{
    public partial class ClaimReport
    {
        public ClaimReport()
        {
            ClaimItems = new HashSet<ClaimItems>();
        }

        public int Reportid { get; set; }
        public string Claimreason { get; set; }
        public DateTime Reportdate { get; set; }
        public int Policyid { get; set; }
        public int Userid { get; set; }

        public virtual PolicyDetails Policy { get; set; }
        public virtual PolicyHolder User { get; set; }
        public virtual ICollection<ClaimItems> ClaimItems { get; set; }
    }
}
