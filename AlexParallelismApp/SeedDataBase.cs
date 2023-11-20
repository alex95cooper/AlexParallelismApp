using System.Data.SqlClient;

namespace AlexParallelismApp;

public static class SeedDataBase
{
    public static void Init(string connectionString)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        InitXEntities(connection);
        FillXEntities(connection);
        InitYEntities(connection);
        FillYEntities(connection);
    }

    private static void InitXEntities(SqlConnection connection)
    {
        SqlCommand cmd = new SqlCommand(@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='XEntity' and xtype='U')  
        BEGIN
        CREATE TABLE XEntity
        (
        Id INT IDENTITY (1,1) ,
        Name  VARCHAR(50) NOT NULL UNIQUE,
        Description VARCHAR(max),
        UpdateTime DATETIME,
        PRIMARY KEY (Id)) 
        END", connection);
        connection.Open();
        cmd.ExecuteNonQuery();
    }

    private static void FillXEntities(SqlConnection connection)
    {
        SqlCommand countCmd = new("SELECT COUNT(*) FROM XEntity", connection);
        if ((int) countCmd.ExecuteScalar() == 0)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO XEntity
            (Name, Description, UpdateTime)
             VALUES 
             ('Alex', 'Its description of Alex', GETDATE()),
             ('Anton', 'Its description of Anton', GETDATE()),
             ('Veleriy', 'Its description of Veleriy', GETDATE()),
             ('Sergey', 'Its description of Sergey', GETDATE()),
             ('Olga', 'Its description of Olga', GETDATE()),
             ('Alexander', 'Its description of Alexander', GETDATE()),
             ('Elena', 'Its description of Elena', GETDATE()),
             ('Vitaliy', 'Its description of Vitaliy', GETDATE())", connection);
            cmd.ExecuteNonQuery();
        }
    }

    private static void InitYEntities(SqlConnection connection)
    {
        SqlCommand cmd = new SqlCommand(@"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='YEntity' and xtype='U')  
        BEGIN
        CREATE TABLE YEntity
        (
        Id INT IDENTITY (1,1) ,
        Name  VARCHAR(50) NOT NULL UNIQUE,
        Description VARCHAR(MAX),
        IsLocked BIT,
        SessionId VARCHAR(MAX),
        PRIMARY KEY (Id)) 
        END", connection);
        cmd.ExecuteNonQuery();
    }

    private static void FillYEntities(SqlConnection connection)
    {
        SqlCommand countCmd = new("SELECT COUNT(*) FROM YEntity", connection);
        if ((int) countCmd.ExecuteScalar() == 0)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO YEntity
            (Name, Description, IsLocked, SessionId)
             VALUES 
             ('Den', 'Its description of Den', 0, '0'),
             ('Peter', 'Its description of Peter', 0, '0'),
             ('Samanta', 'Its description of Samanta', 0, '0'),
             ('Paul', 'Its description of Paul', 0, '0'),
             ('Elvis', 'Its description of Elvis', 0, '0'),
             ('Nasty', 'Its description of Nasty', 0, '0'),
             ('Harry', 'Its description of Harry', 0, '0'),
             ('Bob', 'Its description of Bob', 0, '0')", connection);
            cmd.ExecuteNonQuery();
        }
    }
}