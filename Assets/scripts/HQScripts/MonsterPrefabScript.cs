using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class MonsterPrefabScript : MonoBehaviour
{
    public List<MonsterPieceScript> Sprites;

    private string[] _dressArray =   {"Body_Raw","LArm_Raw","RArm_Raw","LLeg_Raw","RLeg_Raw" };
    
    // Start is called before the first frame update
    void Awake()
    {
        UpdateDress();
    }


    public void UpdateDress(string[] a)
    {
        _dressArray = a;
        Debug.Log($"im updating {_dressArray[0]}, {_dressArray[1]}, {_dressArray[2]}, {_dressArray[3]}, {_dressArray[4]}");
        UpdateDress();
    }
    
    public void UpdateDress(string body=null, string leftArm = null , string rightArm= null, string leftLeg= null, string rightLeg= null)
    {
        _dressArray = new[]
        {
            body ?? _dressArray[0], leftArm ?? _dressArray[0], rightArm ?? _dressArray[0], leftLeg ?? _dressArray[0],
            rightLeg ?? _dressArray[0]
        };
        UpdateDress();
    }

    private void UpdateDress()
    {
        foreach (var monsterPieceScript in Sprites)
        {
            monsterPieceScript.gameObject.SetActive(_dressArray.Contains(monsterPieceScript.ID)); 
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
