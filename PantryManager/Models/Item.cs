using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PantryManager.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string UnitType { get; set; }
        public int UnitQty { get; set; }
        public int PantryQty { get; set; }
        public int ListQty { get; set; }

        //This was the hardest thing to find out how to do on the internet....
        public virtual ApplicationUser User { get; set; }
    }
}