using System;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

public class WMHud : GameHud
{
 
    private VisualElement _sockHolder;
    private Image _hand;
    //private Label _moveCounter;
    private Vector2[] _smallSockSpots;
    
    private float[] _pixelPoints = {0, 246+100, 462, 168+100, 924, 246+100};
    private float[] _polynomial;
    private int[] _amounts;
    private float[] _handTimer;
    private float[] _handYTargets;
    private float _handTime = 1f;
    private float _handPickTime = 0.4f;
    private float _handMoveHeight;
    private VisualElement _frame;
    private VisualElement _pins;
    private float _frameWidth;
    private float _frameHeight;
    private int _amount=1;
    protected VisualElement _fullScreen; // including unsafe
    
    
    public WMHud(): base()
    {
        //Initialize();
    }

    public override void Initialize(float topHeight = 220f, float bottomHeight = 200f)
    {
        base.Initialize(topHeight,bottomHeight);
        
        var s = Resources.Load<Sprite>("ui/clothesline");
        var w = Constants.UiWidth - _monsterFaces.ScaledWidth;
        var h = s.rect.height/s.rect.width*w;
        
        
        _handMoveHeight = 220f * 1.5f;
        _pixelPoints = new float[] {0f, 10f, w*0.5f, 10f+h,  w, 10f};
        _polynomial = Tools.CalcParabolaVertex(0f, 10f, w*0.5f, 10f+h, w, 10f);
        

        


        _frame = new VisualElement()
        {
            style =
            {
                backgroundImage = new StyleBackground(Resources.Load<Sprite>("ui/buttons/money_bg_2")),
                position = Position.Absolute,
                left = 0f,
                top = 0f,
                width = w,
                height = h+200f,
            }
        };
        
        
        
        _fullScreen = new VisualElement();
        _fullScreen.style.position = Position.Absolute;
        _fullScreen.style.top = -Constants.UnsafeTopUi;
        _fullScreen.style.bottom = -Constants.UiHeight;
        _fullScreen.style.left = -Constants.UnsafeLeftUi;
        _fullScreen.style.right = -Constants.UnsafeRightUi - _monsterFaces.ScaledWidth;
        _fullScreen.style.backgroundColor = new Color(0.1f,0.1f,0.1f,0.9f);
        _topBar.Add(_fullScreen);
        
        _topBar.Add(_frame);
        _frameWidth = w;
        _frameHeight = h;
        _pins = new Image
        {
            sprite = s,
            style =
            {
                position = Position.Absolute,
                left = 0f,
                top = 15f,
                width = w,
                height = h,
                
            }
        };
        
        _frame.Add(_pins);
        
        //_topBar.Add(pins);

        _sockHolder = new VisualElement
        {
            style =
            {
                position = Position.Absolute,
                left = 0f,
                top = 0f,
                
            }
        };

        _frame.Add(_sockHolder);

        
        
        
        

        _hand = new Image();
        _hand.sprite = Resources.Load<Sprite>("ui/hand");
        _hand.style.width = _hand.sprite.rect.width * scale;
        _hand.style.height = _hand.sprite.rect.height * scale;
        _hand.style.position = Position.Absolute;
    }
    
    
    public void ClearBg()
    {
        _topBar.Remove(_fullScreen);
        //_fullScreen.style.backgroundColor = Color.clear;
        for (int i = 0; i < _amount-1; i++)
        {
            var im = ((Image) _sockHolder[i]);
            im.tintColor = Color.gray;
            
        }
    }
        
    public void StartAnimation(float alpha)
    {
        var w = _frameWidth*alpha + (Constants.UiWidth+100f)*(1f-alpha);
        _frame.style.top = 0f * alpha + (Constants.UiHeight*0.5f - 220f - 180f) * (1f - alpha);
        _frame.style.width = w;
        _frame.style.left = 0f * alpha - 50f*(1f-alpha);
        _frame.style.height = (_frameHeight+200f) * alpha + 360f * (1f - alpha);
        _pins.style.width = w;
        var xStep = w/_amount;

        for (int i = 0; i < _amount-1; i++)
        {
            //var l = visualElement.style.left;
            var x = (i+1)*xStep;
            var im = ((Image) _sockHolder[i]);
            im.style.left = (x - im.sprite.rect.width/2f)*scale;
            im.tintColor = Color.white;
            
        }
        
        
    }
    
    
    public void generateSocks(string[] address)
    {
        _handTimer = new float[address.Length];
        _handYTargets = new float[address.Length];
        _amount = address.Length +1;
        var xStep = _pixelPoints[4]/_amount;
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
            n.style.top = y;//(y- n.sprite.rect.height)*scale;
            _handYTargets[i] = (y- n.sprite.rect.height - _frameHeight - Constants.UnsafeTopUi)*scale;
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
            n.style.top = y;//(y- n.sprite.rect.height)*scale;
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
                
                var normalDifference = 1f - (_handTimer[i]) / Math.Max(_handPickTime,0.05f);
                _hand.style.bottom = _handYTargets[i] +normalDifference * _handMoveHeight;
            }
            

            if (_handTimer[i] <= _handPickTime && emptyMark) 
            {   
                ((Image) _sockHolder[i]).tintColor = Color.gray;
                _sockHolder[i + _handTimer.Length].visible = false;
            }
            
        }


    }

}
