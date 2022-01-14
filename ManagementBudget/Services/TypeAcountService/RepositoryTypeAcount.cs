using Dapper;
using Microsoft.Data.SqlClient;
using ManagementBudget.Models;

namespace ManagementBudget.Services.TypeAcountService
{

    public interface IRepositoryTypeAcount
    {
        Task Create(TypeAcountModel typeAcount);
        Task Delete(int Id);
        Task<bool> Exists(string name, int userId);
        Task<IEnumerable<TypeAcountModel>> Get(int userId);
        Task<TypeAcountModel> GetForId(int Id, int UserId);
        Task Update(TypeAcountModel typeAcount);
    }

    public class RepositoryTypeAcount: IRepositoryTypeAcount
    {
        private readonly string connectionString;
        public RepositoryTypeAcount(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Create(TypeAcountModel typeAcount)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"INSERT INTO TypeAcount (Name, UserId, Oder) values (@Name, @UserId, 0);SELECT SCOPE_IDENTITY();", typeAcount);
            typeAcount.Id = id;

        }

        public async Task<bool> Exists(string name, int userId)
        {
            using var connection = new SqlConnection(connectionString);
            var exist = await connection.QueryFirstOrDefaultAsync<int>($"SELECT 1 FROM TypeAcount where Name = @Name and UserId = @UserId;", new {name, userId});

            return exist == 1;
        }

        public async Task<IEnumerable<TypeAcountModel>> Get(int userId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TypeAcountModel>(@"select Id, Name, Oder from TypeAcount where UserId = @UserId;", new {userId});
        }

        public async Task Update(TypeAcountModel typeAcount)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Update TypeAcount set Name = @Name Where Id = @Id", typeAcount);

        }

        public async Task<TypeAcountModel> GetForId(int Id, int UserId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TypeAcountModel>(@"select Id, Name, Oder from TypeAcount where Id = @Id and UserId = @UserId", new {Id, UserId });
        }

        public async Task Delete(int Id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Delete TypeAcount where Id = @Id", new {Id});
        }
    }
}
