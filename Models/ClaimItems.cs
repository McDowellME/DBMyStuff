using System;
using System.Collections.Generic;

namespace PersonalPropertyApp.Models
{
    public partial class ClaimItems
    {
        public int Claimitemid { get; set; }
        public int Reportid { get; set; }
        public int Itemid { get; set; }

        public virtual PolicyItems Item { get; set; }
        public virtual ClaimReport Report { get; set; }
    }
}
