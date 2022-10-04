using UnityEngine;

namespace GameFlow.Application.External.Managers
{
    public static class GameFlowBootstrap
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("GameFlowManager")));
        } 
    }
}