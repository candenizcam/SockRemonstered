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
            new DotsLevelsInfo(4,5,15,6, new []{4003},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,8),new DotsTarget(2,8),new DotsTarget(-1,16)}),
            new DotsLevelsInfo(5,6,12,6, new []{5000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30)}),
            new DotsLevelsInfo(5,6,12,4, new []{6004, 6007},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(2,20)}),
            new DotsLevelsInfo(5,6,12,4, new []{7001, 7002},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,20)}),
            new DotsLevelsInfo(5,6,12,4, new []{8000, 8001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,10), new DotsTarget(1,10)}),
            new DotsLevelsInfo(5,6,12,6, new []{9000, 9002},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,40)}),
            new DotsLevelsInfo(5,6,12,5, new []{10006, 10011},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,10), new DotsTarget(1,10), new DotsTarget(2,10)}),
            new DotsLevelsInfo(5,6,12,5, new []{11015},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(0,16), new DotsTarget(1,16)}),
            new DotsLevelsInfo(5,6,12,5, new []{12002},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,40), new DotsTarget(0,16)}),
            new DotsLevelsInfo(5,6,12,5, new []{13001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(2,20)}),
            new DotsLevelsInfo(5,6,12,4, new []{14014},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,40), new DotsTarget(3,20)}),
            new DotsLevelsInfo(6,7,12,4, new []{15000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50)}),
            new DotsLevelsInfo(6,7,12,5, new []{16000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50)}),
            new DotsLevelsInfo(6,7,15,6, new []{17000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50)}),
            new DotsLevelsInfo(6,7,15,6, new []{18000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,40), new DotsTarget(5,10)}),
            new DotsLevelsInfo(6,7,15,6, new []{19001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(4,10), new DotsTarget(5,10)}),
            new DotsLevelsInfo(6,7,20,6, new []{20001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(3,10), new DotsTarget(4,10), new DotsTarget(5,10)}),
            new DotsLevelsInfo(6,7,20,6, new []{21002},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(4,15), new DotsTarget(5,15)}),
            new DotsLevelsInfo(6,7,20,5, new []{22000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(4,20)}),
            new DotsLevelsInfo(6,7,20,5, new []{23000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(3,10), new DotsTarget(4,20)}),
            new DotsLevelsInfo(6,7,20,5, new []{24000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(3,15), new DotsTarget(4,20)}),
            new DotsLevelsInfo(6,7,20,5, new []{25001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,30), new DotsTarget(3,20), new DotsTarget(4,20)}),
            new DotsLevelsInfo(7,8,12,4, new []{26000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50)}),
            new DotsLevelsInfo(7,8,12,4, new []{27000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,60)}),
            new DotsLevelsInfo(7,8,12,5, new []{28000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,60)}),
            new DotsLevelsInfo(7,8,15,6, new []{29000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,60)}),
            new DotsLevelsInfo(7,8,15,6, new []{30000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(0,10)}),
            new DotsLevelsInfo(7,8,15,6, new []{31000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(0,10), new DotsTarget(1,10)}),
            new DotsLevelsInfo(7,8,18,6, new []{32000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(0,10), new DotsTarget(1,10), new DotsTarget(2,10)}),
            new DotsLevelsInfo(7,8,18,6, new []{33001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(0,15), new DotsTarget(1,15)}),
            new DotsLevelsInfo(7,8,18,6, new []{34001},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(2,20), new DotsTarget(3,20)}),
            new DotsLevelsInfo(7,8,18,6, new []{35000},new DotsObstacle[]{}, new DotsTarget[]{new DotsTarget(-1,50), new DotsTarget(4,20), new DotsTarget(5,20)}),
            new DotsLevelsInfo(7,8,12,4, new []{36000},new DotsObstacle[]{new DotsObstacle(4,5,1)}, new DotsTarget[]{new DotsTarget(-1,60)}),
            new DotsLevelsInfo(7,8,12,4, new []{37000},new DotsObstacle[]{new DotsObstacle(5,5,1), new DotsObstacle(1,3,1)}, new DotsTarget[]{new DotsTarget(-1,70)}),
            new DotsLevelsInfo(7,8,12,5, new []{38000},new DotsObstacle[]{new DotsObstacle(5,5,1), new DotsObstacle(1,3,1), new DotsObstacle(7,8,1)}, new DotsTarget[]{new DotsTarget(-1,70)}),
            new DotsLevelsInfo(7,8,12,6, new []{39000},new DotsObstacle[]{new DotsObstacle(1,8,1), new DotsObstacle(4,5,1), new DotsObstacle(7,2,1)}, new DotsTarget[]{new DotsTarget(-1,70)}),
            new DotsLevelsInfo(7,8,15,6, new []{40001},new DotsObstacle[]{new DotsObstacle(3,4,1)}, new DotsTarget[]{new DotsTarget(-1,70), new DotsTarget(0,10)}),
            new DotsLevelsInfo(7,8,15,6, new []{41000},new DotsObstacle[]{new DotsObstacle(1,5,1), new DotsObstacle(6,7,1)}, new DotsTarget[]{new DotsTarget(-1,70), new DotsTarget(0,10), new DotsTarget(1,10)}),
            new DotsLevelsInfo(7,8,18,6, new []{42002},new DotsObstacle[]{new DotsObstacle(1,2,1), new DotsObstacle(7,7,1), new DotsObstacle(3,3,1)}, new DotsTarget[]{new DotsTarget(-1,70), new DotsTarget(2,15), new DotsTarget(3,15)}),
            new DotsLevelsInfo(7,8,15,6, new []{43000},new DotsObstacle[]{new DotsObstacle(1,6,1), new DotsObstacle(7,1,1), new DotsObstacle(2,3,1)}, new DotsTarget[]{new DotsTarget(-1,70), new DotsTarget(4,15), new DotsTarget(5,15)}),
            new DotsLevelsInfo(7,8,15,5, new []{44000},new DotsObstacle[]{new DotsObstacle(2,4,1), new DotsObstacle(5,7,1), new DotsObstacle(3,6,1), new DotsObstacle(3,7,1)}, new DotsTarget[]{new DotsTarget(-1,80)}),
            new DotsLevelsInfo(7,8,15,5, new []{45000},new DotsObstacle[]{new DotsObstacle(6,4,1), new DotsObstacle(4,7,1), new DotsObstacle(2,6,1), new DotsObstacle(1,5,1), new DotsObstacle(1,3,1)}, new DotsTarget[]{new DotsTarget(-1,80)}),
            new DotsLevelsInfo(7,8,15,6, new []{46000},new DotsObstacle[]{new DotsObstacle(1,6,1), new DotsObstacle(7,1,1), new DotsObstacle(2,3,1)}, new DotsTarget[]{new DotsTarget(-1,80)}),
            new DotsLevelsInfo(7,8,15,6, new []{47000},new DotsObstacle[]{new DotsObstacle(1,8,1), new DotsObstacle(4,5,1), new DotsObstacle(7,2,1)}, new DotsTarget[]{new DotsTarget(-1,80), new DotsTarget(0,15)}),
            new DotsLevelsInfo(7,8,15,6, new []{48001},new DotsObstacle[]{new DotsObstacle(5,5,1), new DotsObstacle(1,3,1), new DotsObstacle(7,8,1)}, new DotsTarget[]{new DotsTarget(-1,80), new DotsTarget(1,15), new DotsTarget(2,15)}),
            new DotsLevelsInfo(7,8,18,6, new []{49000},new DotsObstacle[]{new DotsObstacle(2,4,1), new DotsObstacle(5,7,1), new DotsObstacle(3,6,1)}, new DotsTarget[]{new DotsTarget(-1,80), new DotsTarget(3,15), new DotsTarget(4,15), new DotsTarget(5,15)}),
            new DotsLevelsInfo(7,8,18,6, new []{50001},new DotsObstacle[]{new DotsObstacle(4,7,1), new DotsObstacle(2,6,1), new DotsObstacle(1,5,1), new DotsObstacle(1,3,1)}, new DotsTarget[]{new DotsTarget(-1,100)}),
            new DotsLevelsInfo(7,8,18,5, new []{51000},new DotsObstacle[]{new DotsObstacle(7,1,1), new DotsObstacle(2,3,1), new DotsObstacle(4,5,1), new DotsObstacle(7,2,1)}, new DotsTarget[]{new DotsTarget(-1,100)}),
            new DotsLevelsInfo(7,8,20,6, new []{52001},new DotsObstacle[]{new DotsObstacle(6,4,1), new DotsObstacle(4,7,1), new DotsObstacle(2,6,1), new DotsObstacle(1,5,1), new DotsObstacle(1,3,1)}, new DotsTarget[]{new DotsTarget(-1,100)}),
            new DotsLevelsInfo(7,8,20,6, new []{53001},new DotsObstacle[]{new DotsObstacle(1,5,1), new DotsObstacle(6,7,1), new DotsObstacle(2,3,1)}, new DotsTarget[]{new DotsTarget(-1,100)}),
            new DotsLevelsInfo(7,8,20,6, new []{54000},new DotsObstacle[]{new DotsObstacle(1,2,1), new DotsObstacle(7,7,1)}, new DotsTarget[]{new DotsTarget(-1,100)}),
            new DotsLevelsInfo(7,8,20,6, new []{55000},new DotsObstacle[]{new DotsObstacle(1,8,1), new DotsObstacle(4,5,1), new DotsObstacle(7,2,1)}, new DotsTarget[]{new DotsTarget(-1,100)})




        };

        public static readonly TutorialFrame[] Tutorial = new TutorialFrame[]
        {
            new TutorialFrame("TutorialDots/Pause_1", .4f),
            new TutorialFrame("TutorialDots/Pause_2", .4f),
            new TutorialFrame("TutorialDots/Pause_3", .4f),
            new TutorialFrame("TutorialDots/Pause_4", .4f),
            new TutorialFrame("TutorialDots/Pause_5", .2f),
            new TutorialFrame("TutorialDots/Pause_6", .2f),
            new TutorialFrame("TutorialDots/Pause_7", .2f),
            new TutorialFrame("TutorialDots/Pause_8", .2f),
            new TutorialFrame("TutorialDots/Pause_9", .2f),
            new TutorialFrame("TutorialDots/Pause_10", .2f),
            new TutorialFrame("TutorialDots/Pause_11", .4f),
            new TutorialFrame("TutorialDots/Pause_12", .4f),
            new TutorialFrame("TutorialDots/Pause_13", .4f),
            new TutorialFrame("TutorialDots/Pause_14", .4f),
            new TutorialFrame("TutorialDots/Pause_15", .4f),
            new TutorialFrame("TutorialDots/Pause_16", .4f),
            new TutorialFrame("TutorialDots/Pause_17", .4f),
            new TutorialFrame("TutorialDots/Pause_18", .4f),
            new TutorialFrame("TutorialDots/Pause_19", .4f),
            new TutorialFrame("TutorialDots/Pause_20", .2f),
            new TutorialFrame("TutorialDots/Pause_21", .2f),
            new TutorialFrame("TutorialDots/Pause_22", .2f),
            new TutorialFrame("TutorialDots/Pause_23", .2f),
            new TutorialFrame("TutorialDots/Pause_24", .2f),
            new TutorialFrame("TutorialDots/Pause_25", .2f),
            new TutorialFrame("TutorialDots/Pause_26", .2f),
            new TutorialFrame("TutorialDots/Pause_27", .2f),
            new TutorialFrame("TutorialDots/Pause_28", .2f),
            new TutorialFrame("TutorialDots/Pause_29", .2f)
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