using System.Threading.Tasks;
using UnityEngine;

namespace GameFlow.App.Utilities
{
    public class Helpers
    {
        public static async Task Delay(int seconds) => await Task.Delay(seconds * 600);
        
        public static void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public static void UnlockCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}