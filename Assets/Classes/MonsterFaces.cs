using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Classes
{
    public class MonsterFaces
    {
        public VisualElement Portrait
        {
            get; private set;
        }

        private Sprite[] _spriteList;
        private MonsterMood _activeMood = MonsterMood.Happy;
        private Image _im;
        public MonsterFaces()
        {
            Portrait = new VisualElement();

            var addresses = new string[] {"ui/emotions/smile","ui/emotions/sad","ui/emotions/surprised" };
            _spriteList = (from a in addresses select Resources.Load<Sprite>(a)).ToArray();

            _im = new Image();
            _im.style.width = _spriteList[0].rect.width;
            _im.style.height = _spriteList[0].rect.width;
            _im.sprite = _spriteList[0];
            Portrait.style.width = _spriteList[0].rect.width;
            Portrait.style.height= _spriteList[0].rect.height;
            Portrait.style.position = Position.Absolute;
            
            Portrait.Add(_im);

        }

        public void ChangeMood(MonsterMood m)
        {
            _activeMood = m;
            _im.sprite = _spriteList[(int) m];
        }
        
        
    }
    
    public enum MonsterMood{Happy, Sad, Excited}
}