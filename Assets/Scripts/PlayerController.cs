using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameManager gameManager;
    bool onGround = true;
    float? prevY;
    float inAirTime = 0;
    void Start() {
    }

    void FixedUpdate() {
        if (!gameManager.gameStarted) {
            return;
        }
        Vector3 forwardMovement = transform.forward * 2 * Time.deltaTime;

        Vector3 newPosition = transform.position + forwardMovement;

        transform.position = newPosition;

        if (inAirTime > 2) {
            gameManager.StopGame();
        }
    }

    private void Update() {
        if (!onGround) {
            inAirTime += Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gameManager.gameStarted) {
                gameManager.StartGame();
                return;
            }
            transform.rotation = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0);
        }

        float curY = transform.position.y; 
   
        if (prevY != null && Mathf.Abs(curY - (float)prevY) >= .05) {
            onGround = false; 
        } else {
            prevY = curY;
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Fruit") {
            Destroy(other.gameObject);
            gameManager.AddPoint();
        }
    }
}
