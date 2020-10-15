using AsmAppDev2.Models;
using AsmAppDev2.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace AsmAppDev2.Controllers
{
    public class AdminsController : Controller
    {
        private ApplicationDbContext _context;

        public AdminsController()
        {
            _context = new ApplicationDbContext();
        }
        //Get: Manage user
        public ActionResult Index()
        {
            var UsersWithRoles = (from user in _context.Users select new
                {
                   UserId = user.Id, Username = user.UserName, Emailaddress = user.Email,
                   Password = user.PasswordHash, RoleNames = (from userRole in user
                                                   .Roles
                                                   join role in _context
                                            .Roles on userRole.RoleId equals role
                                            .Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserInRoles()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Emailaddress,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(UsersWithRoles);
        }

        //Delete admin role
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var AccountInDb = _context.Users.SingleOrDefault(p => p.Id == id);
            if (AccountInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(AccountInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit admin role
        [HttpGet]

        public ActionResult Edit(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ApplicationUserViewModel
            {
                User = userInDb,
            };

            return View(viewModel);
        }
        [HttpPost]

        public ActionResult Edit(ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userInDb = _context.Users.SingleOrDefault(p => p.Id == user.Id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            userInDb.Email = user.Email;
            userInDb.PasswordHash = user.PasswordHash;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}