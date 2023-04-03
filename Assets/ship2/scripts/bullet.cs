using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 8f;

    private float _timer = 0f;

    public GameObject explosion;

    public float damage = 35f;

    public int type;

    void Update()
    {
        if (type != 3)
        {
            transform.position += -transform.up * speed * Time.deltaTime;

            _timer += Time.deltaTime;
            if (_timer > lifetime)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            //la ancla que circula el barco va aqui xd
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (type == 1)
            {
                GameObject vfx_exp = Instantiate(explosion, new Vector3(transform.position.x, 0.8f, transform.position.z), transform.rotation);
                Destroy(gameObject);
            }
            if(type == 2)
            {
                collision.gameObject.GetComponent<Enemy>().taked(damage);
                Destroy(gameObject);
            }
            if(type == 3)
            {
                collision.gameObject.GetComponent<Enemy>().taked(damage);
            }
        }       
    }
}
