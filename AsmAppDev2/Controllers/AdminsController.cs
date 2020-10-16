using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
    // This file contains the DELETE and EDIT for the Admin Role
    public class AdminsController : Controller
    {
        //Create bridges between entities and databases
        private ApplicationDbContext _context;

        public AdminsController()
        {
            _context = new ApplicationDbContext();
        }

        //Get: Manage user
        public ActionResult Index()
        {
            var usersWithRoles = (from user in _context.Users  
                                  select new  
                                  {  
                                      UserId = user.Id,                                        
                                      Username = user.UserName,  
                                      EmailAddress = user.Email,  
                                      RoleNames = (from userRole in user.Roles  
                                                   join role in _context.Roles on userRole.RoleId   
                                                   equals role.Id  
                                                   select role.Name).ToList()  
                                  }).ToList().Select(u => new UserInRoles()  
   
                                  {  
                                      UserId = u.UserId,  
                                      Username = u.Username,  
                                      Email = u.EmailAddress,  
                                      Role = string.Join(",", u.RoleNames)  
                                  });  
   
   
            return View(usersWithRoles);  
        }

        //Delete admin role
        [HttpGet]
        public ActionResult Delete(string id)
        {
            // Find and assign the Id value in the Users table to accountInDb
            var accountInDb = _context.Users.SingleOrDefault(p => p.Id == id);
            if (accountInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(accountInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit admin role
        [HttpGet]
        public ActionResult Edit(string id)
        {
            // Find and assign the Id value in the Users table to userInDb
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == id);
            if (userInDb == null)
            {
                return HttpNotFound();
            }

            return View(userInDb);
        }

        [HttpPost]
        public ActionResult Edit(ApplicationUser user)
        {
            // Check the value of Id
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == user.Id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            userInDb.UserName = user.UserName;
            userInDb.Email = user.Email;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    
}