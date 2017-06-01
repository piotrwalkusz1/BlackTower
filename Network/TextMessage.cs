using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkProject
{
    public enum TextMessage
    {
        WrongLoginOrPassword = 0,
        AccountAlreadyLogin = 1,
        LoginIsFail = 2,
        RegisterIsFail = 3,
        LoginIsTooShort = 4,
        LoginIsTooLong = 5,
        PasswordIsTooShort = 6,
        PasswordIsTooLong = 7,
        LoginContainsNotAllowedCharacters = 8,
        PasswordContainsNotAllowedCharacters = 9,
        RegisterIsSuccess = 10,
        LoginAlreadyExist = 11,
        PasswordAndRepeatPasswordAreDifferent = 12,
        CharacterNameIsTooShort = 13,
        CharacterNameIsTooLong = 14,
        CharacterNameContainsNotAllowedCharacters = 15,
        CharacterNameAlreadyExist = 16,
        CreateCharacterIsFail = 17,
        ItemBagIsFull = 18,
        ConnectingIsFail = 19,
        YouAreDead = 20
    }
}
