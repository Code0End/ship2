using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Rigidbody2D rb;
    private Transform player;
    private SpriteRenderer sprite;

    public float moveSpeed = 0.5f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    void Update() {
        // Sprite orientation
        if(player.position.x > transform.position.x) {
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }

        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
