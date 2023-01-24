using System.Reflection;

namespace FrogsNetwork.Freelancing.Helpers;
public static class ReflectionHelpers
{
    //public static T UpdateModel<T>(this T source, T destination)
    //{
    //    foreach(System.Reflection.PropertyInfo propertyInfo in typeof(T).GetProperties())
    //    {
    //        if ( propertyInfo.PropertyType.IsPrimitive)
    //        {
    //            if ( propertyInfo.PropertyType == typeof(string))
    //            {
    //                object objDestination = propertyInfo.GetValue(destination);
    //                object objSource= propertyInfo.GetValue(destination);

    //            }
    //        }
    //    }
    //    return source;

    //}

    public static T UpdateModel<T>(this T self, T to) where T : class
    {
        if (self != null && to != null)
        {
            var type = typeof(T);
            var ignoreList = new List<string>(new string[] { "Id" });
            var properties = from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             where !(ignoreList.Contains(pi.Name) || !pi.PropertyType.IsSimpleType() || pi.GetIndexParameters().Length != 0)
                             select pi;

            foreach (var property in properties)
            {
                object selfValue = type.GetProperty(property.Name).GetValue(self, null);
                object toValue = type.GetProperty(property.Name).GetValue(to, null);
                if (selfValue != toValue && toValue != null && (selfValue == null || !selfValue.Equals(toValue)))
                {
                    if (!IsDefaultValue(property, toValue))
                    {
                        property.SetValue(self, toValue);
                    }
                }
            }
        }
        return self;
    }

    private static bool IsDefaultValue(PropertyInfo propertyInfo, object value)
    {
        if (propertyInfo.PropertyType == typeof(decimal))
            return Convert.ToDecimal(value) == default(decimal);
        if (propertyInfo.PropertyType == typeof(int))
            return Convert.ToInt32(value) == default(int);
        if (propertyInfo.PropertyType == typeof(bool))
            return Convert.ToBoolean(value) == default(bool);
        if (propertyInfo.PropertyType == typeof(string))
            return string.IsNullOrEmpty(value.ToString());
        return false;
    }
}
