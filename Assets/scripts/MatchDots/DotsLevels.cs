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
            new TutorialFrame("TutorialDots/Pause_1", .4f),
            new TutorialFrame("TutorialDots/Pause_2", .4f),
            new TutorialFrame("TutorialDots/Pause_3", .4f),
            new TutorialFrame("TutorialDots/Pause_4", .4f),
            new TutorialFrame("TutorialDots/Pause_5", .4f),
            new TutorialFrame("TutorialDots/Pause_6", .4f),
            new TutorialFrame("TutorialDots/Pause_7", .4f),
            new TutorialFrame("TutorialDots/Pause_8", .4f),
            new TutorialFrame("TutorialDots/Pause_9", .4f),
            new TutorialFrame("TutorialDots/Pause_10", .4f),
            new TutorialFrame("TutorialDots/Pause_11", .4f),
            new TutorialFrame("TutorialDots/Pause_12", .4f),
            new TutorialFrame("TutorialDots/Pause_13", .4f),
            new TutorialFrame("TutorialDots/Pause_14", .4f),
            new TutorialFrame("TutorialDots/Pause_15", .4f),
            new TutorialFrame("TutorialDots/Pause_16", .4f),
            new TutorialFrame("TutorialDots/Pause_17", .4f),
            new TutorialFrame("TutorialDots/Pause_18", .4f),
            new TutorialFrame("TutorialDots/Pause_19", .4f),
            new TutorialFrame("TutorialDots/Pause_20", .4f),
            new TutorialFrame("TutorialDots/Pause_21", .4f),
            new TutorialFrame("TutorialDots/Pause_22", .4f),
            new TutorialFrame("TutorialDots/Pause_23", .4f),
            new TutorialFrame("TutorialDots/Pause_24", .4f),
            new TutorialFrame("TutorialDots/Pause_25", .4f),
            new TutorialFrame("TutorialDots/Pause_26", .4f),
            new TutorialFrame("TutorialDots/Pause_27", .4f),
            new TutorialFrame("TutorialDots/Pause_28", .4f),
            new TutorialFrame("TutorialDots/Pause_29", .4f)
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