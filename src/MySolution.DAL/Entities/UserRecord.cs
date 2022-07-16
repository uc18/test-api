using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySolution.DAL.Entities
{
    [Table("Users")]
    public class UserRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public int UserGroupId { get; set; }

        public int UserStateId { get; set; }

        [ForeignKey("UserGroupId")]
        public UserGroupRecord UserGroup { get; set; }

        [ForeignKey("UserStateId")]
        public UserStateRecord UserState { get; set; }
    }    
}