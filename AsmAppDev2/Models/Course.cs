using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AsmAppDev2.Models
{
	public class Course
	{
		public int ID { get; set; }

		[Required]
		[DisplayName("Name Course")]
		public string Name_Course { get; set; }
		public int CategoryID { get; set; }
		public Category Category { get; set; }
		public int TopicID { get; set; }
		public Topic Topic { get; set; }

	}
}