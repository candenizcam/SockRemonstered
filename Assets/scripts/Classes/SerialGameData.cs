using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Classes
{
    [Serializable]
    public class SerialGameData
    {
        //public int activeLevel;
        //public string activeSceneId;
        //public List<LevelSetMain.LevelSetSerialInfo> levelSetInfoList;
        public int coins;
        public int nextLevel;
        public int music;
        public int sound;
        private int hearts;
        public long heartTimeBinary; // use with DateTime.FromBinary()
        public List<string> purchased; // maybe this will be server one day...
        public string[] lineup;
        public List<string> activeFurnitures; // maybe this will be server one day...
        public int AdCounter = 0;
        private const string Location = "/sockMonsterData.dat";

        public bool InterstatialAdTime()
        {
            //AdCounter
            AdCounter += 1;
            if (AdCounter == Constants.AdPeriod)
            {
                AdCounter = 0;
            }
            Save();
            
            
            return AdCounter==0;
        }

        public int getHearts()
        {
            if (hearts == Constants.MaxHearts)
            {
                return hearts;
            }

            var pastTime = DateTime.FromBinary(heartTimeBinary);
            var dt = DateTime.Now;
            var delta = dt - pastTime;
            hearts = Math.Min(hearts + (int)delta.TotalSeconds / Constants.BetweenHeartsTime, Constants.MaxHearts);
            var rem = (int)delta.TotalSeconds % Constants.BetweenHeartsTime;
            var newPast = dt.Subtract(TimeSpan.FromSeconds(rem));
            heartTimeBinary = newPast.ToBinary();
            Save();
            return hearts;
        }
        
        public (int hearts, float rem) getHeartsAndRem()
        {
            if (hearts == Constants.MaxHearts)
            {
                return (hearts,1f);
            }

            var pastTime = DateTime.FromBinary(heartTimeBinary);
            var dt = DateTime.Now;
            var delta = dt - pastTime;
            hearts = Math.Min(hearts + (int)delta.TotalSeconds / Constants.BetweenHeartsTime, Constants.MaxHearts);
            var rem = (int)delta.TotalSeconds % Constants.BetweenHeartsTime;
            var newPast = dt.Subtract(TimeSpan.FromSeconds(rem));
            heartTimeBinary = newPast.ToBinary();
            Save();
            //return hearts;
            return (hearts, (float)rem/(float)Constants.BetweenHeartsTime);
        }

        public int changeHearts(int delta)
        {
            var h = getHearts();
            bool b = h == Constants.MaxHearts;
            h = Math.Min(h+delta,Constants.MaxHearts);
            if (h < Constants.MaxHearts && b)
            {
                heartTimeBinary = DateTime.Now.ToBinary();
            }
            hearts = h;
            Save();
            return hearts; 
        }
        
        public static void ResetSaves()
        {
            var sgd = SerialGameData.DefaultData();
            sgd.Save();
        }
        
        public static SerialGameData DefaultData()
        {
            var sgd = new SerialGameData();
            sgd.coins = 0;
            sgd.nextLevel = 1;
            sgd.music = 1;
            sgd.sound = 1;
            sgd.hearts = 3;
            sgd.lineup = new[] {"Body_Raw","LArm_Raw","RArm_Raw","LLeg_Raw","RLeg_Raw" };
            sgd.purchased = new List<string>();
            sgd.activeFurnitures = new List<string>();
            //sgd.activeLevel = 0;
            //sgd.activeSceneId = "LevelSet";
            //sgd.levelSetInfoList = new List<LevelSetMain.LevelSetSerialInfo>();
            return sgd;
        }

        public static SerialGameData LoadOrGenerate()
        {
            if (File.Exists(UnityEngine.Application.persistentDataPath  + Location))
            {
                var loaded = SerialGameData.Load();
                var mustBeSaved = false;
                /*
                if (loaded.levelSetInfoList == null)
                {
                    loaded.levelSetInfoList = new List<LevelSetMain.LevelSetSerialInfo>();
                    mustBeSaved = true;
                }

                if (mustBeSaved)
                {
                    loaded.Save();
                }
*/
               

                return loaded;
            }
            else
            {
                var data = DefaultData();
                data.Save();
                return  data;
            }
        }
        
        
        public static SerialGameData Load()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = 
                File.Open(UnityEngine.Application.persistentDataPath + Location, FileMode.Open);
            SerialGameData data = (SerialGameData)bf.Deserialize(file);
            file.Close();
            return data;
        }


        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream file = File.Create(UnityEngine.Application.persistentDataPath + Location); 
            bf.Serialize(file, this);
            file.Close();
        }

        public static void Apply(Action<SerialGameData> a, bool andSave=true)
        {
            var sgd = LoadOrGenerate();
            a(sgd);
            if (andSave)
            {
                sgd.Save();
            }
        }
        
        
        
        
        
    }
}