using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    private static string databasePath = "GameDatabase.db";
    private static readonly SQLiteConnection connection;

    static Database()
    {
        connection = new SQLiteConnection(databasePath);

        connection.CreateTable<User>();
        connection.CreateTable<UserRating>();
    }

    public static void RegisterPlayer(string userName, string password)
    {
        connection.Insert(new User 
        { 
            Username = userName, 
            Password = password 
        });
    }

    public static User GetUser(string username)
    {
        try
        {
            return connection.Get<User>(username);
        }
        catch
        {
            return null;
        }
    }
}
