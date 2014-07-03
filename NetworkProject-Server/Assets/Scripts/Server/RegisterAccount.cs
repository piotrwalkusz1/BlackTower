using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NetworkProject;

[System.CLSCompliant(false)]
public class RegisterAccount
{
    public int IdAccount { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<RegisterCharacter> Characters { get; private set; }

    public RegisterAccount()
    {
        Characters = new List<RegisterCharacter>();
    }

    public bool CheckPassword(string password)
    {
        return password == Password;
    }

    public void AddCharacter(RegisterCharacter character)
    {
        character.MyAccount = this;

        Characters.Add(character);
    }

    public bool CanAddCharacter()
    {
        return Characters.Count < Settings.maxCharactersInAccount;
    }
}
