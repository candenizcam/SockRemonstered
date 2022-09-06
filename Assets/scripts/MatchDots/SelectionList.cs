using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine.SocialPlatforms.Impl;

namespace MatchDots
{
    public class SelectionList
    {
        private List<DotsPrefabScript> _selectionList = new List<DotsPrefabScript>();
        public List<DotsPrefabScript> Selections => _selectionList;


        public (int type, int amount) Score()
        {
            return (LineType(), _selectionList.Count);

        }
        
        public bool IsLatestPick(DotsPrefabScript p)
        {
            if (_selectionList.Count > 0)
            {
                return _selectionList.Last() == p;
            }
            else
            {
                return false;
            }
            
        }

        public bool ListContains(DotsPrefabScript p)
        {
            return _selectionList.Contains(p);
        }
        
        public bool IsAdjacent(DotsPrefabScript p)
        {
            if (_selectionList.Count > 0)
            {
                var  l =_selectionList.Last();

                var dy = l.Row - p.Row;
                var dx = l.Column - p.Column;
                if (Math.Abs(dy) + Math.Abs(dx) == 1)
                {
                    return true;
                }else if (Math.Abs(dy) == 1 && Math.Abs(dx)==1 && Constants.MatchDiagonal )
                {
                    return true;
                }

            }
            return false;
        }
        

        public int LineType()
        {
            if (_selectionList.Count > 0)
            {
                return _selectionList.Last().DotType;
            }
            else
            {
                return -1;
            }
        }

        public void AddDot(DotsPrefabScript dps)
        {
            var i = _selectionList.FindIndex(x=>x==dps);
            if (i == -1)
            {
                _selectionList.Add(dps);
            }
            else
            {
                foreach (var dotsPrefabScript in _selectionList)
                {
                    dotsPrefabScript.TweenEffect(1f);
                }

                _selectionList.RemoveRange(i+1,_selectionList.Count-i-1);
            }
            
        }

        public void Clear()
        {
            foreach (var dotsPrefabScript in _selectionList)
            {
                dotsPrefabScript.TweenEffect(1f);
            }
            _selectionList.Clear();
        }

        public string MembersString()
        {
            var s = "";
            foreach (var dotsPrefabScript in _selectionList)
            {
                s += $"r: {dotsPrefabScript.Row}, c: {dotsPrefabScript.Column}, t: {dotsPrefabScript.DotType}, ";
            }

            return s;
        }
        
    }
    
    
}