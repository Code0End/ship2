using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 8f;

    private float _timer = 0f;

    void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;

        _timer += Time.deltaTime;
        if (_timer > lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
