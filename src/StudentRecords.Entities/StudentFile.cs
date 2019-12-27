using System.ComponentModel.DataAnnotations;

namespace StudentRecords.Entities
{
    public class StudentFile : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public byte[] Contents { get; set; }


        public virtual Student Student { get; set; }
    }
}