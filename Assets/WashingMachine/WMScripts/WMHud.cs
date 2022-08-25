using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

public class WMHud
{
    private VisualElement _topBar;
    private VisualElement _bottomBar;
    private VisualElement _sockHolder;
    private Vector2[] _smallSockSpots;

    private float[] _pixelPoints = {0, 250, 410, 175, 920, 250};
    private float[] _polynomial;
    private int[] _amounts;
    
    public WMHud(Rect topBarRect, Rect bottomBarRect)
    {

        _polynomial = Tools.CalcParabolaVertex(_pixelPoints[0], _pixelPoints[1], _pixelPoints[2], _pixelPoints[3],
            _pixelPoints[4], _pixelPoints[5]);
        _topBar = new VisualElement();

        _topBar.style.position = Position.Absolute;
        _topBar.style.left = topBarRect.x;
        _topBar.style.bottom = topBarRect.y;
        _topBar.style.height = topBarRect.height;
        _topBar.style.width = topBarRect.width;
        //_topBar.style.backgroundColor = new Color(1f,1f,0f,0.6f);

        var pins = new Image();
        pins.sprite = Resources.Load<Sprite>("ui/pins");
        pins.style.position = Position.Absolute;
        pins.style.left = 0f;
        pins.style.bottom = 0f;
        pins.style.width = pins.sprite.rect.width;
        pins.style.height = pins.sprite.rect.height;
        //pins.style.backgroundColor = Color.blue;
        _topBar.Add(pins);

        _sockHolder = new VisualElement();
        _sockHolder.style.position = Position.Absolute;
        _sockHolder.style.left = 0f;
        _sockHolder.style.bottom = 0f;
        
        _topBar.Add(_sockHolder);

        /*
        for (int i = 0; i < 6; i++)
        {
            var x = i * _pixelPoints[4]/5f;
            var y = _polynomial[0] * x * x + _polynomial[1] * x + _polynomial[2];
            Debug.Log($"x: {x}, y: {y}");
            var n = new Image();
            n.sprite = Resources.Load<Sprite>("ui/pins");
            n.style.position = Position.Absolute;
            n.style.left = x;
            n.style.bottom = y;
            n.style.width = 50f;
            n.style.height = 50f;
            n.style.backgroundColor = Color.blue;
            _topBar.Add(n);
            
        }
        */
        
        /*
        var thisStar = new Image();
        thisStar.sprite = Resources.Load<Sprite>(  "<path>"  );
        thisStar.style.position = Position.Absolute;
        thisStar.style.right = (j) * (starWidth+10) +20;
        thisStar.style.bottom = outerHeight*0.2f;
        thisStar.style.width = starWidth;
        thisStar.style.height = starWidth * 2;
        outer.Add(thisStar);
        */
        

        _bottomBar = new VisualElement();
        
        _bottomBar.style.position = Position.Absolute;
        _bottomBar.style.left = bottomBarRect.x;
        _bottomBar.style.bottom = bottomBarRect.y;
        _bottomBar.style.height = bottomBarRect.height;
        _bottomBar.style.width = bottomBarRect.width;
        //_bottomBar.style.backgroundColor = new Color(1f,0f,0f,0.6f);
    }


    public void AddToVisualElement(VisualElement ve)
    {
        ve.Add(_topBar);
        ve.Add(_bottomBar);
    }
    
    public void RemoveFromVisualElement(VisualElement ve)
    {
        ve.Remove(_topBar);
        ve.Remove(_bottomBar);
    }



    public void generateSocks(string[] address)
    {
        var totalSize = address.Length + 1;
        var xStep = _pixelPoints[4]/totalSize;
        _sockHolder.Clear();
        for (int i = 0; i < address.Length; i++)
        {
            var x = (i+1)*xStep;
            var y = _polynomial[0] * x * x + _polynomial[1] * x + _polynomial[2];
            var n = new Image();
            n.sprite = Resources.Load<Sprite>(address[i]);
            n.style.position = Position.Absolute;
            n.style.left = x - n.sprite.rect.width/2f;
            n.style.bottom = y- n.sprite.rect.height;
            _sockHolder.Add(n);
        }
        
        for (int i = 0; i < address.Length; i++)
        {
            var x = (i+1)*xStep-15f;
            var y = _polynomial[0] * x * x + _polynomial[1] * x + _polynomial[2]+15f;
            var n = new Image();
            n.sprite = Resources.Load<Sprite>(address[i]);
            n.style.position = Position.Absolute;
            n.style.left = x - n.sprite.rect.width/2f;
            n.style.bottom = y- n.sprite.rect.height;
            n.visible = false;
            _sockHolder.Add(n);
        }
        Debug.Log(_sockHolder.childCount);
        
    }

    public void adjustSocks(int[] amount)
    {
        for (int i = 0; i < amount.Length; i++)
        {
            var sh = _sockHolder[i];
            
            switch(amount[i]) 
            {
                case 0:
                    ((Image)sh).tintColor = Color.gray;
                    _sockHolder[i + amount.Length].visible = false;
                    break;
                case 1:
                    // code block
                    ((Image)sh).tintColor = Color.white;
                    _sockHolder[i + amount.Length].visible = false;
                    break;
                default:
                    ((Image)sh).tintColor = Color.gray;
                    _sockHolder[i + amount.Length].visible = true;
                    // code block
                    break;
            }

            

        }
    }
    
    /*
    public void adjustSocks(string[] address, int[] amount)
    {
        var totalSize = address.Length + 2;
        var xStep = _pixelPoints[4]/totalSize;
        var xHolder = xStep;
        //_sockHolder.Clear();
        
        _sockHolder.RemoveAt();
        for (int i = 0; i < address.Length; i++)
        {
            
            var x = i * _pixelPoints[4]/5f;
            var y = _polynomial[0] * x * x + _polynomial[1] * x + _polynomial[2];
            Debug.Log($"x: {x}, y: {y}");
            var n = new Image();
            n.sprite = Resources.Load<Sprite>("ui/pins");
            n.style.position = Position.Absolute;
            n.style.left = x;
            n.style.bottom = y;
            n.style.width = 50f;
            n.style.height = 50f;
            n.style.backgroundColor = Color.blue;
            _sockHolder.Add(n);
            
        }
        

    }

*/
        //var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
    
}
