﻿using System;
using UnityEngine;

namespace WashingMachine.WMScripts
{
    public static class WMLevels
    {
        public static readonly WMLevelInfo[] WmLevelInfos =
        {
            new WMLevelInfo(0.5f,1f,-1,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    //new WMSockInfo(0,1,1),
                    new WMSockInfo(0,2,0,15,3),
                    new WMSockInfo(0,2,0,5,3),
                }),
            new WMLevelInfo(0.5f,1f,-1,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(0,1,0,10),
                    new WMSockInfo(0,2,1,10),
                })
        };


        public static string[] WmSockTypeLookup = {"prefabs/BaseSockPrefab"};

    }



    public class WMLevelInfo
    {
        private WMSockInfo[] _wmSockInfos;
        private int fullBias;
        public float SockSpawnTime; // sock spawn time in seconds
        public float WheelSpeed; // the speed of the wheel, individual sock speed depends on this
        public int MaxSock; // the max number of socks, if -1 there is no limit
        public WMLevelInfo(float sockSpawnTime, float wheelSpeed, int maxSock, WMSockInfo[] wmSockInfos)
        {
            _wmSockInfos = wmSockInfos;
            fullBias = 0;
            foreach (var wmSockInfo in _wmSockInfos)
            {
                fullBias += wmSockInfo.Bias;
            }

            SockSpawnTime = sockSpawnTime;
            WheelSpeed = wheelSpeed;
            MaxSock = maxSock;


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
            for (int i = 0; i < _wmSockInfos.Length; i++)
            {
                runningBias += _wmSockInfos[i].Bias;
                if (runningBias > dd)
                {
                    return _wmSockInfos[i];
                }
            }
            
            return _wmSockInfos[0];
        }
    }

    public class WMSockInfo
    {
        public readonly byte SockType; // Sock type is the prefab, matched from the lookup table
        public readonly byte SockNo; // sock no is the no of the sprite in the prefab
        public readonly byte LevelCollect; // the number of this sock that needs to be collected
        public readonly byte Speed; // the fall speed where 10 is the bg speed
        public readonly byte Bias; // the partial summed bias
        public WMSockInfo(byte sockType, byte sockNo, byte levelCollect, byte speed, byte bias=1)
        {
            SockType = sockType;
            SockNo = sockNo;
            LevelCollect = levelCollect;
            Speed = speed;
            Bias = bias;
        }
        
        
        
        
    }
    
    
    
}