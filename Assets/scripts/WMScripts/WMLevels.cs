using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using UnityEngine;

namespace WashingMachine.WMScripts
{
    public static class WMLevels
    {
        public static readonly WMLevelInfo[] WmLevelInfos =
        {
            new WMLevelInfo(1f,2f,-1, 5,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(3,0,1,20),
                    new WMSockInfo(0,0,0,10),
                    new WMSockInfo(3,0,0,20),
                }),
            
            new WMLevelInfo(1f,2f,-1, 5,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(3,0,1,20),
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(3,0,1,20),
                }),
            new WMLevelInfo(0.5f,4f,-1, 25,
                new WMSockInfo[]
                {
                    new WMSockInfo(1,0,0,10,bias:2),
                    new WMSockInfo(5,0,0,10,bias:2),
                    new WMSockInfo(4,0,1,10),
                    new WMSockInfo(4,0,1,10),
                    
                }),
            new WMLevelInfo(1f,4f,-1, 5,
                new WMSockInfo[]
                {
                    new WMSockInfo(6,0,1,10),
                    new WMSockInfo(6,0,1,20),
                    new WMSockInfo(6,0,1,20,3),
                    //new WMSockInfo(0,1,1),
                    new WMSockInfo(3,0,0,10,1),
                    new WMSockInfo(7,0,0,10,3),
                    new WMSockInfo(0,0,0,5,3)
                }),
            new WMLevelInfo(0.5f,4f,-1, 25,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(1,0,1,10),
                    new WMSockInfo(2,0,1,10),
                    new WMSockInfo(3,0,0,10),
                    new WMSockInfo(4,0,0,10),
                    new WMSockInfo(5,0,0,10),
                    new WMSockInfo(6,0,0,10),
                    new WMSockInfo(7,0,0,10),
                    new WMSockInfo(8,0,0,10),
                })
        };


        public static string[] WmSockTypeLookup = {
            "prefabs/SockPrefabs/s1", "prefabs/SockPrefabs/s2", "prefabs/SockPrefabs/s3",
            "prefabs/SockPrefabs/s4", "prefabs/SockPrefabs/s5", "prefabs/SockPrefabs/s6",
            "prefabs/SockPrefabs/s7", "prefabs/SockPrefabs/s8", "prefabs/SockPrefabs/s9"};


        public static string[,] WMUISockLookup =
        {
            { "ui/wm_socks/ss1"}, {"ui/wm_socks/ss2" }, {"ui/wm_socks/ss3" },
            {"ui/wm_socks/ss4" }, {"ui/wm_socks/ss5" }, {"ui/wm_socks/ss6" },
            {"ui/wm_socks/ss7" }, {"ui/wm_socks/ss8" }, {"ui/wm_socks/ss9" }
            
        };

    }



    public class WMLevelInfo
    {
        public WMSockInfo[] WmSockInfos;
        private int fullBias;
        public float SockSpawnTime; // sock spawn time in seconds
        public float WheelSpeed; // the speed of the wheel, individual sock speed depends on this
        public int MaxSock; // the max number of socks, if -1 there is no limit
        public int MoveNo; // number of moves
        public WMLevelInfo(float sockSpawnTime, float wheelSpeed, int maxSock, int moveNo, WMSockInfo[] wmSockInfos)
        {
            WmSockInfos = wmSockInfos;
            fullBias = 0;
            foreach (var wmSockInfo in WmSockInfos)
            {
                fullBias += wmSockInfo.Bias;
            }

            SockSpawnTime = sockSpawnTime;
            WheelSpeed = wheelSpeed;
            MaxSock = maxSock;
            MoveNo = moveNo;


        }


        public void InitializeLevel(Action<int, GameObject> also)
        {
            for (var i = 0; i < WmSockInfos.Length; i++)
            {
                var r = WmSockInfos[i].Resource();
                also(i-5,r);
            }

        }

        
        
        /** Returns a randomly picked sock, bias is introduced here if needed
         * random not included and must be inserted from outside
         */
        public WMSockInfo GetRandomSock(double d)
        {
            //var i = (int)Math.Round(_wmSockInfos.Length * d);
            //i %= _wmSockInfos.Length;
            var dd = fullBias * d;
            var runningBias = 0;
            for (int i = 0; i < WmSockInfos.Length; i++)
            {
                runningBias += WmSockInfos[i].Bias;
                if (runningBias > dd)
                {
                    return WmSockInfos[i];
                }
            }
            
            return WmSockInfos[0];
        }
        
        
    }

    public class WMSockInfo
    {
        public readonly byte SockType; // Sock type is the prefab, matched from the lookup table
        public readonly byte SockNo; // sock no is the no of the sprite in the prefab
        public readonly byte LevelCollect; // the number of this sock that needs to be collected
        public readonly byte Speed; // the fall speed where 10 is the bg speed
        public readonly byte Bias; // the partial summed bias
        private GameObject _resource;
        public WMSockInfo(byte sockType, byte sockNo, byte levelCollect, byte speed, byte bias=1)
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
                _resource = (GameObject) Resources.Load(WMLevels.WmSockTypeLookup[SockType]);
                return _resource;
            }
            
        }
        
        
        
        
    }

    
    
    
    
}