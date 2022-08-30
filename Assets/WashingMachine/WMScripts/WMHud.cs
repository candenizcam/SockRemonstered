using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using JetBrains.Annotations;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

public class WMHud
{
    private VisualElement _topBar;
    private VisualElement _bottomBar;
    private VisualElement _sockHolder;
    private Label _moveCounter;
    private Vector2[] _smallSockSpots;
    private List<ButtonClickable> _buttons = new List<ButtonClickable>();
    private float[] _pixelPoints = {0, 246, 462, 168, 924, 246};
    private float[] _polynomial;
    private int[] _amounts;
    
    private MonsterFaces _monsterFaces;
    private float scale;
    public Action SettingsButtonAction = () => {};
    
    public WMHud(WMLayout wmLayout)
    {
        var topBarRect = wmLayout.topBarRect();
        var bottomBarRect = wmLayout.bottomBarRect();

        scale = wmLayout.Scale;
        
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

        var w = 382f;
        var h = 284f;
        //380 veya daha büyük

        var unsafeHeight = (Screen.height - Screen.safeArea.yMax);

        if (unsafeHeight + 284 > 380)
        {
            h = 284;
        }
        else
        {
            h = 380 - unsafeHeight;
        }
        
        
        
        _monsterFaces = new MonsterFaces(scale);
        _monsterFaces.Portrait.style.right = 306*scale- _monsterFaces.ScaledWidth;
        _monsterFaces.Portrait.style.top = (h-64f)*scale - _monsterFaces.ScaledHeight;
        _topBar.Add(_monsterFaces.Portrait);

        var moveBg = new Image();
        moveBg.style.position = Position.Absolute;
        moveBg.sprite = Resources.Load<Sprite>("ui/moveframebg");
        moveBg.style.width = scale*moveBg.sprite.rect.width;
        moveBg.style.height = scale*moveBg.sprite.rect.height;
        moveBg.style.right = (w -moveBg.sprite.rect.width)*scale;
        moveBg.style.top = (h-moveBg.sprite.rect.height)*scale;
        _topBar.Add(moveBg);
        
        var moveTop = new Image();
        moveTop.style.position = Position.Absolute;
        moveTop.sprite = Resources.Load<Sprite>("ui/moveframetop");
        moveTop.style.width = scale*moveTop.sprite.rect.width;
        moveTop.style.height = scale*moveTop.sprite.rect.height;
        moveTop.style.right = (w -moveTop.sprite.rect.width)*scale;
        moveTop.style.top = (h-moveTop.sprite.rect.height)*scale;
        _topBar.Add(moveTop);
        
        _moveCounter = new Label();
        _moveCounter.style.position = Position.Absolute;
        _moveCounter.style.left = 0f;
        _moveCounter.style.bottom = 0f;
        _moveCounter.style.width = scale * 180f;
        _moveCounter.style.height = scale * 180f;
        _moveCounter.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));
        _moveCounter.style.fontSize = 86f * scale;
        _moveCounter.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
        _moveCounter.style.color = Constants.GameColours[11];
        moveBg.Add(_moveCounter);
        
        
        

        _bottomBar = new VisualElement();
        
        _bottomBar.style.position = Position.Absolute;
        _bottomBar.style.left = bottomBarRect.x;
        _bottomBar.style.bottom = bottomBarRect.y;
        _bottomBar.style.height = bottomBarRect.height;
        _bottomBar.style.width = bottomBarRect.width;
        //_bottomBar.style.backgroundColor = new Color(1f,0f,0f,0.6f);

        
        var settingsButton = new ButtonClickable(scale,"ui/buttons/Pause",Color.gray,() =>
        {
            settingsButtonFunction();
        });
        settingsButton.style.position = Position.Absolute;
        settingsButton.style.left = 32f*scale;
        settingsButton.style.bottom = 32f*scale;
        

        _buttons.Add(settingsButton);
        _bottomBar.Add(settingsButton);

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
            n.style.width = scale*n.sprite.rect.width;
            n.style.height = scale*n.sprite.rect.height;
            n.style.left = (x - n.sprite.rect.width/2f)*scale;
            n.style.bottom = (y- n.sprite.rect.height)*scale;
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
    
    public void setVisible(bool b)
    {
        _topBar.visible = b;
        _bottomBar.visible = b;
    }

    public void updateInfo([CanBeNull] string  moveLeft = null, MonsterMood? monsterMood = null)
    {
        if (moveLeft != null)
        {
            _moveCounter.text = moveLeft;
        }

        if (monsterMood != null)
        {
            _monsterFaces.ChangeMood((MonsterMood)monsterMood);
        }
        
    }
    
    void settingsButtonFunction()
    {
        SettingsButtonAction();
    }
    
    public void Update()
    {
        foreach (var buttonClickable in _buttons)
        {
            buttonClickable.Update();
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
