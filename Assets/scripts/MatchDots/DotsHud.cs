using System.Collections.Generic;
using Classes;
using UnityEngine;
using UnityEngine.UIElements;

namespace MatchDots
{
    public class DotsHud: GameHud
    {
        private VisualElement _targetHolder;
        
        
        public DotsHud(GameLayout gl) : base(gl)
        {
            Initialize(gl);
        }
        
        public override void Initialize(GameLayout gl)
        {
            base.Initialize(gl);

            _targetHolder = new VisualElement();
            _targetHolder.style.position = Position.Absolute;
            _targetHolder.style.left = 25f * gl.Scale;
            _targetHolder.style.top = 30f * gl.Scale;
            _targetHolder.style.width = 800f * gl.Scale;
            _targetHolder.style.bottom = 30f * gl.Scale;
            _targetHolder.style.flexDirection = FlexDirection.Row;
            //_targetHolder.style.backgroundColor = Color.blue;

            //_topBar.style.backgroundColor = Color.green;
            _topBar.Add(_targetHolder);


            /*
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
            */
        }

        public void UpdateTargets(List<DotsTarget> dt)
        {
            _targetHolder.Clear();
            foreach (var dotsTarget in dt)
            {
                var frame = new VisualElement();
                frame.style.alignItems = Align.Center;
                var number = new Label();
                //number.style.bottom = 565f * scale;
                //number.style.position = Position.Absolute;
                number.style.fontSize = 48f * scale;
                //number.style.left = 0f;
                // number.style.width = bg.sprite.rect.width*scale;
                number.style.unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft);
                number.text = $"{dotsTarget.Amount}";
                number.style.color = Constants.GetDotColours(dotsTarget.Type);
                

                var image = new Image();
                image.sprite = Resources.Load<Sprite>(Constants.GetDotPath(dotsTarget.Type));
                image.style.height = 48f * scale;
                image.style.width = 48f * scale;
                frame.Add(image);
                
                frame.Add(number);
                /*
                 
                 bg.sprite = Resources.Load<Sprite>("ui/betweenbg");
                bg.style.position = Position.Absolute;                
                bg.style.left = (Screen.width - bg.sprite.rect.width) * 0.5f*scale;
                bg.style.top = (Screen.height - bg.sprite.rect.height) * 0.5f*scale;
                bg.style.width = bg.sprite.rect.width*scale;
                bg.style.height = bg.sprite.rect.height*scale;
                bg.style.unityFontDefinition = new StyleFontDefinition((Font)Resources.Load("fonts/funkyfont"));
                 */
                
                _targetHolder.Add(frame);
            }
        }
        
        public DotsHud() : base()
        {
            
        }
    }
}