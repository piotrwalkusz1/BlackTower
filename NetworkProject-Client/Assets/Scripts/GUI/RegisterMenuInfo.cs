using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RegisterMenuInfo
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

    public RegisterMenuInfo()
    {
        Login = "";
        Password = "";
        RepeatPassword = "";
    }
}
