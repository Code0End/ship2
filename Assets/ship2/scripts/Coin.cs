using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject player;
    private bool isMoving = false;
    
    public float range = 5f;
    public float moveSpeed = 2f;
    public int value =  1;

    // Start is called before the first frame update
    void Start() {
        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (player == null)
        {
            return;
        }
        if (Vector3.Distance(this.transform.position, player.transform.position) <= range) {
            isMoving = true;
        }
    }

    private void FixedUpdate() {
        if (player == null)
        {
            return;
        }
        if (isMoving) {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            GameObject player = collision.gameObject;
            player.GetComponent<Player>().AddCoin(this.value);
            Destroy(gameObject);
        }
    }
}
