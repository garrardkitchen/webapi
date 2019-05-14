using System;
using System.Data;
using System.Data.SqlClient;
using Users.Shared;

namespace Users.Api
{
    public interface IRepository<T>
    {
        T FindUserByEmail(string email);
        T Add(T user);
        T Update(T user);

        IDbConnection Connection { get; }
    }
}