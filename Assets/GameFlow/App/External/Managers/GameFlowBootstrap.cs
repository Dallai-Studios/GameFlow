using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFlow.App.External.Managers
{
    public static class GameFlowBootstrap
    {
        private static Dictionary<string, string> definitions = new();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("GameFlowManager")));
            ReadGameFlowMainDefinitionFile();
            if (definitions["inject-custom-managers"] == "no") return;
            InjectCustomManagers();
        }
        
        private static void ReadGameFlowMainDefinitionFile()
        {
            string basePath = Application.dataPath;
          
            using (StreamReader streamReader = File.OpenText($"{basePath}/GameFlow/Editor/Framework/game-flow.gfd"))
            {
                string content = string.Empty;
                while ((content = streamReader.ReadLine()) != null)
                {
                    if (content.StartsWith("@@")) continue;
                    if (string.IsNullOrEmpty(content)) continue;   
                    string[] stringArray = content.Split(":");
                    definitions.Add(stringArray[0].Trim(), stringArray[1].Trim());
                }
                streamReader.Close();
            }
        }

        private static void InjectCustomManagers()
        {
            string managersFilePath = definitions["managers-folder"] + definitions["managers-file"];
            using (StreamReader streamReader = File.OpenText(managersFilePath))
            {
                string lineContent = string.Empty;
                while ((lineContent = streamReader.ReadLine()) != null)
                {
                    if (lineContent.StartsWith("@@")) continue;
                    if (string.IsNullOrEmpty(lineContent)) continue;
                    Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load(lineContent.Trim())));
                }
                streamReader.Close();
            }
        }
    }
}