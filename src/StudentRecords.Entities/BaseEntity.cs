using System;
using System.ComponentModel.DataAnnotations;

namespace StudentRecords.Entities
{
    public class BaseEntity
    {
        [Required]
        [Display(Name = "Create on")]
        public DateTime CreatedOn{ get; set; }

        [Display(Name ="Modified on")]
        public DateTime? ModifiedOn { get; set; }
    }
}