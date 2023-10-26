using System.Data;
using System.Data.SqlClient;
using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;

namespace AlexParallelismApp.DAL.Repositories;

public class XEntityRepository : IXEntityRepository
{
    private readonly string _connectionString;

    public XEntityRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<XEntity> FindAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM XEntity WHERE Id = @Id", connection);
        cmd.Parameters.AddWithValue("@Id", id);
        connection.Open();
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        return ReadEntity(reader);
    }

    public async Task<List<XEntity>> GetAllAsync()
    {
        await using var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("SELECT * FROM XEntity", connection);
        connection.Open();
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        return ReadList(reader);
    }

    public async Task CreateAsync(XEntity entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(@"INSERT INTO XEntity
            (Name, Description, UpdateTime)
            VALUES (@Name, @Description, GETDATE())", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.ExecuteNonQuery();
    }

    public async Task UpdateAsync(XEntity entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(@"UPDATE XEntity
            SET Name = @Name, Description = @Description, UpdateTime = GETDATE()
            WHERE Id = @Id", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", entity.Id);
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.ExecuteNonQuery();
    }

    public async Task DeleteAsync(XEntity entity)
    {
        await using var connection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(@"DELETE FROM XEntity
            WHERE ID = @Id AND UpdateTime =  @UpdateTime", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", entity.Id);
        cmd.Parameters.AddWithValue("@UpdateTime", entity.UpdateTime);
        cmd.ExecuteNonQuery();
    }

    private static XEntity ReadEntity(IDataReader reader)
    {
        XEntity entity = new XEntity();
        while (reader.Read())
        {
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.Name = reader["Name"].ToString();
            entity.Description = reader["Description"].ToString();
            entity.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
        }

        return entity;
    }

    private static List<XEntity> ReadList(IDataReader reader)
    {
        List<XEntity> entities = new List<XEntity>();
        while (reader.Read())
        {
            XEntity entity = new XEntity
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                UpdateTime = Convert.ToDateTime(reader["UpdateTime"])
            };

            entities.Add(entity);
        }

        return entities;
    }
}