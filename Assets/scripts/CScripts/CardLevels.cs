namespace Cards.CScripts
{
    public static class CardLevels
    {
        public static readonly CardLevelInfo[] CardLevelInfos =
        {
            new CardLevelInfo(2,2,3),
            new CardLevelInfo(3,2,5,2),
            new CardLevelInfo(3,2,7),
            new CardLevelInfo(4,3,9,cardTypes:3),
            new CardLevelInfo(4,3,11,cardTypes:4)
            
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