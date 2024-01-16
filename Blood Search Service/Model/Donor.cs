using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebApplication1.Model
{
    [Table("Donor")]
    [Index(nameof(Id), IsUnique = true)]
    public class Donor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public String FullName { get; set; }

        [Required]
        [StringLength(50)]
        public String BloodType { get; set; }

        [Required]
        [StringLength(50)]
        public String Town { get; set; }

        [Required]
        [StringLength(50)]
        public String City { get; set; }

        [Required]
        [StringLength(50)]
        public String PhoneNo { get; set; }

        //[Required]
        //[StringLength(50)]
        //public String Photo { get; set;
        //
        //
        [Required]
        public long BranchId { get; set; }

        [Required]
        [StringLength(255)]  // Adjust the length based on Azure CDN requirements
        public string PhotoUrl { get; set; }
    }
    }

