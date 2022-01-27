using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //[SerializeField] private int nextLevel;
    [SerializeField] string _nextLevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (SceneManager.GetSceneAt(nextLevel).IsValid())
            
            Debug.Log("Go to level " + _nextLevelName);
            SceneManager.LoadScene(_nextLevelName);
               
        }
    }
        
}
