using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WashingMachine.WMScripts;

namespace Classes
{
    public class BetweenLevels
    {
        private VisualElement _betweenElement;
        private float scale = 1f;
        
        public bool Active;
        private Label _bigText;
        private Label _smallText;
        private ButtonClickable _bigButton;
        private VisualElement _pointDisplay;
        private Label _points;
        private MonsterFaces _monsterFaces;
        private VisualElement _bg;
        public Action OnCross;
        public Action OnBigButton;
        private StyleBackground[] _bgSprites = {};
        
        public BetweenLevels()
        {
            
            _betweenElement = new VisualElement();
            _betweenElement.style.width = Constants.UiWidth;
            _betweenElement.style.height = Constants.UiHeight;
            _betweenElement.style.backgroundColor = new Color(0.102f, 0.024f, 0.071f,0.84f);

            _bgSprites = new[]
            {
                new StyleBackground(Resources.Load<Sprite>("ui/betweenbg")),
                new StyleBackground(Resources.Load<Sprite>("ui/betweenbg_fail2"))
            };
            var bgW = _bgSprites[0].value.sprite.rect.width;
            var bgH = _bgSprites[0].value.sprite.rect.height;
            
            _bg = new VisualElement();
            //bg.sprite = Resources.Load<Sprite>("ui/betweenbg");
            _bg.style.backgroundImage = _bgSprites[0];
            _bg.style.position = Position.Absolute;
            
            _bg.style.left = (Constants.UiWidth - bgW) * 0.5f*scale;
            _bg.style.top = (Constants.UiHeight - bgH) * 0.5f*scale;
            _bg.style.width = bgW;
            _bg.style.height = bgH;
            _bg.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));

            _bigText = new Label();
            _smallText = new Label();

            _smallText.style.bottom = 458f * scale;
            _smallText.style.position = Position.Absolute;
            _smallText.style.left = 0f;
            _smallText.style.width = bgW;
            _smallText.style.fontSize = 109f * scale;
            _smallText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _smallText.text = "Level failed!";
            _smallText.style.color = Constants.GameColours[11];
            
            
            _bigText.style.bottom = 565f * scale;
            _bigText.style.position = Position.Absolute;
            _bigText.style.fontSize = 64 * scale;
            _bigText.style.left = 0f;
            _bigText.style.width = bgW;
            _bigText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _bigText.text = "LEVEL 320";
            _bigText.style.color = Constants.GameColours[11];


            _pointDisplay = new VisualElement();

            _pointDisplay.style.position = Position.Absolute;
            _pointDisplay.style.left = 0f;
            _pointDisplay.style.width = bgW;
            _pointDisplay.style.bottom = 291f * scale;
            _pointDisplay.style.flexDirection = FlexDirection.Row;
            _pointDisplay.style.alignContent = new StyleEnum<Align>(Align.Center);
            _pointDisplay.style.justifyContent = new StyleEnum<Justify>(Justify.Center);
            _pointDisplay.style.flexBasis = bgW;
            _pointDisplay.style.alignItems = new StyleEnum<Align>(Align.Center);
            
            var coin = new Image();
            coin.sprite = Resources.Load<Sprite>("ui/buttons/coin");
            coin.style.width = 100f* scale;
            coin.style.height = 100f* scale;
            _pointDisplay.Add(coin);

            _points = new Label();
            _points.style.fontSize = 109*scale;
            _points.text = "  8888";
            _points.style.color = Constants.GameColours[11];
            _pointDisplay.Add(_points);
            _bg.Add(_pointDisplay);
            
            
            
            _monsterFaces = new MonsterFaces(scale);
            _monsterFaces.Portrait.style.left = 260f*scale;
            _monsterFaces.Portrait.style.bottom = (805f) * scale;
            _bg.Add(_monsterFaces.Portrait);
            
            
            
            var s = Resources.Load<Sprite>("ui/button");

            _bigButton = new ButtonClickable(bigButton)
            {
                style =
                {
                    backgroundImage = new StyleBackground(s),
                    position = Position.Absolute,
                    bottom = 63*scale,
                    left = (bgW - s.rect.width*scale) * 0.5f,
                    width = s.rect.width*scale,
                    height = s.rect.height*scale,
                    backgroundColor = Color.clear,
                    color = Constants.GameColours[11],
                    fontSize = 80f*scale
                },
                Text = "CONTINUE"
            };


            _bigButton.OnTouchDown = () =>
            {
                _bigButton.style.unityBackgroundImageTintColor = Color.gray;
            };
            
            _bigButton.OnTouchUp = () =>
            {
                _bigButton.style.unityBackgroundImageTintColor = Color.white;
            };
            
            var smallButton = new ButtonClickable(scale,"ui/x",Color.gray,() =>
            {
                cross();
            });
            
            smallButton.style.position = Position.Absolute;
            smallButton.style.top = 50f*scale;
            smallButton.style.right = 50f*scale;
            
            
            _bg.Add(smallButton);
            _bg.Add(_bigButton);
            _bg.Add(_smallText);
            _bg.Add(_bigText);
            _betweenElement.Add(_bg);
        }
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_betweenElement);
        }

        public void RemoveFromVisualElement(VisualElement ve)
        {
            ve.Remove(_betweenElement);
        }

        public void setVisible(bool b)
        {
            _betweenElement.visible = b;
            Active = b;
        }



        public void UpdateSmallText(string s)
        {
            _smallText.text = s;
        }

        public void UpdateInfo(bool levelWon, string bigText, string smallText, string pointsText, string buttonText)
        {
            if (levelWon)
            {
                _bigText.style.bottom = 605f * scale;
                _smallText.style.bottom = 458f * scale;
                _monsterFaces.Portrait.style.bottom = (805f) * scale;
                _monsterFaces.ChangeMood(MonsterMood.Happy);
                _bigButton.Text = buttonText;
                _pointDisplay.visible = true;
                _bg.style.backgroundImage = _bgSprites[0];
            }
            else
            {
                _bigText.style.bottom =  459f* scale;
                _smallText.style.bottom = 312f * scale;
                _monsterFaces.Portrait.style.bottom = (606f) * scale;
                _monsterFaces.ChangeMood(MonsterMood.Sad);
                _bigButton.Text = buttonText;
                _pointDisplay.visible = false;
                _bg.style.backgroundImage = _bgSprites[1];
            }
            
            
            _bigText.text = bigText;
            _smallText.text = smallText;
            _points.text = pointsText;
        }
        
        void cross()
        {
            OnCross();

        }

        void bigButton()
        {
            OnBigButton();

        }
        
        
        
    }

    
    
}