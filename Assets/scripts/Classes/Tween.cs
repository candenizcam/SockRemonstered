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
        private Action _startAction;
        private Action _endAction;
        public List<string> Tags;
        private bool firstCalled = false;
        public int Repeat { get; private set; } = 1;
        
        public Tween(float totalTime, Action<float> tweenAction, Action startAction =null, Action endAction =null, int repeat=1, List<string> tags = null)
        {
            _totalTime = totalTime;
            _tweenAction = tweenAction;
            _currentTime = totalTime;
            Repeat = repeat;
            _startAction = startAction ??= () => { };
            _endAction = endAction ??= () => { };
            Tags = tags??= new List<string>();
        }

        public void Update(float deltaTime)
        {
            if (!firstCalled)
            {
                _startAction();
                firstCalled = true;
            }
            if (Repeat == 0)
            {
                //_tweenAction(0f);
                return;
            };
            
            _tweenAction(1f - _currentTime / _totalTime);
            _currentTime -= deltaTime;
            if (!(_currentTime < 0)) return;
            
            _currentTime += _totalTime;
            if (Repeat > 0)
            {
                Repeat -= 1;
                if (Repeat == 0)
                {
                    _endAction();                
                }
            }
        }
    }
    
    
}