using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AccountRepositoryToSave
{
    public RegisterAccount[] RegisterAccounts { get; set; }
    public int NextIdAccount { get; set; }
    public int NextIdCharacter { get; set; }

    public AccountRepositoryToSave(RegisterAccount[] accounts, int nextIdAccount, int nextIdCharacter)
    {
        RegisterAccounts = accounts;
        NextIdAccount = nextIdAccount;
        NextIdCharacter = nextIdCharacter;
    }
}
