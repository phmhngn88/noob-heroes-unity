using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    public Transform cameraLookAt;
    public Vector3 offset;
    [SerializeField] private float speed;


    // Start is called before the first frame update

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,cameraLookAt.position, speed * Time.deltaTime);
        //transform.position = new Vector3(cameraLookAt.position.x + offset.x, cameraLookAt.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
