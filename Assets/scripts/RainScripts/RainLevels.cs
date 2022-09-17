using System;
using System.Collections.Generic;
using Classes;
using UnityEngine;

namespace RainScripts
{
    public static class RainLevels
    {
        public static readonly RainLevelInfo[] RainLevelInfos =
        {
            new RainLevelInfo(1f,2f,-1, 15,
                new RainSockInfo[]
                {
                    new RainSockInfo(0,0,5,10,bias:3),
                    new RainSockInfo(3,0,5,10,bias:3),
                    new RainSockInfo(0,0,0,8),
                    new RainSockInfo(3,0,0,15),
                }),//1
            
            new RainLevelInfo(1f,2f,-1, 15,
                new RainSockInfo[]
                {
                    new RainSockInfo(1,0,5,10),
                    new RainSockInfo(4,0,5,15),
                    new RainSockInfo(6,0,0,10),
                }), //2
            new RainLevelInfo(0.5f,2f,-1, 20,
                new RainSockInfo[]
                {
                    new RainSockInfo(2,0,15,10,bias:2),
                    new RainSockInfo(0,0,0,15),
                    new RainSockInfo(3,0,0,5),
                    new RainSockInfo(6,0,0,10),
                    
                }), //3
            new RainLevelInfo(.8f,4f,-1, 15,
                new RainSockInfo[]
                {
                    new RainSockInfo(4,0,5,10),
                    new RainSockInfo(0,0,0,10,2),
                    new RainSockInfo(1,0,0,10,2),
                    new RainSockInfo(2,0,0,10,2),
                }),//4
            new RainLevelInfo(0.5f,4f,-1, 25,
                new RainSockInfo[]
                {
                    
                    new RainSockInfo(5,0,10,20),
                    new RainSockInfo(5,0,0,8),
                    new RainSockInfo(6,0,10,15),
                    new RainSockInfo(6,0,0,5),
                    new RainSockInfo(7,0,0,10),
                    new RainSockInfo(8,0,0,15),
                }), //5
            new RainLevelInfo(0.8f,4f,-1, 30,
                new RainSockInfo[]
                {
                    
                    new RainSockInfo(1,0,8,8),
                    new RainSockInfo(3,0,8,14),
                    new RainSockInfo(7,0,8,10),
                    new RainSockInfo(4,0,0,5),
                    new RainSockInfo(0,0,0,10),
                    new RainSockInfo(2,0,0,30),
                }),//6
            new RainLevelInfo(.7f,2f,-1, 15,
                new RainSockInfo[]
                {
                    new RainSockInfo(4,0,7,40),
                    new RainSockInfo(0,0,0,10,bias:2),
                    new RainSockInfo(2,0,0,10,bias:2),
                }), //7
            new RainLevelInfo(.9f,4f,-1, 20,
                new RainSockInfo[]
                {
                    new RainSockInfo(4,0,6,20),
                    new RainSockInfo(8,0,6,20),
                    new RainSockInfo(2,0,0,5),
                    new RainSockInfo(2,0,0,10),
                    new RainSockInfo(2,0,0,15),
                    new RainSockInfo(2,0,0,20),
                }),//8
            new RainLevelInfo(.8f,4f,-1, 20,
                new RainSockInfo[]
                {
                    new RainSockInfo(3,1,6,15, bias:1),
                    new RainSockInfo(7,0,9,20, bias:2),
                    new RainSockInfo(8,0,0,5),
                    new RainSockInfo(8,0,0,10),
                    new RainSockInfo(3,0,0,15, bias:2),
                    new RainSockInfo(3,0,0,20),
                }),//9
            new RainLevelInfo(.8f,4f,-1, 20,
                new RainSockInfo[]
                {
                    new RainSockInfo(1,1,5,15, bias:1),
                    new RainSockInfo(6,0,10,20, bias:2),
                    new RainSockInfo(4,0,0,5),
                    new RainSockInfo(3,0,0,15, bias:2),
                    new RainSockInfo(1,0,0,20),
                }),//10
            new RainLevelInfo(.7f,3f,-1, 20,
                new RainSockInfo[]
                {
                    new RainSockInfo(2,1,10,5, bias:2),
                    new RainSockInfo(4,0,5,10, bias:1),
                    new RainSockInfo(7,0,0,10),
                    new RainSockInfo(6,0,0,5, bias:3),
                    new RainSockInfo(2,1,0,20),
                }),//11
            new RainLevelInfo(.6f,2f,-1, 24,
                new RainSockInfo[]
                {
                    new RainSockInfo(4,1,12,20, bias:2),
                    new RainSockInfo(0,1,6,15, bias:1),
                    new RainSockInfo(1,0,0,10),
                    new RainSockInfo(4,0,0,7, bias:3),
                }),//12
            new RainLevelInfo(.7f,4f,-1, 24,
                new RainSockInfo[]
                {
                    new RainSockInfo(8,0,6,20, bias:2),
                    new RainSockInfo(0,1,6,15, bias:1),
                    new RainSockInfo(3,1,6,15),
                    new RainSockInfo(4,0,0,10, bias:3),
                    new RainSockInfo(4,0,0,5, bias:1),
                }),//13
            new RainLevelInfo(.8f,4f,-1, 24,
                new RainSockInfo[]
                {
                    new RainSockInfo(2,0,6,20, bias:2),
                    new RainSockInfo(4,1,6,5, bias:2),
                    new RainSockInfo(8,0,6,15, bias:2),
                    new RainSockInfo(1,1,0,10, bias:3),
                    new RainSockInfo(1,0,0,15, bias:1),
                }),//14
            new RainLevelInfo(.8f,4f,-1, 24,
                new RainSockInfo[]
                {
                    new RainSockInfo(1,1,6,20, bias:2),
                    new RainSockInfo(0,1,6,5, bias:2),
                    new RainSockInfo(8,0,6,15, bias:3),
                    new RainSockInfo(2,1,0,10, bias:3),
                    new RainSockInfo(6,0,0,5, bias:1),
                    new RainSockInfo(5,0,0,15, bias:3),
                    new RainSockInfo(4,0,0,5, bias:1),
                }),//15
            new RainLevelInfo(.9f,4f,-1, 24,
                new RainSockInfo[]
                {
                    new RainSockInfo(3,1,5,20, bias:2),
                    new RainSockInfo(2,1,5,5, bias:2),
                    new RainSockInfo(7,0,5,15, bias:3),
                    new RainSockInfo(3,0,5,15, bias:3),
                    new RainSockInfo(0,1,0,10, bias:3),
                    new RainSockInfo(8,0,0,5, bias:1),
                    new RainSockInfo(2,0,0,15, bias:3),
                    new RainSockInfo(0,0,0,5, bias:1),
                })//16
        };


