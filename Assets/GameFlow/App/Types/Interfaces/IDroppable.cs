using UnityEngine;

namespace GameFlow.App.Types.Interfaces
{
    public interface IDroppable
    {
        public GameObject DropSelfAndReturnReference();
        public void DropSelf();
    }
}