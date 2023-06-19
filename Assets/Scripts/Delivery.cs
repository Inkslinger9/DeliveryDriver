using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    
    bool hasPackage = false;
    [SerializeField] GameObject[] pickUps;
    [SerializeField] GameObject[] customers;

    [SerializeField] Image arrowImage;
    private Transform activeCustomer;


    private void Start()
    {
        pickUps = GameObject.FindGameObjectsWithTag("Pickup");
        customers = GameObject.FindGameObjectsWithTag("Customer");
        AllCustomersVisable(hasPackage);
    }




    private void Update()
    {
        AllPickupsVisable(!hasPackage);


        if (activeCustomer != null)
        {
            arrowImage.enabled = true;

            Vector2 direction = activeCustomer.position - transform.position;

            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            
            arrowImage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            arrowImage.enabled = false;
        }
       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Pickup")
        {

            hasPackage = true;
            int randomIndex = UnityEngine.Random.Range(0, customers.Length);
            customers[randomIndex].SetActive(true);
            SetActiveCustomer(customers[randomIndex].transform);
            

        }
        else if (collision.tag == "Customer")
        {
            hasPackage = false;
            AllCustomersVisable(hasPackage);
            SetActiveCustomer(null);
        }
        
       
        
    }







    private void AllPickupsVisable(bool status)
    {
        
        
        foreach (GameObject obj in pickUps)
        {
            obj.SetActive(status);
        }
    }


    private void AllCustomersVisable(bool status)
    {
    
        foreach (GameObject obj in customers)
        {
            obj.SetActive(status);

        } 
       
    }

    public void SetActiveCustomer(Transform customerTransform)
    {

        activeCustomer = customerTransform;
    }



}
