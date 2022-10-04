using UnityEngine;

namespace GameFlow.General.Types.Interfaces
{
    public interface IDroppable
    {
        public GameObject DropSelfAndReturnReference();
        public void DropSelf();
    }
}