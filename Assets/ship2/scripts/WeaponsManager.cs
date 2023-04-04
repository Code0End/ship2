using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    public float HP = 100f;
    public Camera Cam;
    public TMP_Text Centertxt;
    public Image healthbar;
    public Image XPbar;

    public Player Preference;

    public cam_respawn CR;

    public bool CisActive;
    public float CfireRate;
    private float timeSinceLastFire = 0f;
    public float CturningSpeed;
    public float Cdamage = 100f;

    public GameObject Cpivot;
    public GameObject Cobject;
    public GameObject Cbullet;
    public GameObject Cnozzle;

    public bool BisActive;
    public float BfireRate;
    private float BtimeSinceLastFire = 0f;
    public float BturningSpeed;
    public float Bdamage = 35f;

    public GameObject Bpivot;
    public GameObject Bobject;
    public GameObject Bbullet;
    public GameObject Bnozzle;
    private GameObject closestEnemy;

    public bool DisActive;
    public float DturningSpeed;
    public GameObject Dpivot;
    public float Ddamage = 20f;
    public float DfireRate;
    private float DtimeSinceLastFire = 0f;

    const string SHOOTING = "shooting";
    const string BALLISTA_SHOOTING = "ballista_shooting";
    const string IDLE = "New State";
    private string currentState;

    public Animator Canimator;
    public Animator Banimator;

    void FixedUpdate()
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
        }
        if (BisActive == true)
        {
            Ballista_turn();
            BtimeSinceLastFire += Time.deltaTime;
            if (BtimeSinceLastFire >= BfireRate)
            {
                Ballista_fire();
                BtimeSinceLastFire = 0f; // Reset the timer
            }
        }
        if (DisActive == true)
        {
            Anchor_turn();
            DtimeSinceLastFire += Time.deltaTime;
            if (DtimeSinceLastFire >= DfireRate)
            {
                Anchor_fire();
                DtimeSinceLastFire = 0f; // Reset the timer
            }
            if (DtimeSinceLastFire >= 0.05f)
            {
                Anchor_stop();
            }
        }
    }

    public void Cannon_fire()
    {
        ChangeAnimationState(SHOOTING, Canimator);
        GameObject projectile = Instantiate(Cbullet, Cnozzle.transform.position, Cnozzle.transform.rotation);
        projectile.GetComponent<bullet>().update_damage(Cdamage);
    }
    public void Ballista_fire()
    {
        ChangeAnimationState(BALLISTA_SHOOTING, Banimator);
        GameObject projectile = Instantiate(Bbullet, Bnozzle.transform.position, Bnozzle.transform.rotation);
        projectile.GetComponent<bullet>().update_damage(Bdamage);
    }
    public void Anchor_fire()
    {
        Dpivot.GetComponentInChildren<BoxCollider>().isTrigger = false;
    }
    public void Anchor_stop()
    {
        Dpivot.GetComponentInChildren<BoxCollider>().isTrigger = true;
    }

    public void Cannon_turn()
    {
        Cpivot.transform.Rotate(Vector3.up, CturningSpeed * Time.deltaTime);
    }

    public void Anchor_turn()
    {
        Dpivot.transform.Rotate(Vector3.up, DturningSpeed * Time.deltaTime);
    }

    public void Ballista_turn()
    {
        float closestDistance = Mathf.Infinity;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestEnemy.activeInHierarchy)
        {
            Vector3 directionToTarget = closestEnemy.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(-directionToTarget);
            Bpivot.transform.rotation = Quaternion.Slerp(Bpivot.transform.rotation, targetRotation, BturningSpeed * Time.deltaTime);
            return;
        }

        if (closestEnemy == null)
        {
            return;
        }
    }

    public void set_anchor_active()
    {
        Dpivot.SetActive(true);
        DisActive = true;
        Preference.anchor_a = false;
        Preference.upgrades_off();
    }

    public void set_ballista_active()
    {
        Bpivot.SetActive(true);
        BisActive = true;
        Preference.ballista_a = false;
        Preference.upgrades_off();
    }

    public void upgrade_cdamage(float u)
    {      
            Cdamage += u;
            Preference.upgrades_off();
    }
    public void upgrade_bdamage(float u)
    {
        Bdamage += u;
        Preference.upgrades_off();
    }
    public void upgrade_ddamage(float u)
    {
        Ddamage += u;
        Preference.upgrades_off();
    }

    public void upgrade_Crate(float u)
    {
        CfireRate -= u;
        Preference.upgrades_off();
    }
    public void upgrade_Brate(float u)
    {
        BfireRate -= u;
        Preference.upgrades_off();
    }
    public void upgrade_Drate(float u)
    {
        if (DfireRate - u <= 0.1f)
            Preference.upgrades_off();
        else
        {
            DfireRate -= u;
            Preference.upgrades_off();
        }
    }

    public void upgrade_Cturn(float u)
    {
        CturningSpeed += u;
        Preference.upgrades_off();
    }
    public void upgrade_Dturn(float u)
    {
        DturningSpeed -= u;
        Preference.upgrades_off();
    }

    void ChangeAnimationState(string newState, Animator a)
    {
        if (currentState == newState) return;

        a.Play(newState);

        currentState = newState;
    }

    public void taked(float d)
    {
        HP = HP - d;
        UpdateHealth(HP / 100);
        if (HP <= 0)
        {
            //ass.clip = gothit;
            //ass.pitch = UnityEngine.Random.Range(0.2f, 0.6f);
            //ass.Play();
            //if (GameObject.FindGameObjectWithTag("text1") != null)
            //{
            //GameObject.FindGameObjectWithTag("text1").GetComponent<reloj>().updatenumenemies();
            //}
            //WM.Enemies.Remove(gameObject.GetComponent<Enemy>());
            Cam.transform.parent = null;
            CR.enabled = true;
            Centertxt.text = "PRESS SPACEBAR TO RESTART";
            Destroy(gameObject, 0.10f);
        }
    }

    public void UpdateHealth(float fraction)
    {
        healthbar.fillAmount = fraction;
    }

    public void UpdateXP(float fraction) {
        XPbar.fillAmount = fraction;
    }
}

