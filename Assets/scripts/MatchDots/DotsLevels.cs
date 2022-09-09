using System.Collections.Generic;
using System.Data;

namespace MatchDots
{
    public static class DotsLevels
    {
        public static readonly DotsLevelsInfo[] Levels = new DotsLevelsInfo[]
        {
            new DotsLevelsInfo(4,5,10,4, new []{1000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,20)}),
            new DotsLevelsInfo(4,5,10,4, new []{2000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(2,16),new DotsTarget(3,16)}),
            new DotsLevelsInfo(4,5,15,5, new []{3000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(2,10), new DotsTarget(3,10),new DotsTarget(4,10)}),
            new DotsLevelsInfo(4,5,15,6, new []{4003},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,8),new DotsTarget(2,8),new DotsTarget(-1,16)})
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