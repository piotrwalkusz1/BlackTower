using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.CLSCompliant(false)]
public static class IoC
{
    public static IClient GetImplementationClient()
    {
        return new LidgrenClient();
    }
}

