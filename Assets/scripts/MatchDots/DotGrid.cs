using System.Linq;

namespace MatchDots
{
    public class DotGrid
    {
        private int _rows;
        private int _cols;
        private DotsObstacle[] _obstacle;
        private int _fillSize = 0;
        public DotGrid(DotsLevelsInfo dli)
        {
            
            _rows =dli.Rows;
            _cols = dli.Cols;
            _obstacle = dli.Obstacles;


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
            
            var r = 0;
            for (int i = 0; i < _rows; i++)
            {
                if (thisColObs.Any(x => x.R == i + 1))
                {
                    
                }
                else
                {
                    r += 1;
                }
            }

            return r;

        }

        public bool GapSpot(int r, int c)
        {
            return _obstacle.Any(x => x.C == c && x.R == r && x.Type == -1);
        }
        
        //public 
    }
}