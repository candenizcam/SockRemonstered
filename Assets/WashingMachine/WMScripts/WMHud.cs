using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Classes;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

public class WMHud : GameHud
{
 
    private VisualElement _sockHolder;
    private Image _hand;
    //private Label _moveCounter;
    private Vector2[] _smallSockSpots;
    
    private float[] _pixelPoints = {0, 246, 462, 168, 924, 246};
    private float[] _polynomial;
    private int[] _amounts;
    private float[] _handTimer;
    private float[] _handYTargets;
    private float _handTime = 1f;
    private float _handPickTime = 0.4f;
    private float _handMoveHeight;
    
    
    
    
    public WMHud(WMLayout wmLayout): base(wmLayout)
    {
        _handMoveHeight = topBarRect.height*1.5f;
        _polynomial = Tools.CalcParabolaVertex(_pixelPoints[0], _pixelPoints[1], _pixelPoints[2], _pixelPoints[3],
            _pixelPoints[4], _pixelPoints[5]);
        //_topBar.style.backgroundColor = new Color(1f,1f,0f,0.6f);

        var pins = new Image();
        pins.sprite = Resources.Load<Sprite>("ui/clothesline");
        pins.style.position = Position.Absolute;
        pins.style.left = 0f;
        pins.style.bottom = 168f;
        pins.style.width = pins.sprite.rect.width*scale;
        pins.style.height = pins.sprite.rect.height*scale;
        //pins.style.backgroundColor = Color.blue;
        _topBar.Add(pins);

        _sockHolder = new VisualElement();
        _sockHolder.style.position = Position.Absolute;
        _sockHolder.style.left = 0f;
        _sockHolder.style.bottom = 0f;
        
        _topBar.Add(_sockHolder);

        

        _hand = new Image();
        _hand.sprite = Resources.Load<Sprite>("ui/hand");
        _hand.style.width = _hand.sprite.rect.width * scale;
        _hand.style.height = _hand.sprite.rect.height * scale;
        _hand.style.position = Position.Absolute;
        //_sockHolder.Add(_hand);
    }
    
    
    public void generateSocks(string[] address)
    {
        _handTimer = new float[address.Length];
        _handYTargets = new float[address.Length];
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
            n.style.width = scale*n.sprite.rect.width;
            n.style.height = scale*n.sprite.rect.height;
            n.style.left = (x - n.sprite.rect.width/2f)*scale;
            n.style.bottom = (y- n.sprite.rect.height)*scale;
            _handYTargets[i] = (y- n.sprite.rect.height)*scale;
            _sockHolder.Add(n);
        }
        
        for (int i = 0; i < address.Length; i++)
        {
            var x = (i+1)*xStep-15f;
            var y = _polynomial[0] * x * x + _polynomial[1] * x + _polynomial[2]+15f;
            var n = new Image();
            n.sprite = Resources.Load<Sprite>(address[i]);
            n.style.position = Position.Absolute;
            n.style.width = scale*n.sprite.rect.width;
            n.style.height = scale*n.sprite.rect.height;
            n.style.left = (x - n.sprite.rect.width/2f)*scale;
            n.style.bottom = (y- n.sprite.rect.height)*scale;
            n.visible = false;
            _sockHolder.Add(n);
        }
    }

    public void adjustSocks(int[] amount)
    {
        for (int i = 0; i < amount.Length; i++)
        {
            var sh = _sockHolder[i];
            if (_handTimer[i] > _handPickTime)
            {
                ((Image)sh).tintColor = Color.gray;
                _sockHolder[i + amount.Length].visible = true;
                
                continue;
            }
            
            
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

    public void HandSock(int index, int number)
    {
        
        _handTimer = new float[_handTimer.Length];
        _handTimer[index] = _handTime;

        _hand.style.left = _sockHolder[index].style.left;
        if (!_sockHolder.Contains(_hand))
        {
            _sockHolder.Add(_hand);
        }
        

    }
    
    

    
    
    
    
    public void Update()
    {
        for (var i = 0; i < _handTimer.Length; i++)
        {
            if (_handTimer[i]<=0f) continue;

            bool emptyMark = _handTimer[i] > _handPickTime;

            _handTimer[i] -= Time.deltaTime;

            if (_handTimer[i] > _handPickTime)
            {
                var normalDifference = (_handTimer[i] - _handPickTime) / (_handTime - _handPickTime);
                _hand.style.bottom = _handYTargets[i] + normalDifference * _handMoveHeight;
            }
            else if (_handTimer[i] <= 0f)
            {
                _sockHolder.Remove(_hand);
            }else
            {
                
                var normalDifference = 1f - (_handTimer[i]) / (_handPickTime);
                _hand.style.bottom = _handYTargets[i] +normalDifference * _handMoveHeight;
            }
            
            //_hand.style.bottom = _handYTargets[i] + 

            if (_handTimer[i] <= _handPickTime && emptyMark) 
            {
                
                
                ((Image) _sockHolder[i]).tintColor = Color.gray;
                _sockHolder[i + _handTimer.Length].visible = false;
            }
            
        }
        
        base.Update();


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
