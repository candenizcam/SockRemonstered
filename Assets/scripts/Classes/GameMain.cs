using System;
using GoogleMobileAds.Api;
using MatchDots;
using ui;
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
        protected Action OnAdClosedAction;

        public InterstitialAd interstatial;
       

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            //Debug.Log("yokluğun çok zor");
            //MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            //Debug.Log("vur bu akılsız başı");
            //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "+ args.Message);
        }

        public void HandleOnAdOpening(object sender, EventArgs args)
        {
            //Debug.Log("sensiz olamadım");
            //MonoBehaviour.print("HandleAdOpening event received");
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            //Debug.Log("işte kuzu kuzu geldim");
            //MonoBehaviour.print("HandleAdClosed event received");
            OnAdClosedAction();
        }

        
        
        protected void LevelDone(bool won)
        {
            var lp = GetLevelPoints();
            var levelNo = 0;
            var buttonText = "NEXT";
            if (won)
            {
                _gameState = GameState.Won;
                SerialGameData.Apply(sgd =>
                {
                    levelNo = sgd.nextLevel;
                    sgd.nextLevel += 1;
                    sgd.coins += lp.number;
                });
                _betweenLevels.OnBigButton = Restart;
            }
            else
            {
                _gameState = GameState.Lost;
                SerialGameData.Apply(sgd =>
                {
                    levelNo = sgd.nextLevel;
                    if (sgd.changeHearts(-1) > 0)
                    {
                        buttonText = "RETRY";
                        _betweenLevels.OnBigButton = Restart;
                    }
                    else
                    {
                        buttonText = "RETURN";
                        _betweenLevels.OnBigButton = ToHQ;
                    }
                });
            }
            _betweenLevels.UpdateInfo(won, bigText: getBigText(levelNo), smallText: getSmallText(won), lp.text,buttonText);
        }

        protected virtual (int number, string text)  GetLevelPoints()
        {
            return (0,$"0");
        }
        
        
        protected void Restart()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            if (!Constants.SupressAd)
            {
                if (sgd.InterstatialAdTime() && interstatial.IsLoaded() )
                {
                    OnAdClosedAction = () =>
                    {
                        SerialGameData.Apply(a =>
                        {
                            var nl = Constants.GetNextLevel(a.nextLevel);
                            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
                        });
                        interstatial.Destroy();
                        _uiDocument.enabled = true;
                    };
                    _uiDocument.enabled = false;
                    interstatial.Show();

                }
                else
                {
                    var nl = Constants.GetNextLevel(sgd.nextLevel);
                    SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
                }
            }
            else
            {
                var nl = Constants.GetNextLevel(sgd.nextLevel);
                SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
            }
            
            
        }
    
        protected void NextLevel()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
        }
    
    
        protected string getBigText(int levelNo)
        {
            return $"Level {levelNo}";
        }
    
        protected string getSmallText(bool levelWon)
        {
            var i = _random.Next(0, Constants.WinTexts.Length);
            
            return levelWon ? Constants.WinTexts[i]: "Level failed!";
        }


        private void InitializeHud<T>(float topHeight = 220f, float bottomHeight = 200f) where T: GameHud, new()
        {
            _gameHud = new T();
            _gameHud.Initialize(topHeight,bottomHeight);
            _gameHud.SettingsButtonAction = () =>
            {
                _gameState = GameState.Settings;
            };
            _gameHud.AddToVisualElement(_uiDocument.rootVisualElement);
        }

        protected void InitializeUi<T>(float topHeight=220f, float bottomHeight=200f, TutorialFrame[] tutorialFrames= null) where T: GameHud, new()
        {
            var sgd1 = SerialGameData.LoadOrGenerate();
            InitializeUiDocument();
            var levelInfo = Constants.GetNextLevel(sgd1.nextLevel);
            Debug.Log($"{levelInfo.LevelNo}");
            InitializeHud<T>(topHeight,bottomHeight);
            _gameHud.SetLevelNo($"Level {sgd1.nextLevel}");
            InitializeQuickSettings(tutorialFrames ??= new TutorialFrame[]{},sgd1.sound,sgd1.music, levelInfo.LevelNo==0);
            InitializeBetweenLevels();
            if (!Constants.SupressAd)
            {
                interstatial = AdHandler.RequestInterstitial();
                interstatial.OnAdClosed += HandleOnAdClosed;
            }
                
        }
        
        protected void InitializeMisc()
        {
            _tweenHolder = new TweenHolder();
            _random = new System.Random();
            _timer = new Timer();
        }

        private void InitializeUiDocument()
        {
            
            _uiDocument = gameObject.GetComponent<UIDocument>();
            _uiDocument.panelSettings.referenceResolution = new Vector2Int((int)Constants.UiWidth, (int)Constants.UiHeight);
            _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
            _uiDocument.panelSettings.match = 0f;
        }

        private void InitializeQuickSettings(TutorialFrame[] tutorialFrames, int sound, int music, bool tutorialOn)
        {
            Debug.Log($"tfl {tutorialFrames.Length}");
            _quickSettings = new QuickSettings(sound, music,tutorialOn,tutorialFrames);
            _quickSettings.AddToVisualElement(_uiDocument.rootVisualElement);
            _quickSettings.SetVisible(false);
            _quickSettings.SettingsButtonAction = QuickSettingsButtonFunction;

            _quickSettings.MusicButtonAction = b =>
            {
                var sgd = SerialGameData.LoadOrGenerate();
                sgd.music = b ? 0 : 1;
                sgd.Save();
            };
        
            _quickSettings.SoundButtonAction = b =>
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

        private void InitializeBetweenLevels()
        {
            _betweenLevels = new BetweenLevels();
            _betweenLevels.AddToVisualElement(_uiDocument.rootVisualElement);
            _betweenLevels.setVisible(false);
            _betweenLevels.OnCross = ToHQ;
            _betweenLevels.OnBigButton = () =>
            {
            };
        }
        
        

        protected virtual void QuickSettingsButtonFunction()
        {
            _quickSettings.SetVisible(false);
            _gameState = GameState.Game;
        }
        
        
        
        protected void ToHQ()
        {
            var sgd = SerialGameData.LoadOrGenerate();
            if (!Constants.SupressAd)
            {
                if (sgd.InterstatialAdTime() && interstatial.IsLoaded() )
                {
                    OnAdClosedAction = () =>
                    {
                        SceneManager.LoadScene("HQ", LoadSceneMode.Single);
                        interstatial.Destroy();
                        _uiDocument.enabled = true;
                    };
                    _uiDocument.enabled = false;
                    interstatial.Show();

                }
                else
                {
                    SceneManager.LoadScene("HQ", LoadSceneMode.Single);
                }
            }
            else
            {
                SceneManager.LoadScene("HQ", LoadSceneMode.Single);
            }
        }
        
        
        protected enum GameState
        {
            Loading,Standby,Game,Settings,Won,Lost
        }
    }
    
    
    
    
}