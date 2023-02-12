using System.Collections;

namespace Assets.Scripts.Core.Interfaces
{
    public interface IDisablea
    {
        protected void DisableMe();

        protected IEnumerator DisableInstance();
    }
}