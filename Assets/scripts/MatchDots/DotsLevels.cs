using System.Collections.Generic;
using System.Data;

namespace MatchDots
{
    public static class DotsLevels
    {
        public static readonly DotsLevelsInfo[] Levels = new DotsLevelsInfo[]
        {
            new DotsLevelsInfo(4,5,10,4, new []{1000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,10), new DotsTarget(-1,25)}),
            new DotsLevelsInfo(4,5,10,4, new []{2000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,16),new DotsTarget(1,16)})
        };

    }


    public class DotsLevelsInfo
    {
        public int Rows;
        public int Cols;
        public int DotTypes;
        public int Moves;
        public int[] Seeds;
        public DotsObstacle[] Obstacles;
        public DotsTarget[] Targets;
        
        
        public DotsLevelsInfo(int rows, int cols, int moves, int dotTypes, int[] seeds, DotsObstacle[] obstacles, DotsTarget[] targets)
        {
            Rows = rows;
            Cols = cols;
            DotTypes = dotTypes;
            Obstacles = obstacles;
            Targets = targets;
            Moves = moves;
            Seeds = seeds;

        }
        
        
        
        
    }

    public struct DotsObstacle
    {
        public int Type; //1: gap
        public int R;
        public int C;

        public DotsObstacle(int r, int c, int type)
        {
            Type = type;
            R = r;
            C = c;
        }
    }
    
    public struct DotsTarget
    {
        public int Type; //0->.. colours, negative are specials
        public int Amount;

        public DotsTarget(int type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
    
}