using System;
using System.Collections.Generic;

namespace PersonalPropertyApp.Models
{
    public partial class PolicyItems
    {
        public PolicyItems()
        {
            ClaimItems = new HashSet<ClaimItems>();
        }

        public int Itemid { get; set; }
        public string Itemname { get; set; }
        public string Itemcategory { get; set; }
        public string Itemdescription { get; set; }
        public decimal Purchaseprice { get; set; }
        public DateTime? Purchasedate { get; set; }
        public byte[] Itemimage { get; set; }
        public byte[] Receiptimage { get; set; }
        public string Upc { get; set; }
        public int Userid { get; set; }
        public int Policyid { get; set; }

        public virtual PolicyDetails Policy { get; set; }
        public virtual PolicyHolder User { get; set; }
        public virtual ICollection<ClaimItems> ClaimItems { get; set; }
    }
}
