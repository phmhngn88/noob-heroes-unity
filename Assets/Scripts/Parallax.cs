using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //This script is used to loop background
    private float length, startpos;
    public GameObject cam;
    public float parrallaxEffect;
    // Start is called before the first frame update
    void Start() 
    {
        startpos = transform.position.x;
        //get size of sprite
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parrallaxEffect));
        float distance = (cam.transform.position.x * parrallaxEffect);
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        if (temp > startpos + length)
            startpos += length;
        else if (temp < startpos - length)
            startpos -= length;
    }
}
