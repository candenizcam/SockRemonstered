using System.Collections.Generic;
using System.Data;
using Classes;

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

        public static readonly TutorialFrame[] Tutorial = new TutorialFrame[]
        {
            new TutorialFrame("TutorialDots/Pause - 1", .4f),
            new TutorialFrame("TutorialDots/Pause - 2", .4f),
            new TutorialFrame("TutorialDots/Pause - 3", .4f),
            new TutorialFrame("TutorialDots/Pause - 4", .4f),
            new TutorialFrame("TutorialDots/Pause - 5", .4f),
            new TutorialFrame("TutorialDots/Pause - 6", .4f),
            new TutorialFrame("TutorialDots/Pause - 7", .4f),
            new TutorialFrame("TutorialDots/Pause - 8", .4f),
            new TutorialFrame("TutorialDots/Pause - 9", .4f),
            new TutorialFrame("TutorialDots/Pause - 10", .4f),
            new TutorialFrame("TutorialDots/Pause - 11", .4f),
            new TutorialFrame("TutorialDots/Pause - 12", .4f),
            new TutorialFrame("TutorialDots/Pause - 13", .4f),
            new TutorialFrame("TutorialDots/Pause - 14", .4f),
            new TutorialFrame("TutorialDots/Pause - 15", .4f),
            new TutorialFrame("TutorialDots/Pause - 16", .4f),
            new TutorialFrame("TutorialDots/Pause - 17", .4f),
            new TutorialFrame("TutorialDots/Pause - 18", .4f),
            new TutorialFrame("TutorialDots/Pause - 19", .4f),
            new TutorialFrame("TutorialDots/Pause - 20", .4f),
            new TutorialFrame("TutorialDots/Pause - 21", .4f),
            new TutorialFrame("TutorialDots/Pause - 22", .4f),
            new TutorialFrame("TutorialDots/Pause - 23", .4f),
            new TutorialFrame("TutorialDots/Pause - 24", .4f),
            new TutorialFrame("TutorialDots/Pause - 25", .4f),
            new TutorialFrame("TutorialDots/Pause - 26", .4f),
            new TutorialFrame("TutorialDots/Pause - 27", .4f),
            new TutorialFrame("TutorialDots/Pause - 28", .4f),
            new TutorialFrame("TutorialDots/Pause - 29", .4f)
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