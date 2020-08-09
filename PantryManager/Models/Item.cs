using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PantryManager.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }
        [Display(Name = "Unit Type")]
        public string UnitType { get; set; }
        [Display(Name = "Unit Size")]
        public int UnitQty { get; set; }
        [Display(Name = "Category")]
        [DefaultValue("Other")]
        public Category ItemCategory { get; set; }
        [Display(Name = "Pantry Quantity")]
        [Range(0, 999, ErrorMessage = "Value for pantry quantity can't be negative")]
        [DefaultValue(0)]
        public int PantryQty { get; set; }
        [Display(Name = "Shopping List Quantity")]
        [Range(0, 999, ErrorMessage = "Value for list quantity can't be negative")]
        [DefaultValue(0)]
        public int ListQty { get; set; }
        [DefaultValue(false)]
        [Display(Name ="In Cart?")]
        public bool InCart { get; set; }


        //This was the hardest thing to find out how to do on the internet....
        public virtual ApplicationUser User { get; set; }
    }
    public enum Category  //List of item categories
    {
        [Display(Name = "Beverages")]
        Beverages,
        [Display(Name = "Bakery")]
        Bakery,
        [Display(Name = "Canned/Jar")]
        Canned_Jar,
        [Display(Name = "Dairy")]
        Dairy,
        [Display(Name = "Dry/Baking")]
        Dry_Baking,
        [Display(Name = "Frozen Foods")]
        FrozenFoods,
        [Display(Name = "Meat")]
        Meat,
        [Display(Name = "Produce")]
        Produce,
        [Display(Name = "Cleaning")]
        Cleaning,
        [Display(Name = "Paper Goods")]
        PaperGoods,
        [Display(Name = "Personal Care")]
        PersonalCare,
        [Display(Name = "Other")]
        Other
    }

    public enum Units
    {

    }
}