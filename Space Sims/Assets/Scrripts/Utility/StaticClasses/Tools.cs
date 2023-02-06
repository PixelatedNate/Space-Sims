using System;
using Random = UnityEngine.Random;

public static class Tools
{
    public static T GetRandomEnumValue<T>(Type enumType)
    {
        var values = Enum.GetValues(enumType);
        int index = Random.Range(0, values.Length);
        return ((T)values.GetValue(index));
    }


}
