using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;
using UnityEngine.Rendering;

namespace MatchDots
{
    public class DotsScoreboard
    {
        private List<DotsTarget> _targets;
        private List<int> _removedTypes =new List<int>();
        public int MoveCounter { get; private set; } = 0;
        private int _totalMoves;
        private List<DotsTarget> _rems;
        private int _extraCoins = 0;
        
        public DotsScoreboard(DotsLevelsInfo dli)
        {
            _targets = dli.Targets.ToList();
            _totalMoves = dli.Moves;
            updateRems();
        }


        public (string movesLeft, MonsterMood mood) GetHudInfo()
        {
            var remainers = _totalMoves - MoveCounter;
            var tm = $"{Math.Max(0, remainers)}";
            var rest = _rems.Sum(x => x.Amount);
            var mood = rest > remainers * 5 ? MonsterMood.Sad : (rest > remainers * 3 ? MonsterMood.Excited : MonsterMood.Happy);
            return (tm, mood);
        }

        public bool GameWon()
        {
            return _rems.Sum(x => x.Amount)==0;
        }
        
        public bool GameLost()
        {
            return MoveCounter>= _totalMoves;
        }
        
        public (int number, string text)  GetLevelPoints()
        {
            var remainers = _totalMoves - MoveCounter;
            return (remainers * 10 + _extraCoins*5 + 20,$"  {remainers * 10 + _extraCoins*5 + 20}");
        }

        public void AddToRemoved(List<int> r, int movesChange = 1)
        {
            _extraCoins += Math.Max(r.Count - 5, 0)*2;
            _removedTypes.AddRange(r);
            MoveCounter += movesChange;
            updateRems();
            
        }


        private void updateRems()
        {
            var rems = new List<DotsTarget>();

            var simpleTargets = _targets.Where(x => x.Type >= 0);
            var spareTargets = _targets.Where(x => x.Type == -1);
            
            
            
            var typeList = simpleTargets.Select(x => x.Type).ToList();
            var amountList = simpleTargets.Select(x => x.Amount).ToList();
            var counts = typeList.Select(x => _removedTypes.Count(y => x == y)).ToList();
            var spares = _removedTypes.Count;

            for (int i = 0; i < amountList.Count; i++)
            {
                var d = amountList[i] - counts[i];
                DotsTarget dt;
                if (d < 0)
                {
                    dt = new DotsTarget(typeList[i], 0);
                    spares -= amountList[i];
                    //spares += (int)Math.Abs(d);
                }
                else
                {
                    dt = new DotsTarget(typeList[i], d);
                    spares -= counts[i];
                }
                rems.Add(dt);
            }
            

            if (spareTargets.Any())
            {
                var a = spareTargets.First().Amount;
                
                rems.Add(new DotsTarget(-1,Math.Max(a-spares,0) ));
            }

            _rems = rems;
        } 
        
        public List<DotsTarget>  GetRems()
        {
            return _rems;
        }
        
        
    }
}