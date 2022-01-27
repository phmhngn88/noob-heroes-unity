using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    private void Awake()
    {
        GoToNextLevel();
    }

    void GoToNextLevel()
    {
        Debug.Log("Go to level " + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }
}


