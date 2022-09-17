using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using WashingMachine.WMScripts;

namespace RainScripts
{
    public class RainScoreboard
    {
        private List<RainSockInfo> _scoreSocks;
        private int[] _collected;

        public int[] Collected => _collected;

        public RainScoreboard(List<RainSockInfo> scoreSocks)
        {
            _scoreSocks = scoreSocks;
            _collected = new int[_scoreSocks.Count];
        }

        public string[] ScoreAddressArray()
        {
            return (string[]) (from ss in _scoreSocks select WMLevels.WMUISockLookup[ss.SockType, ss.SockNo]).ToArray();
        }

        public RainSockInfo[] GetSockInfo()
        {
            var v = new List<RainSockInfo>();
            for (var i = 0; i < _scoreSocks.Count; i++)
            {
                v.Add(
                        new RainSockInfo(_scoreSocks[i].SockType, _scoreSocks[i].SockNo, _scoreSocks[i].LevelCollect-_collected[i],
                            _scoreSocks[i].Speed)
                    );
                
            }

            return v.ToArray();
        }

        public void IncreseSock(byte style, byte no, byte by =1)
        {
            var lastIndex = -1;
            var lastValue = 10000;
            
            for (var i = 0; i < _scoreSocks.Count; i++)
            {
                if (_scoreSocks[i].SockNo == no && _scoreSocks[i].SockType == style)
                {
                    _collected[i] += by;
                    if (_scoreSocks[i].LevelCollect < _collected[i])
                    {
                        _collected[i] = _scoreSocks[i].LevelCollect;
                    }
                }
            }
            
            
            
            
            /*
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
            */


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

        public int[] GetCollected()
        {
            for (var i = 0; i < _collected.Length; i++)
            {
                if (_scoreSocks[i].LevelCollect < _collected[i])
                {
                    _collected[i] = -1*_collected[i];
                }
            }


            return _collected;
        }
        
        
    }
}