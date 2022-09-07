using MatchDots;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Classes
{
    public class GameMain: MonoBehaviour
    {
        
        protected BetweenLevels _betweenLevels;
        protected QuickSettings _quickSettings;
        protected UIDocument _uiDocument;

        protected TweenHolder _tweenHolder;
        protected System.Random _random;
        protected Timer _timer;
        protected GameHud _gameHud;
        protected GameState _gameState;

        
        
        protected void Restart()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
        }
    
        protected void NextLevel()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
        }
    
    
        protected string getBigText()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            return $"Level {sgd.nextLevel}";
        }
    
        protected string getSmallText(bool levelWon)
        {

            return levelWon ? "Yarn-tastic!" : "Level failed!";
        }
        

        protected void InitializeHud<T>(GameLayout gl) where T: GameHud, new()
        {
            
            //T data { get; set; };
            _gameHud = new T();
            _gameHud.Initialize(gl);
            _gameHud.SettingsButtonAction = () =>
            {
                _gameState = GameState.Settings;
            };
            _gameHud.AddToVisualElement(_uiDocument.rootVisualElement);
            
        }

        protected void InitializeUi<T>(GameLayout mainCamera) where T: GameHud, new()
        {
            InitializeUiDocument();
            
            InitializeHud<T>(mainCamera);
            InitializeQuickSettings(mainCamera);
            InitializeBetweenLevels(mainCamera);
        }
        
        protected void InitializeMisc()
        {
            _tweenHolder = new TweenHolder();
            _random = new System.Random();
            _timer = new Timer();
        }
        
        protected void InitializeUiDocument()
        {
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
            _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        } 
        
        protected void InitializeQuickSettings(GameLayout camera)
        {
            var sgd = SerialGameData.LoadOrGenerate();
            _quickSettings = new QuickSettings(camera, sgd.sound, sgd.music);
            _quickSettings.AddToVisualElement(_uiDocument.rootVisualElement);
            _quickSettings.setVisible(false);
            _quickSettings.SettingsButtonAction = () =>
            {
                QuickSettingsButtonFunction();
            };

            _quickSettings.MusicButtonAction = (bool b) =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                sgd.music = b ? 0 : 1;
                sgd.Save();
            };
        
            _quickSettings.SoundButtonAction = (bool b) =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                sgd.sound = b ? 0 : 1;
                sgd.Save();
            };
            _quickSettings.ReturnButtonAction = () =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                sgd.changeHearts(-1);
                sgd.Save();
                ToHQ();
            };
        }

        protected void InitializeBetweenLevels(GameLayout camera)
        {
            _betweenLevels = new BetweenLevels(camera);
            _betweenLevels.AddToVisualElement(_uiDocument.rootVisualElement);
            _betweenLevels.setVisible(false);
            _betweenLevels.OnCross = () =>
            {
                ToHQ();
            };
            _betweenLevels.OnBigButton = () =>
            {
            };
        }
        
        

        protected virtual void QuickSettingsButtonFunction()
        {
        }
        
        
        
        protected void ToHQ()
        {
            SceneManager.LoadScene("HQ", LoadSceneMode.Single);
        }
        
        
        protected enum GameState
        {
            Loading,Standby,Game,Settings,Won,Lost
        }
    }
    
    
    
    
}