        public static string[] RainSockTypeLookup = {
            "prefabs/SockPrefabs/s1", "prefabs/SockPrefabs/s2", "prefabs/SockPrefabs/s3",
            "prefabs/SockPrefabs/s4", "prefabs/SockPrefabs/s5", "prefabs/SockPrefabs/s6",
            "prefabs/SockPrefabs/s7", "prefabs/SockPrefabs/s8", "prefabs/SockPrefabs/s9"};


        public static List<string[]> RainUISockLookup = new List<string[]>()
        {
            new []{"ui/wm_socks/ss1", "ui/wm_socks/ss12"},
            new []{"ui/wm_socks/ss2", "ui/wm_socks/ss22"},
            new []{"ui/wm_socks/ss3", "ui/wm_socks/ss32"},
            new []{"ui/wm_socks/ss4", "ui/wm_socks/ss42"}, 
            new []{"ui/wm_socks/ss5", "ui/wm_socks/ss52"}, 
            new []{"ui/wm_socks/ss6" },
            new []{"ui/wm_socks/ss7" }, 
            new []{"ui/wm_socks/ss8" }, 
            new []{"ui/wm_socks/ss9" }
            
        };
        
        public static readonly TutorialFrame[] Tutorial = new TutorialFrame[]
        {

        };

    }



    public class RainLevelInfo
    {
        public RainSockInfo[] RainSockInfos;
        private int fullBias;
        public float SockSpawnTime; // sock spawn time in seconds
        public float WheelSpeed; // the speed of the wheel, individual sock speed depends on this
        public int MaxSock; // the max number of socks, if -1 there is no limit
        public int MoveNo; // number of moves
        public RainLevelInfo(float sockSpawnTime, float wheelSpeed, int maxSock, int moveNo, RainSockInfo[] rainSockInfos)
        {
            RainSockInfos = rainSockInfos;
            fullBias = 0;
            foreach (var rainSockInfo in RainSockInfos)
            {
                fullBias += rainSockInfo.Bias;
            }

            SockSpawnTime = sockSpawnTime;
            WheelSpeed = wheelSpeed;
            MaxSock = maxSock;
            MoveNo = moveNo;


        }


        public void InitializeLevel(Action<int, GameObject> also)
        {
            for (var i = 0; i < RainSockInfos.Length; i++)
            {
                var r = RainSockInfos[i].Resource();
                also(i-5,r);
            }

        }

        
        
        /** Returns a randomly picked sock, bias is introduced here if needed
         * random not included and must be inserted from outside
         */
        public RainSockInfo GetRandomSock(double d)
        {
            var dd = fullBias * d;
            var runningBias = 0;
            for (int i = 0; i < RainSockInfos.Length; i++)
            {
                runningBias += RainSockInfos[i].Bias;
                if (runningBias > dd)
                {
                    return RainSockInfos[i];
                }
            }
            
            return RainSockInfos[0];
        }
        
        
    }

    public class RainSockInfo
    {
        public readonly byte SockType; // Sock type is the prefab, matched from the lookup table
        public readonly byte SockNo; // sock no is the no of the sprite in the prefab
        public readonly int LevelCollect; // the number of this sock that needs to be collected
        public readonly byte Speed; // the fall speed where 10 is the bg speed
        public readonly byte Bias; // the partial summed bias
        private GameObject _resource;
        public RainSockInfo(byte sockType, byte sockNo, int levelCollect, byte speed, byte bias=1)
        {
            SockType = sockType;
            SockNo = sockNo;
            LevelCollect = levelCollect;
            Speed = speed;
            Bias = bias;
        }

        public GameObject Resource()
        {
            try
            {
                var a = _resource.gameObject.transform;
                return _resource;
            }
            catch(NullReferenceException)
            {
                _resource = (GameObject) Resources.Load(RainLevels.RainSockTypeLookup[SockType]);
                return _resource;
            }
            
        }
        
        
        
        
    }

    
    
    
    
}