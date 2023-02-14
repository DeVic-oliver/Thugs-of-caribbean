using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core.Interfaces
{
    public interface IScoreable 
    {
        public bool InstanceScored { get; set; }
        public void CountScore();
    }
}