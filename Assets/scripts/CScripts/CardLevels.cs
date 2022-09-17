using Classes;

namespace Cards.CScripts
{
    public static class CardLevels
    {
        public static readonly CardLevelInfo[] CardLevelInfos =
        {
            new CardLevelInfo(2,2,5),
            new CardLevelInfo(3,2,8,2),
            new CardLevelInfo(3,2,10),
            new CardLevelInfo(4,3,14,cardTypes:3),
            new CardLevelInfo(4,3,17,cardTypes:4)
            
        };

        public static readonly TutorialFrame[] Tutorial = new TutorialFrame[]
        {
            new TutorialFrame("TutorialCards/CardTutorial_1", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_2", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_3", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_4", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_5", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_6", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_7", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_8", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_9", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_10", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_11", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_12", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_13", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_14", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_15", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_16", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_17", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_18", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_19", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_20", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_21", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_22", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_23", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_24", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_25", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_26", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_27", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_28", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_29", .2f),
            new TutorialFrame("TutorialCards/CardTutorial_30", .2f),
        };
    }


    public class CardLevelInfo
    {
        public int Row;
        public int Column;
        public int CardTypes;
        public int Moves;
        public CardLevelInfo(int row, int column, int moves, int cardTypes = -1)
        {
            Row = row;
            Column = column;
            CardTypes = cardTypes;
            Moves = moves;
        }
    }
}