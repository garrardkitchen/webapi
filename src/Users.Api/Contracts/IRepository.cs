using System.Data;

namespace Users.Api.Contracts
{
    public interface IRepository<T>
    {
        T FindUserByEmail(string email);
        T Add(T user);
        T Update(T user);

        IDbConnection Connection { get; }
    }
}