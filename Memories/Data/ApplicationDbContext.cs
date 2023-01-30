using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Memories.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Memories.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memories.Data
{
    public class AppUser:IdentityUser
    {
        [StringLength (50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(100)]
        public override string Email { get; set; }
        [StringLength(100)]
        public string City { get; set; }
        [StringLength(100)]
        public string State { get; set; }
        [StringLength(100)]
        public string FamilyName { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<UserCategory> UserCategory { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Category {get;set;}
        public DbSet<CategoryItem> CategoryItem {get;set;}
        public DbSet<MediaType> MediaType {get;set;}
        public DbSet<UserCategory> UserCategory {get;set;}
        public DbSet<Content> Content {get;set;}







    }
}