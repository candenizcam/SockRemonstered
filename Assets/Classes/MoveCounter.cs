using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    
    
    
    public class MoveCounter
    {
        
        private Label _moveCounter;
        public Image MoveBg;

        public MoveCounter(float scale)
        {
            MoveBg = new Image();
            MoveBg.style.position = Position.Absolute;
            MoveBg.sprite = Resources.Load<Sprite>("ui/moveframebg");
            MoveBg.style.width = scale*MoveBg.sprite.rect.width;
            MoveBg.style.height = scale*MoveBg.sprite.rect.height;
            //MoveBg.style.right = (w -MoveBg.sprite.rect.width)*scale;
            //MoveBg.style.top = (h-MoveBg.sprite.rect.height)*scale;
        
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
            MoveBg.Add(_moveCounter);
        
            var moveTop = new Image();
            moveTop.style.position = Position.Absolute;
            moveTop.sprite = Resources.Load<Sprite>("ui/moveframetop");
            moveTop.style.width = scale*moveTop.sprite.rect.width;
            moveTop.style.height = scale*moveTop.sprite.rect.height;
            moveTop.style.right = 0f;
            moveTop.style.top = 0f;
            MoveBg.Add(moveTop);
        }

        public void UpdateMoves(string s)
        {
            _moveCounter.text = s;
        }
        
    }
}