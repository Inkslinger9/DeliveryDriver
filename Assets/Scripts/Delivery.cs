using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    
    bool hasPackage = false;
    [SerializeField] GameObject[] pickUps;
    [SerializeField] GameObject[] customers;
    [SerializeField] GameObject[] boostPickups;
    [SerializeField] float boostSpawnTimer;
    [SerializeField] int scoreIncreasePerDelivery = 5;

    [SerializeField] Image arrowImage;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText;
    [SerializeField] TMP_Text timerText;
    private Transform activeCustomer;

    int score = 0;
    public float timeRemaining = 60;
    public bool gameOver = false;
    [SerializeField] GameObject gameHud;
    [SerializeField] GameObject gameOverScreen;



    private Driver driver;






    private void Start()
    {
        score = 0;
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




         if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = Convert.ToInt32 (timeRemaining).ToString();
            
            if (timeRemaining <= 0)
            {
                gameOver = true;
                Time.timeScale = 0;
                

            }
            
            
        }

        if (gameOver)
        {

            gameHud.SetActive(false);
            gameOverScreen.SetActive(true);
            finalScoreText.text = score.ToString();
            Debug.Log("Game is over");
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
            score += scoreIncreasePerDelivery;
            scoreText.text = score.ToString();


        }else if (collision.tag == "Boost")
        {
            driver.ApplyBoost();
            collision.gameObject.SetActive(false);
        }
        
       
        
    }







    protected void AllPickupsVisable(bool status)
    {
        
        
        foreach (GameObject obj in pickUps)
        {
            obj.SetActive(status);
        }
    }


    protected void AllCustomersVisable(bool status)
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


    protected void DeactivateAllBoostPickups()
    {
        foreach (GameObject boostPickup in boostPickups)
        {
            boostPickup.SetActive(false);
        }
    }

    protected void ActivateRandomBoostPickup()
    {
       
        int randomIndex = UnityEngine.Random.Range(0, boostPickups.Length);

        
        boostPickups[randomIndex].SetActive(true);
    }



    public void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {

        Application.Quit();
    }

}
