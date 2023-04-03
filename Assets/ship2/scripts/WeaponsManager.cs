using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public bool CisActive;
    public float CfireRate;
    private float timeSinceLastFire = 0f;
    public float CturningSpeed;

    public GameObject Cpivot;
    public GameObject Cobject;
    public GameObject Cbullet;
    public GameObject Cnozzle;

    const string SHOOTING = "shooting";
    const string IDLE = "New State";
    private string currentState;

    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (CisActive == true)
        {
            Cannon_turn();
            timeSinceLastFire += Time.deltaTime;
            if (timeSinceLastFire >= CfireRate)
            {
                Cannon_fire();
                timeSinceLastFire = 0f; // Reset the timer
            }
            else
            {
                //ChangeAnimationState(IDLE);
            }
        }
            
               
           
           
        
    }

    public void Cannon_fire()
    {
        ChangeAnimationState(SHOOTING);
        //GameObject projectile = Instantiate(Cbullet, Cnozzle.transform.position, Quaternion.Euler(90,0,0));
        GameObject projectile = Instantiate(Cbullet, Cnozzle.transform.position, Cnozzle.transform.rotation);
    }

    public void Cannon_turn()
    {
        Cpivot.transform.Rotate(Vector3.up, CturningSpeed * Time.deltaTime);
    }

    public void setA()
    {
        this.CisActive = true;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}

