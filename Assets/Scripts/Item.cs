using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject[] item;
    int randomItem;

    float elapsedTime;
    public float timeLimit = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= timeLimit)
        {
            elapsedTime = 0;
            //Rest of your code or function call goes here
            spawItem();
        }
    }
    void spawItem()
    {
        int num = Random.Range(1, 12);
        if(num == 1 || num == 2)
        {
            randomItem = 2;
        }
        else
        {
            if(num == 3 || num == 4 || num == 5 || num == 6)
            {
                randomItem = 1;
            }
            else
            {
                randomItem = 0;
            }
        }
        GameObject newItem = Instantiate(item[randomItem], new Vector2(transform.position.x + num / 5, transform.position.y + 2), transform.rotation);
        newItem.GetComponent<Item>();
    }



}
