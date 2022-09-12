using UnityEngine;

namespace GameFlow.General.Interfaces
{
    public interface IDroppable
    {
        public GameObject DropSelfAndReturnReference();
        public void DropSelf();
    }
}