using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cam_respawn : MonoBehaviour
{

    private float lasclicktime;
    private const float doubleclicktime = .2f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float timesincelastclick = Time.time - lasclicktime;

            if(timesincelastclick <= doubleclicktime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {

            }
            lasclicktime = Time.time;
        }
    }
}
