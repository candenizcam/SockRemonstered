using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Classes;
using HQScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HQMainScript : MonoBehaviour
{
    private System.Random _random;
    private HQLayout _mainCamera;
    private UIDocument _uiDocument;
    private Timer _timer;
    private float _timeHolder;
    private HQHud _hqHud;
    private Shop _shop;
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
        
        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
        
        _mainCamera = new HQLayout(Camera.main,190f,200f);

        _hqHud = new HQHud(_mainCamera);
        _hqHud.AddToVisualElement(_uiDocument.rootVisualElement);
        var h = sgd.getHeartsAndRem();
        _hqHud.UpdateInfo(sgd.coins,h.hearts,h.rem);
        _hqHud.PlayButtonAction = () =>
        {
            var sgd = SerialGameData.LoadOrGenerate();
            var nl = Constants.GetNextLevel(sgd.nextLevel);
            SceneManager.LoadScene(nl.SceneName, LoadSceneMode.Single);
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
            _shop.AddToVisualElement(_uiDocument.rootVisualElement);
        };

        _hqHud.AchiButtonAction = () =>
        {
            SerialGameData.ResetSaves();
            

            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
          
        };


        _shop.BgButtonAction = () =>
        {
            _shop.RemoveFromVisualElement(_uiDocument.rootVisualElement);
            UpdateStuff();

        };
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
            
            
           
            
            
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
        _timer.Update(Time.deltaTime);
        _hqHud.Update();
        _shop.Update();


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
