using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Driver : MonoBehaviour
{


    [SerializeField] float rotateSpeed = 150f;
    [SerializeField] float boostMoveSpeed = 50f;
    [SerializeField] float baseMoveSpeed = 20f;
    [SerializeField] float boostTime = 1f;

    float currentMoveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = baseMoveSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    public void Movement()
    {
        float steerAmount = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;

        transform.Translate(0, moveAmount, 0);



        if (moveAmount != 0)
        {

            transform.Rotate(0, 0, -steerAmount);

        }
    }

   public void ApplyBoost()
    {
        StartCoroutine(BoostDuration());
    }



    
    private IEnumerator BoostDuration()
    {

        currentMoveSpeed = boostMoveSpeed;

        yield return new WaitForSeconds(boostTime);

        currentMoveSpeed = baseMoveSpeed;

    }


    
}


