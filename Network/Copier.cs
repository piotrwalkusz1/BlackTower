using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NetworkProject
{
    public static class Copier
    {
        public static void CopyProperties(object source, object target)
        {
            foreach (var property in source.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(source, null);

                    try
                    {
                        property.SetValue(target, value, null);
                    }
                    catch (TargetException)
                    {
                        throw new ArgumentException("Source has more properties than target");
                    }
                }
            }
        }
    }
}
