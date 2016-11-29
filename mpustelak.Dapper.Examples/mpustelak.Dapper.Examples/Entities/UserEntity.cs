using System.Collections.Generic;

namespace mpustelak.Dapper.Examples.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public UserDetailEntity UserDetails { get; set; }
    }
}