using Classes;
using RainScripts;
using UnityEngine;
using UnityEngine.UIElements;

public class RainHud : GameHud
{
    private VisualElement _sockHolder;
    private Vector2[] _smallSockSpots;
    private int[] _amounts;
    private VisualElement _frame;
    private float _frameWidth;
    private float _frameHeight;
    private int _amount=1;
    private VisualElement _fullScreen; // including unsafe

    public RainHud(): base()
    {
        //Initialize();
    }

    public override void Initialize(float topHeight = 220f, float bottomHeight = 200f)
    {
        base.Initialize(topHeight,bottomHeight);
        var s = Resources.Load<Sprite>("ui/clothesline");
        var w = Constants.UiWidth - _monsterFaces.ScaledWidth-20f;
        var h = s.rect.height/s.rect.width*w;
        _frameWidth = w;
        _frameHeight = h;
        //_pixelPoints = new float[] {0f, 10f, w*0.5f, 10f+h,  w, 10f};
        //_polynomial = Tools.CalcParabolaVertex(0f, 10f, w*0.5f, 10f+h, w, 10f);
        
        _fullScreen = new VisualElement
        {
            style =
            {
                position = Position.Absolute,
                top = -Constants.UnsafeTopUi,
                bottom = -Constants.UiHeight,
                left = -Constants.UnsafeLeftUi,
                right = -Constants.UnsafeRightUi - _monsterFaces.ScaledWidth,
                backgroundColor = new Color(0.1f,0.1f,0.1f,0.9f)
            }
        };
        _topBar.Add(_fullScreen);
        
        
        
        
        
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

        _topBar.Add(_frame);
        _sockHolder = new VisualElement
        {
            style =
            {
                position = Position.Absolute,
                left = 0f,
                top = 0f,
                flexDirection = FlexDirection.Row,
                justifyContent = Justify.SpaceAround,
                alignContent = Align.Center,
                
            }
        };
        _sockHolder.StretchToParentSize();
        _frame.Add(_sockHolder);
    }
    
    
    public void ClearBg()
    {
        _topBar.Remove(_fullScreen);
        for (int i = 0; i < _amount-1; i++)
        {
            var im = ((Image) _sockHolder[i]);
            im.tintColor = Color.gray;
        }
    }
        
    public void StartAnimation(float alpha)
    {
        var w = _frameWidth*alpha + (Constants.UiWidth+100f)*(1f-alpha);
        _frame.style.top = 47f * alpha + (Constants.UiHeight*0.5f - 220f - 180f) * (1f - alpha);
        _frame.style.width = w;
        _frame.style.left = 24f * alpha - 50f*(1f-alpha);
        _frame.style.height = (_frameHeight+200f) * alpha + 360f * (1f - alpha);
        
        var xStep = w/_amount;

        for (int i = 0; i < _amount-1; i++)
        {
            var x = (i+1)*xStep;
            var im = ((Image) _sockHolder[i]);
            im.style.left = (x - im.sprite.rect.width/2f)*scale;
            im.tintColor = Color.white;   
        }
    }


    public void GenerateSocks(RainSockInfo[] sockInfos)
    {
        _sockHolder.Clear();
        foreach (var rainSockInfo in sockInfos)
        {
            var no = rainSockInfo.LevelCollect;
            
            var path = RainLevels.RainUISockLookup[rainSockInfo.SockType, rainSockInfo.SockNo];
            var frame = new VisualElement
            {
                style =
                {
                    alignItems = Align.Center,
                    justifyContent = Justify.Center,
                    flexDirection = FlexDirection.Row
                }
            };
            var number = new Label
            {
                style =
                {
                    fontSize = 64f * scale,
                    width = 64f,
                    unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft),
                    color = Constants.GameColours[11]
                },
                text = $" {no}"
            };


            var s = Resources.Load<Sprite>(path);
            
            var image = new VisualElement()
            {
                
                style =
                {
                    //height = 72f,
                    //width = 72f,
                    height = s.rect.height,
                    width = s.rect.width,
                    backgroundImage = new StyleBackground(s),
                }
            };
            frame.Add(image);
            frame.Add(number);
            _sockHolder.Add(frame);
        }
        
        UpdateSocks(sockInfos);
    }


    public void UpdateSocks(RainSockInfo[] sockInfos)
    {
        
        for (var i = 0; i < sockInfos.Length; i++)
        {
            var no = sockInfos[i].LevelCollect;
            ((Label) (_sockHolder[i][1])).text = $" {no}";
        }
    }
    



    public void Update()
    {
        


    }

}
