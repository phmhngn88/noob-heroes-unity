using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;

    public TMP_Text ammo0;
    public TMP_Text ammo1;
    public TMP_Text ammo2;
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopPanelsGO;
    public ShopTemplate[] shopPanels;

    public GameObject mainMenu;

    public Button[] myPurchaseBtns;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanelsGO[i].SetActive(true);
        }

        coinUI.text = "Coins: " + coins.ToString();
        LoadPanels();
        checkPurchaseable();

        LoadGame();
        setAmmoTxt();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCoin()
    {
        coins++;
        coinUI.text = "Coins: " + coins.ToString();
        checkPurchaseable();
    }

    public void checkPurchaseable()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            if (coins >= shopItemSO[i].basecost)
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopPanels[i].titleTxt.text = shopItemSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemSO[i].description;
            shopPanels[i].costTxt.text = shopItemSO[i].basecost.ToString();
        }
    }
    public void PurchaseItem(int btnNo)
    {
        if (coins >= shopItemSO[btnNo].basecost)
        {
            coins = coins - shopItemSO[btnNo].basecost;
            coinUI.text = "Coins: " + coins.ToString();
            checkPurchaseable();
        }

        switch (btnNo)
        {
            case 0:
                BowController.numberOfAmmo[2]++;
                break;
            case 1:
                BowController.numberOfAmmo[1]++;
                break;
            case 2:
                BowController.numberOfAmmo[0]++;
                break;
        }

        setAmmoTxt();
        SaveAmmo();
    }

    public void LoadGame()
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
            this.coins = data.coin;

            Debug.Log("Loaded game " + path);
        }
    }

    public void SaveAmmo()
    {
        string path = Path.Combine(Application.persistentDataPath, "ammodata.hd");
        FileStream file = File.Create(path);

        AmmoData data = new AmmoData(BowController.numberOfAmmo, coins);


        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, data);
        file.Close();

        Debug.Log("Save game " + path);
    }

    public void setAmmoTxt()
    {
        ammo0.text = ": " + BowController.numberOfAmmo[0].ToString();
        ammo1.text = ": " + BowController.numberOfAmmo[1].ToString();
        ammo2.text = ": " + BowController.numberOfAmmo[2].ToString();
    }

    public IEnumerator SetActive(Scene scene)
    {
        int i = 0;
        while (i == 0)
        {
            i++;
            yield return null;
        }
        SceneManager.SetActiveScene(scene);
        yield break;
    }


    public void Close()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        Scene Game_scene = SceneManager.GetSceneByName("Menu");
        StartCoroutine(SetActive(Game_scene));

        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }
}
