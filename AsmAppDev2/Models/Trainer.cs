using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsmAppDev2.Models
{
	public class Trainer
	{
		public int TrainerID { get; set; }
		public int UserID { get; set; }
		public string Full_Name { get; set; }
		public string Email { get; set; }
		public string Working_Place { get; set; }
		public int Phone { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; }
	}
}