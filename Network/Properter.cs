using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NetworkProject
{
    public class Properter<T> where T : class
    {
        T _item;

        public Properter(T item)
        {
            Set(item);
        }

        public void Set(T item)
        {
            var typeBuilder = GetTypeBuilder();

            var interfaceType = typeof(T);
            var propertiesInInterface = interfaceType.GetProperties();

            typeBuilder.AddInterfaceImplementation(interfaceType);

            foreach (var property in propertiesInInterface)
            {
                AddFieldAndProperty(typeBuilder, property);
            }

            Type generatedType = typeBuilder.CreateType();

            T properterItem = (T)Activator.CreateInstance(generatedType);

            SetProperties(item, properterItem, propertiesInInterface);

            _item = properterItem;
        }

        public T Get()
        {
            return _item;
        }

        private TypeBuilder GetTypeBuilder()
        {
            var assemblyName = new AssemblyName("ProperterAssembly");
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("ProperterModule");
            return moduleBuilder.DefineType("ProperterData");
        }

        private void AddFieldAndProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            var fieldBuilder = typeBuilder.DefineField('_' + property.Name, property.PropertyType, FieldAttributes.Public);

            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig | MethodAttributes.Virtual;

            var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, getSetAttributes,
                property.PropertyType, Type.EmptyTypes);

            var getILGenerator = getMethodBuilder.GetILGenerator();
            getILGenerator.Emit(OpCodes.Ldarg_0);
            getILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getILGenerator.Emit(OpCodes.Ret);

            if(property.CanRead)
            {

                typeBuilder.DefineMethodOverride(getMethodBuilder, property.GetGetMethod());      
            }

            var setMethodBuilder = typeBuilder.DefineMethod("set_" + property.Name, getSetAttributes,
                    null, new Type[] { property.PropertyType });

            var setILGenerator = setMethodBuilder.GetILGenerator();
            setILGenerator.Emit(OpCodes.Ldarg_0);
            setILGenerator.Emit(OpCodes.Ldarg_1);
            setILGenerator.Emit(OpCodes.Stfld, fieldBuilder);
            setILGenerator.Emit(OpCodes.Ret);

            if (property.CanWrite)
            {
                typeBuilder.DefineMethodOverride(setMethodBuilder, property.GetSetMethod());              
            }
        }

        private void SetProperties(T source, T target, PropertyInfo[] properties)
        {
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    object value = property.GetValue(source, null);
                    target.GetType().GetField('_' + property.Name).SetValue(target, value);
                }             
            }
        }
    }
}
