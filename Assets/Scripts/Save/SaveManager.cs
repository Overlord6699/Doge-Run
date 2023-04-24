using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { get { return instance; } }
        private static SaveManager instance;

        public SaveData SaveData { get; private set; }



        public Action<SaveData> OnLoad; 
        public Action OnSave;

        private BinaryFormatter _formatter;
        private string _filePath;

        public void CreateSaveData(int highScore, int money)
        {
            SaveData.Highscore = highScore;
            SaveData.Money = money;
            SaveData.LastSave = DateTime.Today;
        }
        
        private void Awake()
        {
            instance = this;
            _filePath = Application.persistentDataPath + "/Save.json";
            _formatter = new BinaryFormatter();
        }

        public void Load()
        {
            try
            {
                FileStream file = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                SaveData = (SaveData)_formatter.Deserialize(file);
                file.Close();
                OnLoad?.Invoke(SaveData);
            }
            catch
            {
                Debug.Log("Save file not found! Creating new file");
                Save();
            }
        }

        public void Save()
        {
            // If theres no previous state found, create a new one!
            if (SaveData == null)
                SaveData = new SaveData();

            // Set the time at which we've tried saving
            SaveData.LastSave = DateTime.Now;

            // Open a file on our system, and write to it
            FileStream file = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.Write);
            _formatter.Serialize(file, SaveData);
            file.Close();

            OnSave?.Invoke();
        }
    }
}
