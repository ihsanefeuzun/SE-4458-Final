using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    [Table("House")]
    [Index(nameof(Address), IsUnique = true)]
    public class House
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(20)]
        public String Name { get; set; }

        //[Required]
        //public List<DateTime> BookedDates { get; set; }

        [Required]
        [StringLength(20)]
        public String OwnerName { get; set; }

        [Required]
        [StringLength(50)]
        public String Address { get; set; }

        [Required]
        public int NumberofPeople { get; set; }

        [Required]
        [StringLength(20)]
        public string Amenities { get; set; }
    }
}
