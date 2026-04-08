using System;
using UnityEngine.Events;


namespace TOC.Core.Extensions
{
    public static class TOCMethodsExtensions
    {
        public static void SafeSubscribe(this UnityEvent unityEvent, UnityAction a)
        {
            unityEvent.RemoveListener(a);
            unityEvent.AddListener(a);
        }

        public static void SafeSubscribe(this Action arg0, Action arg1)
        {
            arg0 -= arg1;
            arg0 += arg1;
        }
    }
}