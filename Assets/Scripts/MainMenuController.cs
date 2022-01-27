using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject player;

    public GameObject bow;

    private void Awake()
    {
        //this.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    public void PlayGame()
    {
        MyManager.Instance.isNewGame = true;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Scene Game_scene = SceneManager.GetSceneByName("Paralax");
        //StartCoroutine(SetActive(Game_scene));
        Debug.Log("Play game");
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void PauseGame()
    {
        MyManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        MyManager.Instance.ResumeGame();
        this.gameObject.SetActive(false);
        Debug.Log("Resume");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        Scene Game_scene = SceneManager.GetSceneByName("Menu");
        StartCoroutine(SetActive(Game_scene));

        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    public void LoadStore()
    {
        SceneManager.LoadScene("StoreMenu", LoadSceneMode.Additive);
        Scene Game_scene = SceneManager.GetSceneByName("StoreMenu");
        StartCoroutine(SetActive(Game_scene));

        Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
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

    public void SaveGame()
    {
        MyManager.Instance.SaveGame(player);
        MyManager.Instance.SaveAmmo();
    }

    public void LoadGame()
    {
        MyManager.Instance.isNewGame = false;
        Time.timeScale = 1;
        MyManager.Instance.LoadScene();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Load game name" + MyManager.Instance.sceneLevel);
        SceneManager.LoadScene(MyManager.Instance.sceneLevel);
        this.gameObject.SetActive(false);
        Debug.Log("Load game" + MyManager.Instance.isNewGame);
        
    }
}
