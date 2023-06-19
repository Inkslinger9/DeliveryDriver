using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Driver : MonoBehaviour
{


    [SerializeField] float rotateSpeed = 150f;
    [SerializeField] float moveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {


        float steerAmount = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(0, moveAmount, 0);



        if (moveAmount != 0)
        {

            transform.Rotate(0, 0, -steerAmount);

        }
        




        


        
    }

    
    
}
