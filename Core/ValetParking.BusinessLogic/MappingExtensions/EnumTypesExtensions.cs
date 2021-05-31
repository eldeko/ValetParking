using System;

namespace ValetParking.BusinessLogic.MappingExtensions
{
    public static class EnumTypesExtensions
    {
        public static TEnum ConvertEnum<TEnum>(this Enum source)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), source.ToString(), true);
        }
    }
}
