using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using GameFlow.App.Types.Enums;
using GameFlow.App.Types.ScriptableObjects;
using GameFlow.App.Utilities;
using GameFlow.Types.ScriptableObjects;
using GameFlow.Types.Classes;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFlow.App.External.Managers
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance;

        [Header("Define the actual game state")]
        [SerializeField] private GameState actualGameState;
        public event Action OnGameStateChange;

        [Space]
        
        [Header("Game Default Parameters")]
        [Tooltip("Define the Game general configuration parameters")]
        [SerializeField] private GameGeneralConfiguration gameGeneralConfig;
        
        [Space]
        
        [Header("Define the GameFlow Debug Options")]
        [SerializeField] private GameDebugConfig gameDebugConfig;
        
        [Space]
        
        [Header("Define the game default loading screen")]
        [SerializeField] private GameObject loadingCanvas;
        private Slider loadingSlider;

        [Space] 
        [Header("Define the audio sources for music and sound effects")] 
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioSource soundPlayer;
        [SerializeField] private AudioRecordDatabase audioRecordDatabase;
        
        
        
        // MONO BEHAVIOUR
        private void Start()
        {
            Instance ??= this;
            this.SubscribeToEvents();
            this.GetOrInstantiateDefaultLoadingCanvas();
            this.LoadGlobalAudioSources();
            Helpers.UnlockCursor();
            if (this.gameDebugConfig.ShouldRunFromTheStartScene) this.GoToFirstGameScene();
        }

        private void OnDisable() => this.UnsubscribeToEvents();
        
        
        
        // APPLICATION MANAGER
        public void CloseApplication() => Application.Quit();
        
        
        
        // GAME STATE MANAGEMENT
        public void SetGameState(GameState NewGameState) 
        {
            this.actualGameState = NewGameState;
            this.RaiseEventOnGameStateChange();
        }

        public GameState GetGameState() => this.actualGameState;

        
        
        // SCENE MANAGEMENT
        public void LoadSceneByName(string sceneName)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            this.loadingCanvas.SetActive(true);
            while (scene.progress < 0.9f)
            {
                this.UpdateLoadingSliderValue(scene.progress);
            }
            scene.allowSceneActivation = true;
            this.loadingCanvas.SetActive(false);
        }

        public void LoadSceneByIndex(int index)
        {
            AsyncOperation scene = SceneManager.LoadSceneAsync(index);
            scene.allowSceneActivation = false;
            this.loadingCanvas.SetActive(true);
            while (scene.progress < 0.9f)
            {
                this.UpdateLoadingSliderValue(scene.progress);
            }
            scene.allowSceneActivation = true;
            this.loadingCanvas.SetActive(false);
        }
        
        
        
        // GLOBAL SOUND MANAGEMENT
        public void PlaySound(string soundName)
        {
            AudioRecord audioClip = this.GetAudioRecordFromAudioDatabase(soundName);
            this.SetupAndPlaySoundPlayer(audioClip);   
        }

        public void PlaySound(int index)
        {
            AudioRecord audioClip = this.GetAudioRecordFromAudioDatabase(index);
            this.SetupAndPlayMusicPlayer(audioClip);
        }

        public void StopSound() => this.soundPlayer.Stop();

        public void PlayMusic(string musicName)
        {
            AudioRecord audioClip = this.GetAudioRecordFromAudioDatabase(musicName);
            this.SetupAndPlayMusicPlayer(audioClip);
        }
        
        public void PlayMusic(int index)
        {
            AudioRecord audioClip = this.GetAudioRecordFromAudioDatabase(index);
            this.SetupAndPlayMusicPlayer(audioClip);
        }

        public void StopMusic() => this.musicPlayer.Stop();
        

        
        // EVENT RAISERS
        private void RaiseEventOnGameStateChange() => this.OnGameStateChange?.Invoke();
        
        
        
        // PRIVATE METHODS
        private void GoToFirstGameScene() => SceneManager.LoadScene(this.gameDebugConfig.StartSceneIndex);
        
        private void GetOrInstantiateDefaultLoadingCanvas()
        {
            if (GameObject.Find(this.loadingCanvas.name)) 
                this.loadingCanvas = GameObject.Find(this.loadingCanvas.name);
            else 
                this.loadingCanvas = Resources.Load<GameObject>(this.loadingCanvas.name);

            this.loadingCanvas.SetActive(false);
            DontDestroyOnLoad(this.loadingCanvas);
            this.loadingSlider = this.loadingCanvas.GetComponentInChildren<Slider>();
            this.loadingSlider.value = 0f;
        }
        
        private void ResetLoadingSliderValue() => this.loadingSlider.value = 0f;
        
        private void UpdateLoadingSliderValue(float value) => this.loadingSlider.value = value;
        
        private void SubscribeToEvents()
        {
            this.gameGeneralConfig.OnChangeMasterVolume += this.ChangeVolumes;
            this.gameGeneralConfig.OnChangeMusicVolume += this.ChangeVolumes;
            this.gameGeneralConfig.OnChangeSoundEffectsVolume += this.ChangeVolumes;
        }

        private void UnsubscribeToEvents()
        {
            this.gameGeneralConfig.OnChangeMasterVolume -= this.ChangeVolumes;
            this.gameGeneralConfig.OnChangeMusicVolume -= this.ChangeVolumes;
            this.gameGeneralConfig.OnChangeSoundEffectsVolume -= this.ChangeVolumes;
        }
        
        private void LoadGlobalAudioSources()
        {
            AudioSource[] audioSources = this.GetComponents<AudioSource>();
            this.musicPlayer = audioSources[0];
            this.soundPlayer = audioSources[1];
        }
        
        private AudioRecord GetAudioRecordFromAudioDatabase(int index)
        {
            AudioRecord gameAudioClip = this.audioRecordDatabase.Records[index];
            return gameAudioClip;
        }
        
        public AudioRecord GetAudioRecordFromAudioDatabase(string clipName)
        {
            AudioRecord gameAudioClip = this.audioRecordDatabase.Records.Find(record => record.ClipName == clipName);
            return gameAudioClip;
        }
        
        private void SetupAndPlaySoundPlayer(AudioRecord gameAudioClip)
        {
            this.soundPlayer.loop = gameAudioClip.Loop;
            this.soundPlayer.clip = gameAudioClip.AudioClip;
            this.soundPlayer.volume = this.gameGeneralConfig.GetGameSoundEffectsVolume();
            this.soundPlayer.Play();
        }
        
        private void SetupAndPlayMusicPlayer(AudioRecord gameAudioClip)
        {
            this.musicPlayer.loop = gameAudioClip.Loop;
            this.musicPlayer.clip = gameAudioClip.AudioClip;
            this.musicPlayer.volume = this.gameGeneralConfig.GetGameMusicVolume();
            this.musicPlayer.Play();
        }

        private void ChangeVolumes()
        {
            this.musicPlayer.volume = this.gameGeneralConfig.GetGameMusicVolume();
            this.soundPlayer.volume = this.gameGeneralConfig.GetGameSoundEffectsVolume();
        }
    }
}