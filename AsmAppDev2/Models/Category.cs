using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsmAppDev2.Models
{
	public class Category
	{
		public int ID { get; set; }
		[Required]
		[DisplayName("Category Name")]
		public string Name_Category { get; set; }
		public string Description { get; set; }
	}
}