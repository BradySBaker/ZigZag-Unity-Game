using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{

    public GameObject player;

    Vector3 cameraOffset;
    void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }
    void Update()
    {
        transform.position = cameraOffset + player.transform.position;
    }
}
