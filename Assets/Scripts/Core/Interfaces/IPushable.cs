using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Interfaces
{
    public interface IPushable
    {
        public void Push(Vector3 force);
    }
}