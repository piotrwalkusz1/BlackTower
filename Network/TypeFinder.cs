using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NetworkProject
{
    public class TypeFinder<T>
    {
        private Type[] _findedTypes;

        public TypeFinder()
        {
            FindTypesInheritType();
        }

        public void FindTypesInheritType()
        {
            var findedTypes = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                              from type in assembly.GetTypes()
                              where typeof(T).IsAssignableFrom(type)
                              where !type.IsAbstract && !type.IsInterface
                              select type;

            _findedTypes = findedTypes.ToArray();
        }

        public string[] GetTypesNames()
        {
            var names = from type in _findedTypes
                        select type.Name;

            return names.ToArray();
        }

        public T CreateInstantiateByName(string name)
        {
            for (int i = 0; i < _findedTypes.Length; i++)
            {
                if (_findedTypes[i].Name == name)
                {
                    CreateInstantiateByIndex(i);
                }
            }

            throw new ArgumentException("Type finder nie posiada w swojej bazie typu o takiej nazwie.");
        }

        public T CreateInstantiateByIndex(int index)
        {
            try
            {
                return (T)Activator.CreateInstance(_findedTypes[index]);
            }
            catch(IndexOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Type finder nie posiada w swojej bazie typu o takim indeksie.");
            }
        }
    }
}
