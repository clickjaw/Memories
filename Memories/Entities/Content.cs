using System;
using System.ComponentModel.DataAnnotations;

namespace Memories.Entities
{
	public class Content
	{
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        public string Image { get; set; }

        public string ImageLink { get; set; }

        public CategoryItem CategoryItem { get; set; }

    }
}

