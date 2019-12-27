using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentRecords.Entities
{
    public class Student : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Grade { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }


        public virtual ICollection<StudentFile> Files { get; set; }

    }
}
