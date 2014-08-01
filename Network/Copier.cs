using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NetworkProject
{
    public static class Copier
    {
        public static void CopyAllSourceProperties(object source, object target)
        {
            Type targetType = target.GetType();

            foreach (var property in source.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    object value = property.GetValue(source, null);

                    try
                    {
                        targetType.GetProperty(property.Name).SetValue(target, value, null);
                    }
                    catch (NullReferenceException)
                    {
                        throw new ArgumentException("Source has more properties than target : " + property.Name);
                    }
                }
            }
        }

        public static void CopyAllTargetProperties(object source, object target)
        {
            Type sourceType = source.GetType();

            foreach (var property in target.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    try
                    {
                        object value = sourceType.GetProperty(property.Name).GetValue(source, null);

                        property.SetValue(target, value, null);
                    }
                    catch (NullReferenceException)
                    {
                        throw new ArgumentException("Target has more properties than source : " + property.Name);
                    }
                }
            }
        }
    }
}
