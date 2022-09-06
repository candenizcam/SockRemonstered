using System;
using UnityEngine;

namespace Classes
{
    public static class Constants
    {
        public static readonly Color[] GameColours =  {
            new Color(0.63f, 0.18f, 0.15f), //a12d26 0
            new Color(0.71f, 0.22f, 0.19f), //b53831
            new Color(0.76f, 0.30f, 0.20f), //c14c33
            new Color(0.83f, 0.49f, 0.25f), //d37c41
            new Color(0.91f, 0.74f, 0.33f), //e8bd53
            new Color(0.92f, 0.87f, 0.41f), //ebdd68 5
            new Color(0.91f, 0.85f, 0.55f), //e9d98d
            new Color(0.77f, 0.75f, 0.53f), //c5bf88
            new Color(0.64f, 0.72f, 0.36f), //a4b85c
            new Color(0.45f, 0.55f, 0.25f), //748b41
            new Color(0.27f, 0.35f, 0.16f), //455928 10
            new Color(0.16f, 0.25f, 0.29f), //29414b sexually interesting green
            new Color(0.28f, 0.51f, 0.44f), //478371
            new Color(0.37f, 0.70f, 0.69f), //5eb2af
            new Color(0.57f, 0.81f, 0.84f), //92cfd6
            new Color(0.32f, 0.59f, 0.65f), //5296a5 15
            new Color(0.24f, 0.47f, 0.61f), //3e789b
            new Color(0.44f, 0.51f, 0.80f), //7181cc
            new Color(0.78f, 0.50f, 0.42f), //c67f6c
            new Color(0.36f, 0.13f, 0.22f), //5b2139
            new Color(0.62f, 0.13f, 0.41f), //9d2269 20
            new Color(0.75f, 0.45f, 0.53f), //c07486
            new Color(0.87f, 0.65f, 0.66f), //dda6a9
            new Color(0.86f, 0.79f, 0.78f), //dcc9c7
            new Color(0.93f, 0.95f, 0.96f), //eef2f6
            new Color(0.76f, 0.79f, 0.80f), //c2cacb 25
            new Color(0.42f, 0.43f, 0.45f), //6b6d74
            new Color(0.24f, 0.22f, 0.22f), //3e3839
            new Color(0.10f, 0.10f, 0.10f), //191a19

        };

        public const int MaxHearts = 3;
        public const int BetweenHeartsTime = 60; // in secs
        public const float FurnitureChance = 0.1f;
        public const bool MatchDiagonal = true;
        
        public static readonly Color[] DotsColours = new Color[]
        {
            new Color(0.3f,0f,0f,1f),
            new Color(0.3f,0.15f,0f,1f),
            new Color(0.3f,0.3f,0f,1f),
            new Color(0f,0.3f,.1f,1f),
            new Color(0f,0.1f,0.3f,1f),
            new Color(0.3f,0.1f,0.4f,1f)
        };

        public static NextLevelData GetNextLevel(int bigNumber)
        {

            Debug.Log($"big number {bigNumber}");

            var v = bigNumber - 1;
            var l = (v / 10)*5;
            if (v % 10 >= 5)
            {
                return new NextLevelData("Cards",l + v % 5 );
            }
            else
            {
                return new NextLevelData("WashingMachineScene",l + v % 5 );
            }
            /*
            var l = (bigNumber / 10)*5;
            if ((bigNumber-1) % 10 >= 5)
            {
                return new NextLevelData("Cards",l + bigNumber % 5 -1);
            }
            else
            {
                return new NextLevelData("WashingMachineScene",l + bigNumber % 5-1 );
            }
            */
            /*
            if (bigNumber % 5==0)
            {
                return new NextLevelData("Cards",bigNumber / 5 - 1);
            }

            var l = (bigNumber / 10) * 4;
            if (bigNumber % 10 > 5)
            {
                return new NextLevelData("Dots",l+bigNumber % 5 - 1);
            }
            else
            {
                return new NextLevelData("WashingMachineScene",l + bigNumber % 5 - 1);
            }
            */

        }
        

    }

    public class NextLevelData
    {
        public string SceneName;
        public int LevelNo;
        public NextLevelData(string sceneName, int levelNo)
        {
            SceneName = sceneName;
            LevelNo = levelNo;

        }
    }
    
}