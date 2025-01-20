using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevSpot.Data;
using DevSpot.Migrations;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DevSpot.Models
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Company { get; set; }
        
        [Required]
        public string Location { get; set; }

        public DateTime? PostedDate { get; set; } = DateTime.UtcNow;

        public bool IsApproved { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
