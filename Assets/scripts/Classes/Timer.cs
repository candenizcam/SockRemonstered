using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private List<TimerEvent> _timerEvents = new List<TimerEvent>();
    public Timer()
    {
        
    }

    public void addEvent(float time, Action action, bool repeat=false)
    {
        _timerEvents.Add(new TimerEvent(time, action, repeat));
            
    }
    
    
    public void Update(float t)
    {
        
        foreach (var timerEvent in _timerEvents)
        {
            timerEvent.ActiveTime -= t;
            if (!(timerEvent.ActiveTime < 0)) continue;
            timerEvent.Action();
            if (timerEvent.Repeat)
            {
                timerEvent.ActiveTime = timerEvent.Time;
            }
            else
            {
                timerEvent.Done = true;
            }
        }

        _timerEvents.RemoveAll(x => x.Done);
    }


    class TimerEvent
    {
        public readonly float Time;
        public float ActiveTime;
        public Action Action;

        public bool Done = false;
        public bool Repeat;
        public TimerEvent(float time, Action action, bool repeat=false)
        {
            Time = time;
            ActiveTime = time;
            Repeat = repeat;
            Action = action;
        }

        
    }
}
