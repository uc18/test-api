using System;

namespace MySolution.Web.DTO
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserGroupId { get; set; }

        public int UserStateId { get; set; }
    }
}