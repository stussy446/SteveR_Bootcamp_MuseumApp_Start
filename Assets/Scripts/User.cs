using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Table("User")]
public class User 
{
    public static string loggedUserSaveKey = "loggedUserSaveKey";
    public static string LoggedInUserName => PlayerPrefs.GetString(loggedUserSaveKey, string.Empty);
    public static bool IsLoggedIn => !LoggedInUserName.Equals(string.Empty);

    public static void Login(string username) => PlayerPrefs.SetString(loggedUserSaveKey, username);

    public static void LogOff() => PlayerPrefs.DeleteKey(loggedUserSaveKey);
    [PrimaryKey] public string Username { get; set; }

    // THIS IS NOT RECOMMENDED DONT STORE PASSWORDS LIKE THIS IRL
    public string Password { get; set; }
}
