using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum AccountRepositoryExceptionCode
{
    WrongLoginOrPassword,
    AccountAlreadyLogin,
    CharacterAlreadyLogin,
    AcconuntAlreadyLogout,
    LoginIsTooShort,
    LoginIsTooLong,
    PasswordIsTooShort,
    PasswordIsTooLong,
    LoginContainsNotAllowedCharacters,
    PasswordContainsNotAllowedCharacters,
    LoginAlreadyExist,
    CharacterNameIsTooShort,
    CharacterNameIsTooLong,
    CharacterNameContainsNotAllowedCharacters,
    CharacterNameAlreadyExist,
    CharacterSlotIsEmpty
}
