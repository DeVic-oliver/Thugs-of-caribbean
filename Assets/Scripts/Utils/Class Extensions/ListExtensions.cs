using System;
using System.Collections;
using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetRandomItem<T>(this List<T> list)
    {
        Random randomizer = new Random();
        int listSize = list.Count;
        if (listSize == 1)
        {
            return list[0];
        }
        int index = randomizer.Next(0, listSize);
        return list[index];
    }
}
