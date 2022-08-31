using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        private const string Location = "/sockMonsterData.dat";


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
        
        
        
        
        
    }
}