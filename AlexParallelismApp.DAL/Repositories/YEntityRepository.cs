using System.Data;
using System.Data.SqlClient;
using AlexParallelismApp.DAL.Interfaces;
using AlexParallelismApp.DAL.Models;
using Microsoft.Extensions.Options;

namespace AlexParallelismApp.DAL.Repositories;

public class YEntityRepository : IYEntityRepository
{
    private readonly ConnectionStrings _connectionStrings;

    public YEntityRepository(IOptionsMonitor<ConnectionStrings> option)
    {
        _connectionStrings = option.CurrentValue;
    }

    public async Task<YEntity> FindAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        SqlCommand cmd = new SqlCommand("SELECT * FROM YEntity WHERE Id = @Id", connection);
        cmd.Parameters.AddWithValue("@Id", id);
        connection.Open();
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        return ReadEntity(reader);
    }

    public async Task<List<YEntity>> GetAllAsync()
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        SqlCommand cmd = new SqlCommand("SELECT * FROM YEntity", connection);
        connection.Open();
        SqlDataReader reader = await cmd.ExecuteReaderAsync();
        return ReadList(reader);
    }

    public async Task CreateAsync(YEntity entity)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        SqlCommand cmd = new SqlCommand(@"INSERT INTO YEntity
            (Name, Description, IsLocked, SessionId)
            VALUES (@Name, @Description, 0, '0')", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.ExecuteNonQuery();
    }

    public async Task UpdateAsync(YEntity entity, string sessionId)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        SqlCommand cmd = new SqlCommand(@"UPDATE YEntity
            SET Name = @Name, Description = @Description 
            WHERE Id = @Id AND SessionId = @SessionId", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", entity.Id);
        cmd.Parameters.AddWithValue("@Name", entity.Name);
        cmd.Parameters.AddWithValue("@Description", entity.Description);
        cmd.Parameters.AddWithValue("@SessionId", sessionId);
        cmd.ExecuteNonQuery();
    }

    public async Task LockAsync(int id, string sessionId)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        var cmd = new SqlCommand(@"UPDATE YEntity
            SET IsLocked = 1, SessionId = @SessionId WHERE Id = @Id AND IsLocked = 0", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.Parameters.AddWithValue("@SessionId", sessionId);
        cmd.ExecuteNonQuery();
    }

    public async Task UnlockAsync(int id)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        var cmd = new SqlCommand(@"UPDATE YEntity
            SET IsLocked = 0, SessionId = '0' WHERE Id = @Id", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", id);
        cmd.ExecuteNonQuery();
    }

    public async Task DeleteAsync(YEntity entity)
    {
        await using var connection = new SqlConnection(_connectionStrings.DefaultConnection);
        var cmd = new SqlCommand(@"DELETE FROM YEntity WHERE ID = @Id", connection);
        connection.Open();
        cmd.Parameters.AddWithValue("@Id", entity.Id);
        cmd.ExecuteNonQuery();
    }

    private static YEntity ReadEntity(IDataReader reader)
    {
        YEntity entity = new YEntity();
        while (reader.Read())
        {
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.Name = reader["Name"].ToString();
            entity.Description = reader["Description"].ToString();
            entity.IsLocked = Convert.ToBoolean(reader["IsLocked"]);
            entity.SessionId = reader["SessionId"].ToString();
        }

        return entity;
    }

    private static List<YEntity> ReadList(IDataReader reader)
    {
        List<YEntity> entities = new List<YEntity>();
        while (reader.Read())
        {
            YEntity entity = new YEntity
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                IsLocked = Convert.ToBoolean(reader["IsLocked"]),
                SessionId = reader["SessionId"].ToString()
            };

            entities.Add(entity);
        }

        return entities;
    }
}