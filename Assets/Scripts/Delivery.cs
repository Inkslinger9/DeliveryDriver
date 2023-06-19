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
    [SerializeField] GameObject[] boostPickups;
    [SerializeField] float boostSpawnTimer;

    [SerializeField] Image arrowImage;
    private Transform activeCustomer;

    private Driver driver;


    private void Start()
    {

        driver = GetComponent<Driver>();

        pickUps = GameObject.FindGameObjectsWithTag("Pickup");
        customers = GameObject.FindGameObjectsWithTag("Customer");
        boostPickups = GameObject.FindGameObjectsWithTag("Boost");

        DeactivateAllBoostPickups();
        AllCustomersVisable(hasPackage);

        StartCoroutine(ActivateRandomBoostTimer());

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
        }else if (collision.tag == "Boost")
        {
            driver.ApplyBoost();
            collision.gameObject.SetActive(false);
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




    private IEnumerator ActivateRandomBoostTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(boostSpawnTimer);

            DeactivateAllBoostPickups();

            ActivateRandomBoostPickup();

        }

    }


    private void DeactivateAllBoostPickups()
    {
        foreach (GameObject boostPickup in boostPickups)
        {
            boostPickup.SetActive(false);
        }
    }

    private void ActivateRandomBoostPickup()
    {
       
        int randomIndex = UnityEngine.Random.Range(0, boostPickups.Length);

        
        boostPickups[randomIndex].SetActive(true);
    }


}
