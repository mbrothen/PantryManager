using Microsoft.AspNet.Identity;
using PantryManager.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PantryManager.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PantryList()
        {
            return View();
        }
        public ActionResult ShoppingList()
        {
            return View();
        }
        private IEnumerable<Item> GetUserItems()
        {
            //Retreive only items for the logged in user
            //Get current user
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);
            return db.Items.ToList().Where(x => x.User == currentUser);  //Only return items where the user matches logged in user

        }
        //Build Ajax table of items
        public ActionResult BuildItemTable()
        {

            return PartialView("_ItemTable", GetUserItems());
        }

        public ActionResult BuildPantryTable()
        {
            return PartialView("_PantryTable", GetUserItems());
        }
        public ActionResult BuildShoppingTable()
        {
            return PartialView("_ShoppingTable", GetUserItems());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            //Only allow user to edit their own items

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ItemName,ItemDescription,UnitType,UnitQty,ItemCategory,PantryQty,ListQty,InCart")] Item item)
        {
            if (ModelState.IsValid)
            {
                //Add user object to the item
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                item.User = currentUser;
                //Finish adding user to item object

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        //Add an item from the table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "Id,ItemName,ItemDescription,UnitType,UnitQty,ItemCategory,PantryQty,ListQty,InCart")] Item item)
        {
            if (ModelState.IsValid)
            {
                //Add user object to the item
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                item.User = currentUser;
                //Finish adding user to item object
                item.InCart = false;
                db.Items.Add(item);
                db.SaveChanges();
                return PartialView("_ItemTable", GetUserItems());

            }

            return View(item);
        }

        //Move quantites from shopping list to the pantry list and uncheck inCart for the checkout process
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Checkout([Bind(Include = "Id,ItemName,ItemDescription,UnitType,UnitQty,ItemCategory,PantryQty,ListQty,InCart")] Item item)
        {
            //Get Current User ID
            string currentUser = User.Identity.GetUserId();

            //Checkout Query String
            string updateQuery = "UPDATE Items " +
                "SET PantryQty = PantryQty + ListQty," +
                "ListQty = 0," +
                "InCart = 0" +
                "WHERE InCart = 1 AND User_Id = {0};";

            int rows = db.Database.ExecuteSqlCommand(updateQuery, currentUser);
            return PartialView("_ItemTable", GetUserItems());
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemName,ItemDescription,UnitType,UnitQty,ItemCategory,PantryQty,ListQty,InCart")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        //Update inCart.  Currently only affects the checkbox
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AJAXEdit(int? id, bool value)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Verify that the item belogs to the user
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                if (item.User == currentUser)
                {
                    //Update the inCart value
                    item.InCart = value;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //return bad request if user is not the user of the item ID
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

            }
            return PartialView("_ShoppingTable", GetUserItems());
        }

        //Change pantry qty by passed value
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePantryList(int? id, int value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Verify that the item belogs to the user
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                if (item.User == currentUser)
                {
                    item.PantryQty = (item.PantryQty + value);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //return bad request if user is not the user of the item ID
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return PartialView("_ShoppingTable", GetUserItems());
        }

        //Change pantry qty by passed value
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePantryPantry(int? id, int value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Verify that the item belogs to the user
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                if (item.User == currentUser)
                {
                    item.PantryQty = (item.PantryQty + value);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //return bad request if user is not the user of the item ID
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return PartialView("_PantryTable", GetUserItems());
        }

        //Change list qty by passed value
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangeListList(int? id, int value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Verify that the item belogs to the user
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                if (item.User == currentUser)
                {
                    item.ListQty = (item.ListQty + value);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //return bad request if user is not the user of the item ID
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return PartialView("_ShoppingTable", GetUserItems());
        }
        //Change list qty by passed value
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangeListPantry(int? id, int value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Verify that the item belogs to the user
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                if (item.User == currentUser)
                {
                    item.ListQty = (item.ListQty + value);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    //return bad request if user is not the user of the item ID
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return PartialView("_PantryTable", GetUserItems());
        }
        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
