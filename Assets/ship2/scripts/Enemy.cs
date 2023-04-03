using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour {
    private Rigidbody rb;
    private Transform player;
    private SpriteRenderer sprite;
    private Vector3 lastPosition;
    private bool isMoving = true;

    public float moveSpeed;
    public float followDelay;
    public float strenght = 0.5f;

    public float hp = 100f;
    AudioSource ass;
    public AudioClip gothit;

    public WeaponsManager WM;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    void Update() {        

        followDelay -= Time.deltaTime;

        if (followDelay <= 0) {
            lastPosition = player.position;
            followDelay = 1f;
        }
        // Sprite orientation
        if (player.position.x > transform.position.x) {
            sprite.flipX = true;
        } else {
            sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.Q)) {
            Knockback();
        }
    }

    private void FixedUpdate() {
        // Movement
        if (isMoving) {
            transform.position = Vector3.MoveTowards(this.transform.position, lastPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void Knockback() {
        isMoving = false;
        
        Vector3 direction = (transform.position - player.transform.position).normalized;
        rb.AddForce(direction * strenght, ForceMode.Impulse);
        StartCoroutine(Reset());
    }

    public IEnumerator Reset() {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
        isMoving = true;
    }

    public void taked(float d)
    {
        hp = hp - d;
        if (hp <= 0)
        {
            //ass.clip = gothit;
            //ass.pitch = UnityEngine.Random.Range(0.2f, 0.6f);
            //ass.Play();
            //Debug.Log("DEATH");
            //if (GameObject.FindGameObjectWithTag("text1") != null)
            //{
            //GameObject.FindGameObjectWithTag("text1").GetComponent<reloj>().updatenumenemies();
            //}
            //WM.Enemies.Remove(gameObject.GetComponent<Enemy>());
            Destroy(gameObject,0.10f);
        }
    }

}
