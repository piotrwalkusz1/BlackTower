using UnityEngine;
using System.Collections;

public class LoginMenuInfo
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool IsHelpDisplay { get; set; }

    public LoginMenuInfo()
    {
        Login = "";
        Password = "";
        IsHelpDisplay = false;
    }
}
