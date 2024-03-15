using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class FruitController : MonoBehaviour {

    float yRotation = 0;
    Vector3 startPoint;
    Vector3 endPoint;
    float elapsedTime = 0;

    public Mesh[] meshes;

    void Start () {
        startPoint = transform.position;
        endPoint = startPoint + transform.up/2;

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null | meshes.Length == 0) {
            Debug.LogError("Please add a meshFilter and a list of meshes to the script");
            return;
        }

        int randomIndex = Random.Range(0, meshes.Length);
        meshFilter.mesh = meshes[randomIndex];
    }

    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float t = elapsedTime / 2;

        yRotation += Time.deltaTime * 30;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.SmoothStep(0f, 1f, t));

        if (transform.position == endPoint) {
            endPoint = startPoint;
            startPoint = transform.position;
            elapsedTime = 0;
        }
    }
}
