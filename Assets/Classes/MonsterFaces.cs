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

        public float ScaledWidth;
        public float ScaledHeight;
        private Sprite[] _armsList;
        private Sprite[] _headList;
        private Sprite[] _hornsList;
        private Image[] _labels;
        private MonsterMood _activeMood = MonsterMood.Happy;
        private Image _arm;
        private Image _horn;
        private Image _head;
        private float _scale;
        public MonsterFaces(float scale)
        {
            _scale = scale;
            Portrait = new VisualElement();
            

            var emotions = new string[] {"Smile", "Sad", "Surprised"};
            _armsList = (from a in emotions select Resources.Load<Sprite>($"ui/emotions/{a}-Arms")).ToArray();
            _headList = (from a in emotions select Resources.Load<Sprite>($"ui/emotions/{a}-Head")).ToArray();
            _hornsList = (from a in emotions select Resources.Load<Sprite>($"ui/emotions/{a}-Horns")).ToArray();
            
            
            var frameBg = new Image();
            frameBg.sprite = Resources.Load<Sprite>("ui/Frame-Back");
            

            var frameFg = new Image();
            frameFg.sprite = Resources.Load<Sprite>("ui/Frame-Top");
            
            var width = frameBg.sprite.rect.width * _scale;
            var height = frameBg.sprite.rect.width*_scale;
            ScaledWidth = width;
            ScaledHeight = height;
            frameBg.style.width = width;
            frameBg.style.height = height;
            frameBg.style.position = Position.Absolute;
            frameBg.style.left = 0f;
            frameBg.style.bottom = 0f;
            frameFg.style.height = height;
            frameFg.style.width = width;
            frameFg.style.position = Position.Absolute;
            frameFg.style.left = 0f;
            frameFg.style.bottom = 0f;

            Portrait.style.width = width;
            Portrait.style.height= height;
            Portrait.style.position = Position.Absolute;
            
            
            var body = new Image();
            body.style.width = width;
            body.style.height = height;
            body.sprite = Resources.Load<Sprite>($"ui/emotions/BackBody");
            
            body.style.position = Position.Absolute;
            body.style.left = 0f;
            body.style.bottom = 0f;
            
            
            Portrait.Add(frameBg);
            Portrait.Add(body);   
            Portrait.Add(frameFg);

            _labels = new Image[3];
            for (int i = 0; i < 3; i++)
            {
                var a = new Image();
                a.style.width = width;
                a.style.height = height;
                a.style.position = Position.Absolute;
                a.style.left = 0f;
                a.style.bottom = 0f;
                _labels[i] = a;
                Portrait.Add(a);                
            }
            
            
            
            
            

        }

        public void ChangeMood(MonsterMood m)
        {
            _activeMood = m;
            _labels[0].sprite = _armsList[(int) m];
            _labels[1].sprite = _headList[(int) m];
            _labels[2].sprite = _hornsList[(int) m];
        }
        
        
    }
    
    public enum MonsterMood{Happy, Sad, Excited}
}