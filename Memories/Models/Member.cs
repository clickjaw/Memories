using System;
using Memories.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Memories.Data.Validation;

namespace Memories.Models
{
	public class Member
	{
        public long Id { get; set; }

        [Required(ErrorMessage = "Who is in the picture")]
        public string Name { get; set; }

        //public string? Name1 { get; set; }
        //public string? Name2 { get; set; }
        //public string? Name3 { get; set; }

        public string Slug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 2")]
        public string Description { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please enter a category")]
        public long CategoryId { get; set; }

        public Family Family { get; set; }

        public string Image { get; set; } = "noimage.png";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}

