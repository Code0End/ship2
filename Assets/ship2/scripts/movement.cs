using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movement : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    private string currentState;

    private float savedX;
    private float savedY;

    const string PN = "n";
    const string PS = "s";
    const string PE = "o";
    const string PO = "e";
    const string PNE = "nne";
    const string PNO = "no";
    const string PSE = "es";
    const string PSO = "os";

    public TMP_Text Centertxt;
    public bool ON = false;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Centertxt.text = "";
            ON = true;
        }
    }

    void FixedUpdate()
    {       
        if (ON == false)       
            return;
        
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.z += input.y;

                savedX = input.x;
                savedY = input.y;

                StartCoroutine(Move(targetPos));
                checkDirection(input.x, input.y);
            }
            else{
                checkDirection(savedX, savedY);
            }                       
        }
    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

    void checkDirection(float x, float y)
    {
        if (x >= 1 && y < 1 && y > -1){
            ChangeAnimationState(PE);
            return;
        }
        if (x >= -1 && y < 1 && y > -1)
        {
            ChangeAnimationState(PO);
            return;
        }

        if (y >= 1 && x < 1 && x > -1)
        {
            ChangeAnimationState(PN);
            return;
        }
        if (y >= -1 && x < 1 && x > -1)
        {
            ChangeAnimationState(PS);
            return;
        }

        if (x >= 1 && y >= 1)
        {
            ChangeAnimationState(PNO);
            return;
        }
        if (x >= 1 && y >= -1)
        {
            ChangeAnimationState(PSO);
            return;
        }
        if (x >= -1 && y >= 1)
        {
            ChangeAnimationState(PNE);
            return;
        }
        if (x >= -1 && y >= -1)
        {
            ChangeAnimationState(PSE);
            return;
        }
    }
}
