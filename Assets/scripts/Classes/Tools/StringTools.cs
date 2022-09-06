namespace Classes
{
    public static class StringTools
    {
        public static string NumberToThreeDigits(int v, string start="", string end="")
        {
            string[] l = {"","k","M","B","T","Q","S"};
            var c = v;
            var oldrem = 0;
            var s = "A LOT";
            for (var i = 0; i < l.Length; i++)
            {
                if (c < 1000)
                {
                    if (oldrem == 0)
                    {
                        s = $"{c}{l[i]}";
                        
                    }
                    else
                    {
                        s = $"{(float)c + oldrem/1000f:G3}{l[i]}";
                        
                    }
                    break;
                }
                else
                {
                    oldrem = c % 1000;
                    c = c / 1000;
                    
                }
            }

            return start+ s+end;
        }
        
    }
}