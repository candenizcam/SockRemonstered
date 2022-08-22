using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Classes;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class WMMain : MonoBehaviour
{
    //public List<Prefab> 
    private string _baseSockPrefabPath = "prefabs/BaseSockPrefab";
    private List<SockPrefabScript> _activeSocks = new List<SockPrefabScript>();
    private CameraTools mainCamera;

    private float _playfieldTopBorder = 0.9f;
    private float _playfieldBottomBorder = 0.05f;
    public GameObject playfield;
    
    private float sockSpawnTime = 1f;
    private float sockSpawnTimer = 0f;
    
    void Awake()
    {
        mainCamera = new CameraTools(Camera.main);

        var r = mainCamera.vp2wRect(0f, _playfieldBottomBorder, 1f, _playfieldTopBorder);
        
        playfield.transform.localScale = new Vector3(r.width,r.height,0f);

        playfield.transform.position = new Vector3(r.center.x,r.center.y,100f);



    }

    // Update is called once per frame
    void Update()
    {


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
        
        
        
        var thisTurnTouches = Array.FindAll(Input.touches, x => x.phase == TouchPhase.Ended).ToList();
        
        
        

        if (thisTurnTouches.Count > 0)
        {
            //Debug.Log("something is touched");
            foreach (var sockPrefabScript in _activeSocks)
            {
                var touched = false;
                for (int i = 0; i < thisTurnTouches.Count; i++)
                {
                    
                    var worldPoint = mainCamera.Camera.ScreenToWorldPoint(thisTurnTouches[i].position);
                    
                    if (sockPrefabScript.Collides(worldPoint))
                    {
                        touched = true;
                        thisTurnTouches.RemoveAt(i);
                        break;
                    }
                    
                }

                if (touched)
                {
                    Destroy(sockPrefabScript.gameObject);
                    sockPrefabScript.ToBeDestroyed = true;
                    
                }
            
            }

            
        }
       
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
