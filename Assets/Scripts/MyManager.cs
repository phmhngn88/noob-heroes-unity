using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class MyManager
{
    public bool isNewGame;

    public int coin;
    public int sceneLevel;

    //Singleton
    private static MyManager _instance;

    public static MyManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MyManager();

            return _instance;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SaveGame(GameObject player)
    {
        string path = Path.Combine(Application.persistentDataPath, "playerdata.hd");
        FileStream file = File.Create(path);

        PlayerController playerController = player.GetComponent<PlayerController>();

        PlayerData data = new PlayerData(playerController.GetHealth(), playerController.transform.position, MyManager.Instance.coin, SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Scenessssssssssssssssssssssssssssss: " + SceneManager.GetActiveScene().buildIndex);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, data);
        file.Close();

        Debug.Log("Save game " + path);
    }

    public void SaveAmmo()
    {
        string path = Path.Combine(Application.persistentDataPath, "ammodata.hd");
        FileStream file = File.Create(path);

        AmmoData data = new AmmoData(BowController.numberOfAmmo, coin);


        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, data);
        file.Close();

        Debug.Log("Save game " + path);
    }


    public void LoadGame(PlayerController rubyController)
    {
        string path = Path.Combine(Application.persistentDataPath, "playerdata.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
            file.Close();

            //Load data
            rubyController.setHealth(data.health);
            rubyController.transform.position = new Vector2(data.position[0], data.position[1]);
            MyManager.Instance.coin = data.coin;
            sceneLevel = data.sceneIndex;
            Debug.Log(sceneLevel);
            Debug.Log("Loaded game " + path);
            Debug.Log("Loaded game " + sceneLevel);
        }
    }    
    public void LoadScene()
    {
        string path = Path.Combine(Application.persistentDataPath, "playerdata.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            PlayerData data = (PlayerData)binaryFormatter.Deserialize(file);
            file.Close();

            //Load data

            sceneLevel = data.sceneIndex;
            Debug.Log(sceneLevel);
            Debug.Log("Loaded game " + path);
            Debug.Log("Loaded game " + sceneLevel);
        }
    }

    public void LoadAmmo()
    {
        string path = Path.Combine(Application.persistentDataPath, "ammodata.hd");
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            AmmoData data = (AmmoData)binaryFormatter.Deserialize(file);
            file.Close();

            //Load data

            BowController.numberOfAmmo = new int[] { data.numberOfAmmo[0], data.numberOfAmmo[1], data.numberOfAmmo[2] };
            this.coin = data.coin;

            Debug.Log("Loaded game " + path);
        }
    }

}
