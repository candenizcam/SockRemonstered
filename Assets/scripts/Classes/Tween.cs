using System;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public class Tween
    {
        private float _totalTime;
        private float _currentTime;
        private Action<float> _tweenAction;
        public List<string> Tags;
        public int Repeat { get; private set; } = 1;
        
        public Tween(float totalTime, Action<float> tweenAction, int repeat=1, List<string> tags = null)
        {
            _totalTime = totalTime;
            _tweenAction = tweenAction;
            _currentTime = totalTime;
            Repeat = repeat;
            Tags = tags??= new List<string>();
        }

        public void Update(float deltaTime)
        {
            if (Repeat == 0)
            {
                _tweenAction(0f);
                return;
            };
            
            _tweenAction(_currentTime / _totalTime);
            _currentTime -= deltaTime;
            if (!(_currentTime < 0)) return;
            
            _currentTime += _totalTime;
            if (Repeat > 0) Repeat -= 1;
        }
    }
    
    
}