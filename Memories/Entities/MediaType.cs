using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memories.Entities
{
	public class MediaType
	{
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [ForeignKey("MediaTypeId")]
        public ICollection<CategoryItem> CategoryItems { get; set; }
    }
}

