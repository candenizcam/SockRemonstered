using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MatchDots
{
    public class DotGrid
    {
        private int _rows;
        private int _cols;
        private DotsObstacle[] _obstacle;
        
        private int _fillSize = 0;
        private List<DotsPrefabScript> _dotsList = new List<DotsPrefabScript>();
        public List<DotsPrefabScript> DotsList => _dotsList;
        public DotsObstacle[] Obstacles => _obstacle;
        
        public DotGrid(DotsLevelsInfo dli)
        {
            
            _rows =dli.Rows;
            _cols = dli.Cols;
            _obstacle = dli.Obstacles;

       
            
            
            

        }



        public void FillTheDotsList( List<DotsPrefabScript> l)
        {
            _dotsList = l;
        }


        public int ActiveMemberNo()
        {
            return _rows * _cols - _obstacle.Count(x => x.Type == 1);
        }

        /** Returns the row that dist from top corresponds to for a row
         * this is useful with obstacles and such
         */
        public int GetRowFromColTop(int col, int dist)
        {
            var thisColObs = _obstacle.Where(x => x.C == col && x.Type==1);
            if (!thisColObs.Any())
            {
                return dist;
            }
            
            var r =dist;
            for (int i = 0; i < dist; i++)
            {
                if (thisColObs.Any(x => x.R == i + 1))
                {
                    r += 1;
                    
                }
                else
                {
                    
                }
            }

            return r;

        }

        public bool GapSpot(int r, int c)
        {
            return _obstacle.Any(x => x.C == c && x.R == r && x.Type == 1);
        }

        public void GetBlobs()
        {
            var ar = Get2DDotArray();
            var blobList = new List<List<DotsPrefabScript>>();

            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    var l1 = new List<DotsPrefabScript>();
                    l1.Add(ar[i,j]);
                    RecursiveBlober(ar,l1, i,j);
                    if(l1.Count<2) continue;
                    if (blobList.Any(x => x.Contains(l1[0]))) continue;
                    blobList.Add(l1);
                }

            }

            // look at blobs to find legal moves some day
        }

        public bool AnyLegalMoves()
        {
            var ar = Get2DDotArray();
            
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    var l1 = new List<DotsPrefabScript>();
                    if(ar[i,j]==null) continue;
                    l1.Add(ar[i,j]);
                    RecursiveBlober(ar,l1, i,j);
                    if (l1.Count > 2)
                    {
                        
                        return true;
                    }
                }   
            }

            return false;
        }

        void RecursiveBlober(DotsPrefabScript[,] ar, List<DotsPrefabScript> l, int r, int c)
        {
            for (int i = -1; i < 2; i++)
            {
                var newR = r + i;
                if(newR<0 || newR>=_rows) continue;
                for (int j = -1; j < 2; j++)
                {
                    if(i==0 && j==0) continue;
                    var newC = c + j;
                    if(newC<0 || newC>=_cols) continue;

                    var t = ar[newR, newC];
                    if (t == null) continue;
                    if (t.DotType == l.Last().DotType && !l.Contains(t))
                    {
                        l.Add(t);
                        RecursiveBlober(ar,l,newR,newC);
                    }

                }
            }
        }
        
        
        DotsPrefabScript[,] Get2DDotArray()
        {
            var ar = new DotsPrefabScript[_rows, _cols];
            foreach (var dotsPrefabScript in DotsList)
            {
                ar[dotsPrefabScript.Row - 1, dotsPrefabScript.Column - 1] = dotsPrefabScript;
            }
            return ar;
        }
        //public 
        
        public int[] ColNeeds()
        {
            var stabilized = DotsList.Where(x => x.InTheRightPlace);
            
            
            
            
                var a = new int[_cols];
            for (int i = 0; i < _cols; i++)
            {
                a[i] = _rows - stabilized.Count(x => x.Column == i+1) - _obstacle.Count(x => x.C == i+1);
            }
            return a;
        }
    }
}