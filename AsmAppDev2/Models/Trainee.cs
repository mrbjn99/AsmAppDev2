using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsmAppDev2.Models
{
	public class Trainee
	{
		public int TraineeID { get; set; }
		public string Full_Name { get; set; }
		public string Email { get; set; }
		public string Education { get; set; }
		public string Programming_Language { get; set; }
		public string Experience_Details { get; set; }
		public string Department { get; set; }
		public int Phone { get; set; }
		public string UserID { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}