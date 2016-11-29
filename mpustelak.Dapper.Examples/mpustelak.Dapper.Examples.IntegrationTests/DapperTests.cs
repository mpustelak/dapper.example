using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using mpustelak.Dapper.Examples.Entities;
using NUnit.Framework;

namespace mpustelak.Dapper.Examples.IntegrationTests
{
    [TestFixture]
    public class DapperTests
    {
        [Test]
        public void Given_DataInDatabase_When_FetchingUsersUsingDapper_Then_ItShouldReturnAtLeastOneUser()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlScript = @"
                    SELECT * 
                    FROM 
	                    dbo.[User] u";
                var result = sqlConnection.Query<UserEntity>(sqlScript);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Any(), Is.True);
            }
        }

        [Test]
        [TestCase(1, "mpustelak")]
        [TestCase(2, "testuser")]
        [TestCase(3, "usertest")]
        public void Given_DataInDatabase_When_FetchingUserWithUserIdUsingDapper_Then_ItShouldReturnExpectedUserName(
            int userId,
            string expectedUserName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlScript = @"
                    SELECT * 
                    FROM 
	                    dbo.[User] u
                    WHERE
                        u.UserId = @UserId";
                var result = sqlConnection.QueryFirstOrDefault<UserEntity>(sqlScript, new {userId});

                Assert.That(result, Is.Not.Null);
                Assert.That(result.UserName, Is.EqualTo(expectedUserName));
            }
        }

        [Test]
        public void Given_DataInDatabase_When_FetchingUsersAndUserDetailsUsingDapper_Then_ItShouldReturnAtLeastOneUserAndOneUserDetail()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlScript = @"
                    SELECT * FROM dbo.[User] u
                    SELECT * FROM dbo.UserDetail ud";
                using (var multipleQuery = sqlConnection.QueryMultiple(sqlScript))
                {
                    var users = multipleQuery.Read<UserEntity>();
                    var userDetails = multipleQuery.Read<UserDetailEntity>();

                    Assert.That(users, Is.Not.Null);
                    Assert.That(users.Any(), Is.True);
                    Assert.That(userDetails, Is.Not.Null);
                    Assert.That(userDetails.Any(), Is.True);
                }
            }
        }


        [Test]
        public async Task Given_DataInDatabase_When_FetchingUsersUsingDapperAsync_Then_ItShouldReturnAtLeastOneUser()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlScript = @"
                    SELECT * 
                    FROM dbo.[User] u";
                var result = await sqlConnection.QueryAsync<UserEntity>(sqlScript);

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Any(), Is.True);
            }
        }

        [Test]
        public void Given_DataInDatabase_When_FetchingUsersAndUserDetailsUsingDapper_Then_ItShouldReturnAtLeastOneUserWithJoinedUserDetail()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["UserDatabase"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var sqlScript = @"
                    SELECT * 
                    FROM dbo.[User] u
	                    INNER JOIN dbo.UserDetail ud ON ud.UserId = u.UserId";

                var result = sqlConnection.Query<UserEntity, UserDetailEntity, UserEntity>(
                    sqlScript,
                    (u, ud) =>
                    {
                        u.UserDetails = ud;
                        return u;
                    },
                    splitOn: "UserId");

                Assert.That(result, Is.Not.Null);
                Assert.That(result.All(e => e.UserDetails != null), Is.True);
            }
        }
    }
}
