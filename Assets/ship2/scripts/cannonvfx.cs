using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonvfx : MonoBehaviour
{
    public float damage = 100f;
    public float delay = 0.8f;
    void Start()
    {
        Destroy(transform.parent.gameObject, delay); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().taked(damage);
        }
    }

    public void update_damage(float d)
    {
        damage = d;
    }
}
