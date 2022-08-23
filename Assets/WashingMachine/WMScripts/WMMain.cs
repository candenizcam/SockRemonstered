using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using Classes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;
using Random = System.Random;

public class WMMain : MonoBehaviour
{
    //public List<Prefab> 
    
    public GameObject playfield;
    public WMHud hudScript;
    public GameObject wheel;
    
    
    private float sockSpawnTime = 1f;
    private float sockSpawnTimer = 0f;
    private string _baseSockPrefabPath = "prefabs/BaseSockPrefab";
    private List<SockPrefabScript> _activeSocks = new List<SockPrefabScript>();
    private WMLayout mainCamera;

    private float _playfieldTopBorder = 0.9f;
    private float _playfieldBottomBorder = 0.05f;
    private UIDocument _uiDocument;
    private WMHud _wmHud;
    private float _baseWheelHeight;
    private float _wheelSpeed = 5f;
    void Awake()
    {

        
        
        mainCamera = new WMLayout(Camera.main);

        var r = mainCamera.playfieldRect();
        
        playfield.transform.localScale = new Vector3(r.width,r.height,0f);

        playfield.transform.position = new Vector3(r.center.x,r.center.y,100f);
        wheel.transform.position = new Vector3(r.center.x,r.center.y,90f);

        var wsr = wheel.GetComponent<SpriteRenderer>();
        //Debug.Log($"size: { wsr.sprite.vertices[0]}, { wsr.sprite.vertices[1]}");
        _baseWheelHeight = wsr.sprite.vertices[0].y * 2;
        wsr.size = new Vector2(r.width, r.height * 4f);
        //wheel.transform.localScale = new Vector3(r.width,r.height*2f,0f);
        
        _uiDocument = gameObject.GetComponent<UIDocument>();
        _uiDocument.panelSettings.referenceResolution = new Vector2Int(Screen.width, Screen.height);
        _uiDocument.panelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;

        _wmHud = new WMHud(mainCamera.topBarRect, mainCamera.bottomBarRect);
        _wmHud.AddToVisualElement(_uiDocument.rootVisualElement);
    }

    
    private void HandleTouch()
    {
        var thisTurnTouches1 = Array
            .FindAll(Input.touches, x => x.phase == TouchPhase.Ended);

        var r = mainCamera.playfieldRect(CameraTools.CoordSystem.Screen);

        var thisTurnTouches2 =
            Array.FindAll(thisTurnTouches1, x => r.Contains(new Vector2(x.position.x, x.position.y)));


        var thisTurnTouches = thisTurnTouches2.ToList();
        
        if (thisTurnTouches.Count <= 0) return; // if there is no touch to be handled, break
        //Debug.Log("exists");
        foreach (var sockPrefabScript in _activeSocks) // look at each sock with each touch, remove touches and socks that collide starting from the top sock and first touch
        {
            var touched = false;
            for (var i = 0; i < thisTurnTouches.Count; i++)
            {
                var worldPoint = mainCamera.Camera.ScreenToWorldPoint(thisTurnTouches[i].position);
                if (!sockPrefabScript.Collides(worldPoint)) continue; // if no collision continue
                touched = true;
                thisTurnTouches.RemoveAt(i);
                break;
            }
            if (!touched) continue;
            Destroy(sockPrefabScript.gameObject);
            sockPrefabScript.ToBeDestroyed = true;
        }
    }
    
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        Tools.TranslatePosition(wheel,y: Time.deltaTime*_wheelSpeed );
        if (wheel.transform.position.y > _baseWheelHeight)
        {
            Tools.MutatePosition(wheel,y: wheel.transform.position.y%_baseWheelHeight);
        }

        sockSpawnTimer += Time.deltaTime;
        if (sockSpawnTimer > sockSpawnTime)
        {
            sockSpawnTimer %= sockSpawnTime;
            
            if (_activeSocks.Count < 5)
            {
                _activeSocks.Add(generateSock(Tools.RandomVector2(y: _playfieldTopBorder)));
                ArrangeActiveSocks();
            }
            
        }
        
        foreach (var sockPrefabScript in _activeSocks)
        {
            sockPrefabScript.MoveDownTime();
            var p = mainCamera.Camera.WorldToViewportPoint(sockPrefabScript.gameObject.transform.position);
            if (p.y < _playfieldBottomBorder)
            {
                sockPrefabScript.ToBeDestroyed = true;
                Destroy(sockPrefabScript.gameObject);
            }
        }
        
        
        
        HandleTouch();
       
        _activeSocks.RemoveAll(x => x.ToBeDestroyed);
        ArrangeActiveSocks();
        
    }

    /** This script generates a sock
     * position is viewPort based, which is 0-1
     */
    SockPrefabScript generateSock(Vector2 viewPortPos)
    {
        var bsp = Instantiate(Resources.Load(_baseSockPrefabPath));
        var sps = bsp.GetComponent <SockPrefabScript> ();
        sps.gameObject.transform.position = Tools.MutateVector3(mainCamera.Camera.ViewportToWorldPoint(viewPortPos), z : 1f);


        return sps;
    }

    void ArrangeActiveSocks()
    {
        for (int i = 0; i < _activeSocks.Count; i++)
        {
            Tools.MutatePosition(_activeSocks[i].gameObject, z: (float) i+10f);
        }
    }
}
