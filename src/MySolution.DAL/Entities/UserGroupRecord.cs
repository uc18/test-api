using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySolution.DAL.Entities
{
    [Table("UserGroup")]
    public class UserGroupRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }
    }    
}