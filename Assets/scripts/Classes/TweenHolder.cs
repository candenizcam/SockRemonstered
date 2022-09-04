using System;
using System.Collections.Generic;
using UnityEngine.PlayerLoop;

namespace Classes
{
    public class TweenHolder
    {
        private List<Tween> _tweens =  new List<Tween>();
        
        public TweenHolder()
        {
            
            
        }


        void newTween(Tween t)
        {
            _tweens.Add(t);
        }
        
        public void newTween(float totalTime, Action<float> tweenAction, Action endAction = null, int repeat = 1, List<string> tags = null )
        {
            var t = new Tween(totalTime, tweenAction ,endAction: endAction ??= () => {},repeat:repeat, tags:tags);
            _tweens.Add(t);
            //_tweens.Add(t);
        }
        
        public void Update(float dt)
        {
            foreach (var tween in _tweens)
            {
                tween.Update(dt);
            }

            _tweens.RemoveAll(x => x.Repeat == 0);
        }

        
    }
}