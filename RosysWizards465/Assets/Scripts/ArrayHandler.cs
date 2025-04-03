

using System.Collections.Generic;

public class ArrayHandler
{
    public static string arrayListToString(List<ButtonType> list)
    {
        string result = "[";
        for (int i = 0; i < list.Count; i++)
        {
            result += list[i].ToString();
            if (i < list.Count - 1)
            {
                result += ", ";
            }
        }
        result += "]";
        return result;
    }
}