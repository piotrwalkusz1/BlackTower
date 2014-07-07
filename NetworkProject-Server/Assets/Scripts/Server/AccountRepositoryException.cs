using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AccountRepositoryException : Exception
{
    public AccountRepositoryExceptionCode ErrorCode { get; set; }

    public AccountRepositoryException(AccountRepositoryExceptionCode errorCode)
    {
        ErrorCode = errorCode;
    }
}
