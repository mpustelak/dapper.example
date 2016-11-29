using System;

namespace mpustelak.Dapper.Examples.Entities
{
    public class UserDetailEntity
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime Created { get; set; }
    }
}
