using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NetworkProject
{
    //Źródła powinny mieć tylko akcesory, które dodatkowo nie mają na początku
    //nazwy znaku podkreślenia "_", nie można dziedziczyć po dwóch interfejsach,
    //które mają akcesor o tej samej nazwie, tyczy się to również interfejsach
    //po sobie dziedziczących

    public static class Properter
    {
        public static object GetProperter(object source)
        {

            var typeBuilder = GetTypeBuilder();

            AddSerializableAttribute(typeBuilder);

            var interfaces = source.GetType().GetInterfaces();

            foreach (var sourceInterface in source.GetType().GetInterfaces())
            {
                typeBuilder.AddInterfaceImplementation(sourceInterface);

                foreach (var property in sourceInterface.GetProperties())
                {
                    AddFieldAndProperty(typeBuilder, property);
                }        
            }

            Type generatedType = typeBuilder.CreateType();  

            object properterItem = Activator.CreateInstance(generatedType);

            foreach (var sourceInterface in source.GetType().GetInterfaces())
            {
                SetProperties(source, properterItem, sourceInterface.GetProperties());
            }

            return properterItem;
        }

        private static TypeBuilder GetTypeBuilder()
        {
            var assemblyName = new AssemblyName("ProperterAssembly");
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("ProperterModule");
            return moduleBuilder.DefineType("ProperterData");
        }

        private static void AddFieldAndProperty(TypeBuilder typeBuilder, PropertyInfo property)
        {
            string fieldName = GetFieldName(property.Name);

            var fieldBuilder = typeBuilder.DefineField(fieldName, property.PropertyType, FieldAttributes.Public);          

            var getSetAttributes = MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig | MethodAttributes.Virtual;

            var getMethodBuilder = typeBuilder.DefineMethod("get_" + property.Name, getSetAttributes,
            property.PropertyType, Type.EmptyTypes);

            var getILGenerator = getMethodBuilder.GetILGenerator();
            getILGenerator.Emit(OpCodes.Ldarg_0);
            getILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            getILGenerator.Emit(OpCodes.Ret);

            if (property.CanRead)
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

        private static void SetProperties(object source, object target, PropertyInfo[] properties)
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

        private static string GetFieldName(string propertyName)
        {
            return '_' + propertyName;
        }

        private static void AddSerializableAttribute(TypeBuilder builder)
        {
            builder.SetCustomAttribute(new CustomAttributeBuilder(typeof(SerializableAttribute).GetConstructor(new Type[0]), new object[0]));
        }
    }
}
