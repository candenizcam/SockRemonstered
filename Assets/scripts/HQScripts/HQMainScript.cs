using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using GoogleMobileAds.Api;
using HQScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HQMainScript : MonoBehaviour
{
    private System.Random _random;
    private HQLayout _mainCamera;
    private UIDocument _uiDocument;
    private VisualElement _safeElement;
    private Timer _timer;
    private float _timeHolder;
    private HQHud _hqHud;
    private Shop _shop;
    private RewardedAd _rewarded;
    public bool ResetSaves = false;
    public MonsterPrefabScript MonsterPrefabScript;
    public List<FurnitureScript> Furnitures;

    public SpriteRenderer bg;
    // Start is called before the first frame update
    void Awake()
    {
        if (ResetSaves)
        {
            Debug.LogWarning("warning, saves are reset");
            SerialGameData.ResetSaves();
        }

        
        Application.targetFrameRate = 60;
        
        _random = new System.Random();
        _timer = new Timer();
        var sgd = SerialGameData.LoadOrGenerate();
        
        
        //Debug.Log($"sw: {Screen.width}, {Screen.height}");
        _uiDocument = gameObject.GetComponent<UIDocument>();
        //_uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        //1170, 2532
        _uiDocument.panelSettings.referenceResolution = new Vector2Int((int)Constants.UiWidth, (int)Constants.UiHeight);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        _uiDocument.panelSettings.match = 0f;
        _safeElement = new VisualElement();
        _safeElement.style.position = Position.Absolute;
        _safeElement.style.top = Constants.UnsafeTopUi;
        _safeElement.style.bottom = Constants.UnsafeBottomUi;
        _safeElement.style.left = Constants.UnsafeLeftUi;
        _safeElement.style.right = Constants.UnsafeRightUi;
        _uiDocument.rootVisualElement.Add(_safeElement);
        

        _mainCamera = new HQLayout(Camera.main,190f,200f);

        _hqHud = new HQHud(_mainCamera);
        
        
        _hqHud.AddToVisualElement(_safeElement);
        var h = sgd.getHeartsAndRem();
        _hqHud.UpdateInfo(sgd.coins,h.hearts,h.rem);
        _hqHud.PlayButtonAction = () =>
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
        };

        _hqHud.LivesButtonAction = () =>
        {
            SerialGameData.Apply(sgd=>
            {
                if (sgd.getHearts()!=Constants.MaxHearts)
                {
                    _rewarded.Show();
                }
                
                
                
            },andSave : false);

        };
        
        _timer.addEvent(1f, () =>
        {
            var sgd = SerialGameData.Load();
            var h = sgd.getHeartsAndRem();
            _hqHud.UpdateInfo(sgd.coins,h.hearts,h.rem);
        },true);


        _shop = new Shop(_mainCamera);


        _hqHud.ShopButtonAction = () =>
        {
            //_shop.AddToVisualElement(_uiDocument.rootVisualElement);
            _shop.AddToVisualElement(_safeElement);
        };

        _hqHud.AchiButtonAction = () =>
        {
            if (!Constants.ReleaseVersion)
            {
                SerialGameData.ResetSaves();
            

                SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }
            
            
          
        };


        _shop.BgButtonAction = () =>
        {
            _shop.RemoveFromVisualElement(_safeElement);
            UpdateStuff();

        };
        UpdateStuff();
        
        
        if (!Constants.SupressAd)
        {
            _rewarded = AdHandler.RequestRewardedAd();
            _rewarded.OnUserEarnedReward += OnAdReward;
            _rewarded.OnAdClosed += OnAdClosed;
        }
        
    }


    private void OnAdClosed(object sender, EventArgs e)
    {
        _rewarded.Destroy();
        _rewarded = AdHandler.RequestRewardedAd();
        _rewarded.OnUserEarnedReward += OnAdReward;
        _rewarded.OnAdClosed += OnAdClosed;
        
    }
    
    private void OnAdReward(object sender, Reward r)
    {
        Debug.Log("reward");
        SerialGameData.Apply(sgd =>
        {
            sgd.changeHearts(1);
        });
        
        UpdateStuff();

        
    }

    private void UpdateStuff()
    {
        var furnTheWitch = _random.NextDouble() < Constants.FurnitureChance;
        
        SerialGameData.Apply(sgd =>
        {
            var l = new List<FurnitureScript>();
            foreach (var furnitureScript in Furnitures)
            {
                var isActive = sgd.activeFurnitures.Contains(furnitureScript.ID);
                furnitureScript.gameObject.SetActive(isActive);
                
                if (furnitureScript.ThereIsMonster && isActive)
                {
                    furnitureScript.MonsterEnabled(false);
                    l.Add(furnitureScript);
                }
            }

            
            if (l.Count > 0&& furnTheWitch)
            {
                MonsterPrefabScript.gameObject.SetActive(false);
                if (l.Count > 0)
                {
                    var t = _random.Next(l.Count );
                    l[t].MonsterEnabled(true);
                }
            }
            else
            {
                MonsterPrefabScript.gameObject.SetActive(true);
                MonsterPrefabScript.UpdateDress(sgd.lineup);
            }
            
            // ShopExclamation
            // sgd.activeFurnitures
            try
            {
                var f = ShopItems.ShopItemsArray.First(x => !sgd.activeFurnitures.Contains(x.ID));
                _hqHud.ShopExclamation(f.Price <= sgd.coins);
            }
            catch (InvalidOperationException e)
            {
                
            }
            

        });
    }

    // Update is called once per frame
    void Update()
    {
        
        _timer.Update(Time.deltaTime);


        if (Input.touches.Length > 0)
        {
            var thisTouch = Input.touches[0];
            if (thisTouch.deltaPosition.x != 0)
            {
                var cx2 = _mainCamera.Camera.transform.position.x -1f * _mainCamera.screen2wpWidth(thisTouch.deltaPosition.x);
                var halfWidth = _mainCamera.screen2wpWidth(Screen.width) * 0.5f;
                Tools.MutatePosition(_mainCamera.Camera.transform, x:  Math.Clamp(cx2, bg.bounds.min.x+halfWidth, bg.bounds.max.x-halfWidth));
            }
            
        }



    }
    
    
}
