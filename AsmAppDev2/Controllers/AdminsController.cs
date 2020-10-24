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
            var userInfor = (from user in _context.Users
                             select new
                             /*FROM-IN: xác định nguồn dữ liệu truy vấn (Users). 
                             Nguồn dữ liệu tập hợp những phần tử thuộc kiểu lớp triển khai giao diện IEnumrable*/
                             /*SELECT: chỉ ra các dữ liệu được xuất ra từ nguồn */
                             {
                                 UserId = user.Id,
                                 Username = user.UserName,
                                 Emailaddress = user.Email,
                                 Phonenumber = user.PhoneNumber,
                                 RoleName = (from userRole in user.Roles
                                                 //JOIN kết hợp 2 trường dữ liệu tương ứng
                                             join role in _context.Roles //JOIN-IN: chỉ ra nguồn kết nối vs nguồn của FROM   
                                             on userRole.RoleId          //ON: chỉ ra sự ràng buộc giữa các phần tử  
                                             equals role.Id              //EQUALS: chỉ ra căn cứ vs ràng buộc (userRole.RoleId ~~ role.Id)
                                             select role.Name).ToList()  //==> chọn name Role đưa ra list
                             }
                             ).ToList().Select(p => new UserInRoles()    // ViewModel
                             {
                                 UserId = p.UserId,
                                 Username = p.Username,
                                 Email = p.Emailaddress,
                                 setPhone = p.Phonenumber,
                                 Role = string.Join(",", p.RoleName)  //  nối list roleName bởi dấu ,

                             }
                                                );
            return View(userInfor);
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
            userInDb.PhoneNumber = user.PhoneNumber;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}