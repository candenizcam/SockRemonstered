using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class WMMain : MonoBehaviour
{
    //public List<Prefab> 
    private string _baseSockPrefabPath = "prefabs/BaseSockPrefab";
    private List<SockPrefabScript> _activeSocks = new List<SockPrefabScript>();
    private Camera mainCamera;
    
    void Awake()
    {
        mainCamera = Camera.main;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        var thisTurnTouches = Array.FindAll(Input.touches, x => x.phase == TouchPhase.Ended).ToList();
        
        if (_activeSocks.Count < 5)
        {
            _activeSocks.Add(generateSock(Tools.RandomVector2()));
            ArrangeActiveSocks();
        }
        

        if (thisTurnTouches.Count > 0)
        {
            //Debug.Log("something is touched");
            foreach (var sockPrefabScript in _activeSocks)
            {
                var touched = false;
                for (int i = 0; i < thisTurnTouches.Count; i++)
                {
                    
                    var worldPoint = mainCamera.ScreenToWorldPoint(thisTurnTouches[i].position);
                    
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

            _activeSocks.RemoveAll(x => x.ToBeDestroyed);
            ArrangeActiveSocks();
        }
       
    }

    /** This script generates a sock
     * position is viewPort based, which is 0-1
     */
    SockPrefabScript generateSock(Vector2 viewPortPos)
    {
        var bsp = Instantiate(Resources.Load(_baseSockPrefabPath));
        var sps = bsp.GetComponent <SockPrefabScript> ();
        sps.gameObject.transform.position = Tools.MutateVector3(mainCamera.ViewportToWorldPoint(viewPortPos), z : 1f);


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
