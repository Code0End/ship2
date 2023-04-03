using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private GameObject player;
    private bool isMoving = false;
    
    public float range = 5f;
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if(Vector3.Distance(this.transform.position, player.transform.position) <= range) {
            isMoving = true;
        }
    }

    private void FixedUpdate() {
        if(isMoving) {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            Debug.Log("Earn XP");
            Destroy(gameObject);
        }
    }
}
