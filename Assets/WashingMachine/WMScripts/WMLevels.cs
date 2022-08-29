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
            new WMLevelInfo(1f,4f,-1, 5,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(0,0,1,20),
                    //new WMSockInfo(0,1,1),
                    new WMSockInfo(1,0,0,10,1),
                    new WMSockInfo(0,0,1,10,1),
                    new WMSockInfo(0,0,0,5,1)
                }),
            new WMLevelInfo(0.5f,1f,-1, 25,
                new WMSockInfo[]
                {
                    new WMSockInfo(0,0,1,10),
                    new WMSockInfo(1,0,0,10),
                    new WMSockInfo(4,0,1,10),
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
        public WMSockInfo(byte sockType, byte sockNo, byte levelCollect, byte speed, byte bias=1)
        {
            SockType = sockType;
            SockNo = sockNo;
            LevelCollect = levelCollect;
            Speed = speed;
            Bias = bias;
        }
        
        
        
        
    }

    public class WMScoreboard
    {
        private List<WMSockInfo> _scoreSocks;
        private int[] _collected;

        public int[] Collected => _collected;

        public WMScoreboard(List<WMSockInfo> scoreSocks)
        {
            _scoreSocks = scoreSocks;
            _collected = new int[_scoreSocks.Count];
        }

        public string[] ScoreAddressArray()
        {
            return (string[]) (from ss in _scoreSocks select WMLevels.WMUISockLookup[ss.SockType, ss.SockNo]).ToArray();
        }

        public void IncreseSock(byte style, byte no, byte by =1)
        {
            var lastIndex = -1;
            var lastValue = 10000;
            
            for (var i = 0; i < _scoreSocks.Count; i++)
            {
                if (_scoreSocks[i].SockNo == no && _scoreSocks[i].SockType == style)
                {
                    if (_collected[i] == 0)
                    {
                        _collected[i] += by;
                        return; 
                    }else if (_collected[i] == 10000)
                    {
                        throw new Exception("collected is greater than 10000 something is clearly wrong");
                    }
                    else
                    {
                        if (lastValue>_collected[i])
                        {
                            lastIndex = i;
                            lastValue = _collected[i];
                            
                        }
                        
                    }        
                } 
            }
            if(lastIndex==-1) return;

            _collected[lastIndex] += by;



        }

        public bool GameWon()
        {
            for (var i = 0; i < _scoreSocks.Count; i++)
            {
                if (_scoreSocks[i].LevelCollect != _collected[i])
                {
                    return false;
                }
            }

            return true;
        }

        public MonsterMood GetWashingMachineMood(int moveLeft)
        {
            var leftMoves = 0;
            for (var i = 0; i < _scoreSocks.Count; i++)
            {
                leftMoves += _scoreSocks[i].LevelCollect - _collected[i];
            }

            if (leftMoves > moveLeft)
            {
                return MonsterMood.Sad;
            }else if (leftMoves*1.5f > moveLeft )
            {
                return MonsterMood.Excited;
            }
            else
            {
                return MonsterMood.Happy;
            }

        }
        
        
    }
    
    
    
}