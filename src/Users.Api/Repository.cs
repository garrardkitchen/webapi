using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Users.Api.Contracts;
using Users.Api.Exceptions;
using Users.Shared;

namespace Users.Api
{
    public class Repository : IRepository<UserDto>
    {
        private readonly string _connString;

        public Repository(string connString)
        {
            _connString = connString;
        }

        public UserDto FindUserByEmail(string email)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.QueryFirstOrDefault<UserDto>("sp_sel_user", new {email = email}, commandType: CommandType.StoredProcedure);
            }
        }


        public UserDto Add(UserDto user)
        {
            try
            {
                using (IDbConnection conn = Connection)
                {
                    return conn.QueryFirstOrDefault<UserDto>("sp_ins_user", new {firstname= user.Firstname, surname=user.Surname, email=user.Email, password=user.Password}, commandType: CommandType.StoredProcedure);
                }
            }
            // TODO: Assume less and do more! (G. Kitchen)
            catch (MySqlException)
            {
                throw new DuplicateUserException();
            }
        }

        public UserDto Update(UserDto user)
        {
            using (IDbConnection conn = Connection)
            {
                return conn.QueryFirstOrDefault<UserDto>("sp_upd_user", new {firstname= user.Firstname, surname=user.Surname, email=user.Email, password=user.Password}, commandType: CommandType.StoredProcedure);
            }
        }

        public IDbConnection Connection
        {
            get { return new MySqlConnection(_connString);}
        }
    }
}