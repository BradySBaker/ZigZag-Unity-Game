using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public GameManager gameManager;
    public TerrainController terrainController;

    bool onGround = true;
    float? prevY;
    float inAirTime = 0;

    public float speed = 3;

    public float speedChange = .03f;

    void Start() {
    }

    void FixedUpdate() {
        if (!gameManager.gameStarted) {
            return;
        }
        if (speedChange > .005) {
            speedChange *= Mathf.Pow(.99f, Time.deltaTime);
        }
        if (speed < 7) {
            speed += speedChange * Time.deltaTime;
        }
        Debug.Log(speed);
        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;

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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "block") {
            terrainController.CheckBlockAndHandleSpawnAndDestroy(collision.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Fruit") {
            Destroy(other.gameObject);
            gameManager.AddPoint();
        }
    }
}
