using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class TestMain : MonoBehaviour
{
    public GameObject visual;
    public GameObject otherVisual;
    public GameObject otherOtherVisual; 
    public GameObject otherOtherOtherVisual;

    public GameObject otherVisual5;
    // Start is called before the first frame update
    void Start()
    {
        var r = new Random(6);

        var s = "";
        for (int i = 0; i < 500; i++)
        {
            var r2 = r.Next(0,4);
            s += $"{r2}";
        }
        

        Debug.Log(s);

        //Debug.Log($"screen, width {Screen.width}, height {Screen.height}");

        //Debug.Log($"{Screen.resolutions}, {Screen.currentResolution}");

        //Debug.Log($"camera main target width: {Camera.main.scaledPixelWidth}, height: {Camera.main.scaledPixelHeight}");

        //Debug.Log($"os: {Camera.main.orthographicSize}");

        //Camera.main.orthographicSize = Screen.height / 200f;

        /*
        // Debug.Log($"camera main rect width: {Camera.main.rect.width}, height: {Camera.main.rect.height}"); 1,1
        
        Debug.Log($"camera main pixel width: {Camera.main.pixelWidth}, height: {Camera.main.pixelHeight}");
        
        //Debug.Log($"camera main target width: {Camera.main.targetTexture.width}, height: {Camera.main.targetTexture.height}");
        
        Debug.Log($"camera main target width: {Camera.main.scaledPixelWidth}, height: {Camera.main.scaledPixelHeight}");
        
        // Debug.Log($"Camera.main.transform.lossyScale x: {Camera.main.transform.lossyScale.x}, y: {Camera.main.transform.lossyScale.y}"); 1,1
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");

        var gc1 = visual.GetComponent<SpriteRenderer>();
        rendererStats("1",gc1);
       
        var gc5 = otherVisual5.GetComponent<SpriteRenderer>();
        rendererStats("5",gc5);

        Debug.Log($"orth size {Camera.main.orthographicSize}");
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");
        
        //Camera.main.orthographicSize *= 2f;

        
        var gc12 = visual.GetComponent<SpriteRenderer>();
        rendererStats("12",gc12);
       
        var gc52 = otherVisual5.GetComponent<SpriteRenderer>();
        rendererStats("52",gc52);
        
        Debug.Log($"orth size {Camera.main.orthographicSize}");
        
        Debug.Log($"v2 screen world {Camera.main.ViewportToScreenPoint(new Vector3(1,1,0))} {Camera.main.ViewportToWorldPoint(new Vector3(1,1,0))}");

        visual.transform.localScale = new Vector3(2f, 2f, 1f);
        rendererStats("12",gc12);
       
        otherVisual5.transform.localScale = new Vector3(2f, 2f, 1f);
        rendererStats("52",gc52);
        */
    }

    void rendererStats(string pre, SpriteRenderer sr)
    {
        var bounds = sr.sprite.bounds;
        Debug.Log($"{pre} size .x: {sr.size.x}, .y: {sr.size.y}, {sr.transform.lossyScale.x}, {sr.transform.lossyScale.y}");
        
        
        //Debug.Log($"{pre} sprite rect width: {sr.sprite.rect.width}, height: {sr.sprite.rect.height}");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
