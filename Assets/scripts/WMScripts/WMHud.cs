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
    
    private float[] _pixelPoints = {0, 246, 462, 168, 924, 246};
    private float[] _polynomial;
    private int[] _amounts;
    private float[] _handTimer;
    private float[] _handYTargets;
    private float _handTime = 1f;
    private float _handPickTime = 0.4f;
    private float _handMoveHeight;
    
    
    
    public WMHud(): base()
    {
        //Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        
        
        _handMoveHeight = 220f * 1.5f;
        _polynomial = Tools.CalcParabolaVertex(_pixelPoints[0], _pixelPoints[1], _pixelPoints[2], _pixelPoints[3],
            _pixelPoints[4], _pixelPoints[5]);
        var s = Resources.Load<Sprite>("ui/clothesline");
        var pins = new Image
        {
            sprite = s,
            style =
            {
                position = Position.Absolute,
                left = 0f,
                bottom = 168f,
                width = s.rect.width,
                height = s.rect.height
            }
        };
        _topBar.Add(pins);

        _sockHolder = new VisualElement
        {
            style =
            {
                position = Position.Absolute,
                left = 0f,
                bottom = 0f
            }
        };

        _topBar.Add(_sockHolder);

        

        _hand = new Image();
        _hand.sprite = Resources.Load<Sprite>("ui/hand");
        _hand.style.width = _hand.sprite.rect.width * scale;
        _hand.style.height = _hand.sprite.rect.height * scale;
        _hand.style.position = Position.Absolute;
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
            

            if (_handTimer[i] <= _handPickTime && emptyMark) 
            {   
                ((Image) _sockHolder[i]).tintColor = Color.gray;
                _sockHolder[i + _handTimer.Length].visible = false;
            }
            
        }


    }

}
