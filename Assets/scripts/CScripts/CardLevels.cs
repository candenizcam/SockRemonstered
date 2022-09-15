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