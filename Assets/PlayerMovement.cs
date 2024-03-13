using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Start() {

    }

    void FixedUpdate() {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = currentPosition + transform.forward * 2 * Time.deltaTime;

        transform.position = newPosition;

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            transform.rotation = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0);
        }
    }
}
