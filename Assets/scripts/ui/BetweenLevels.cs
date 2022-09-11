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
        private List<ButtonClickable> _buttons = new List<ButtonClickable>();
        public bool Active;
        private Label _bigText;
        private Label _smallText;
        private ButtonClickable _bigButton;
        private VisualElement _pointDisplay;
        private Label _points;
        private MonsterFaces _monsterFaces;
        public Action OnCross;
        public Action OnBigButton;
        
        public BetweenLevels()
        {
            
            _betweenElement = new VisualElement();
            _betweenElement.style.width = Constants.UiWidth;
            _betweenElement.style.height = Constants.UiHeight;
            _betweenElement.style.backgroundColor = new Color(0.102f, 0.024f, 0.071f,0.84f);
            
            
            var bg = new Image();
            bg.sprite = Resources.Load<Sprite>("ui/betweenbg");
            bg.style.position = Position.Absolute;
            
            bg.style.left = (Constants.UiWidth - bg.sprite.rect.width) * 0.5f*scale;
            bg.style.top = (Constants.UiHeight - bg.sprite.rect.height) * 0.5f*scale;
            bg.style.width = bg.sprite.rect.width*scale;
            bg.style.height = bg.sprite.rect.height*scale;
            bg.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));

            _bigText = new Label();
            _smallText = new Label();

            _smallText.style.bottom = 458f * scale;
            _smallText.style.position = Position.Absolute;
            _smallText.style.left = 0f;
            _smallText.style.width = bg.sprite.rect.width*scale;
            _smallText.style.fontSize = 64f * scale;
            _smallText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _smallText.text = "Level failed!";
            _smallText.style.color = Constants.GameColours[11];
            
            
            _bigText.style.bottom = 565f * scale;
            _bigText.style.position = Position.Absolute;
            _bigText.style.fontSize = 109f * scale;
            _bigText.style.left = 0f;
            _bigText.style.width = bg.sprite.rect.width*scale;
            _bigText.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleCenter);
            _bigText.text = "LEVEL 320";
            _bigText.style.color = Constants.GameColours[11];


            _pointDisplay = new VisualElement();

            _pointDisplay.style.position = Position.Absolute;
            _pointDisplay.style.left = 0f;
            _pointDisplay.style.width = bg.sprite.rect.width*scale;
            _pointDisplay.style.bottom = 291f * scale;
            _pointDisplay.style.flexDirection = FlexDirection.Row;
            _pointDisplay.style.alignContent = new StyleEnum<Align>(Align.Center);
            _pointDisplay.style.justifyContent = new StyleEnum<Justify>(Justify.Center);
            _pointDisplay.style.flexBasis = bg.sprite.rect.width * scale;
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
            bg.Add(_pointDisplay);
            
            
            
            _monsterFaces = new MonsterFaces(scale);
            _monsterFaces.Portrait.style.left = 260f*scale;
            _monsterFaces.Portrait.style.bottom = (805f) * scale;
            bg.Add(_monsterFaces.Portrait);
            
            
            
            
            
            
            _bigButton = new ButtonClickable(() =>
            {
                bigButton();
            });

                
                
                
                
            var s = Resources.Load<Sprite>("ui/button");

            _bigButton.style.backgroundImage = new StyleBackground(s);
            _bigButton.style.position = Position.Absolute;
            _bigButton.style.bottom = 63*scale;
            _bigButton.style.left = (bg.sprite.rect.width*scale - s.rect.width*scale) * 0.5f;
            _bigButton.style.width= s.rect.width*scale;
            _bigButton.style.height= s.rect.height*scale;
            _bigButton.style.backgroundColor = Color.clear;
            _bigButton.style.color = Constants.GameColours[11];
            _bigButton.style.fontSize = 80f*scale;
            _bigButton.text = "CONTINUE";
            
            _buttons.Add(_bigButton);
            _bigButton.onTouchDown = () =>
            {
                _bigButton.style.unityBackgroundImageTintColor = Color.gray;
            };
            
            _bigButton.onTouchUp = () =>
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
            
            
            _buttons.Add(smallButton);
            
            bg.Add(smallButton);
            bg.Add(_bigButton);
            bg.Add(_smallText);
            bg.Add(_bigText);
            _betweenElement.Add(bg);
        }
        
        public void AddToVisualElement(VisualElement ve)
        {
            ve.Add(_betweenElement);
        }

        public void setVisible(bool b)
        {
            _betweenElement.visible = b;
            Active = b;
        }


        public void Update()
        {
            foreach (var buttonClickable in _buttons)
            {
                buttonClickable.Update();
            }
        }

        public void UpdateSmallText(string s)
        {
            _smallText.text = s;
        }

        public void UpdateInfo(bool levelWon, string bigText, string smallText, string pointsText, string buttonText)
        {
            if (levelWon)
            {
                _bigText.style.bottom = 565f * scale;
                _smallText.style.bottom = 458f * scale;
                _monsterFaces.Portrait.style.bottom = (805f) * scale;
                _monsterFaces.ChangeMood(MonsterMood.Happy);
                _bigButton.text = buttonText;
                _pointDisplay.visible = true;
            }
            else
            {
                _bigText.style.bottom =  419f* scale;
                _smallText.style.bottom = 312f * scale;
                _monsterFaces.Portrait.style.bottom = (606f) * scale;
                _monsterFaces.ChangeMood(MonsterMood.Sad);
                _bigButton.text = buttonText;
                _pointDisplay.visible = false;
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