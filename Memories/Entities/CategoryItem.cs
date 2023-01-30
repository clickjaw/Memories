using System;
using System.ComponentModel.DataAnnotations;

namespace Memories.Entities
{
	public class CategoryItem
	{
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public int MediaTypeId { get; set; }

        public DateTime DateTimeAdded { get; set; }
    }
}

