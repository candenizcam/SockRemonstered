using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPieceScript : MonoBehaviour
{
    public string ID;

    [NonSerialized] public SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
