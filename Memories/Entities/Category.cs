using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memories.Entities
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[StringLength (200, MinimumLength = 1)]
		public string Title { get; set; }

		public string Description { get; set; }

		[Required]
		public string ImagePath { get; set; }

		[ForeignKey("CategoryId")]
		public virtual ICollection<CategoryItem> CategoryItems { get; set; }

		[ForeignKey("CategoryId")]
		public virtual ICollection<UserCategory> UserCategory { get; set; }



	}
}

