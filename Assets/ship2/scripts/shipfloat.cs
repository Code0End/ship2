using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipfloat : MonoBehaviour
{
    public bool on = true;
    public AnimationCurve lacurvo;

    void Update()
    {
        if (on == true)
        {
            transform.position = new Vector3(transform.position.x, lacurvo.Evaluate((Time.time)), transform.position.z);
        }
    }
}
