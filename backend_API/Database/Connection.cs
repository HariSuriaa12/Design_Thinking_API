using backend_API.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace backend_API.Database
{
    public class Connection : DbContext
    {
        public Connection(DbContextOptions options)
                                    : base(options)
        {

        }

        public DbSet<Users> Users { get; set; } //For selecting records from DB
        public DbSet<swap_offer> swap_offers { get; set; } //For selecting records from DB

        public int SQL_Command_Execute(string query, List<NpgsqlParameter> DBParams)
        {
            try
            {
                Database.OpenConnection();
                Database.BeginTransaction();

                int RowsAffectedCount = Database.ExecuteSqlRaw(query, DBParams);

                Database.CommitTransaction();
                Database.CloseConnection();
                return RowsAffectedCount;
            }
            catch(Exception ex)
            {
                Database.RollbackTransaction();
                throw new Exception("Hit Error when executuing SQL command, Error : " + ex.Message);
            }
            finally
            {
                Database.CloseConnection();
            }
        }
    }
}