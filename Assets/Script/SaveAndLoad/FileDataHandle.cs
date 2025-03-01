using System;
using System.IO;
using UnityEngine;

public class FileDataHandle 
{
    private string dataDirPath = "";
    private string dataFileName = "";
    private bool encryptData = false;
    private string codeWord = "@Sktt9123@";
    public FileDataHandle(string _dataDirPath,string _dataFileName,bool _enCryptData)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
        encryptData = _enCryptData;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            if(encryptData)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath,dataFileName);

        GameData loadData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }catch(Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
        return loadData;
    }
    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryptDecrypt(string _data)
    {
        string modifierData = "";
        for(int i = 0; i < _data.Length; i++)
        {
            modifierData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
        }
        return modifierData;
    }
}
