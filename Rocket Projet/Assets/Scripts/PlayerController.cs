using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float rocketSpeed;
    public float minRocketSpeed;
    public float maxRocketSpeed;
    public float rotateSpeed;

    public float transitionSpeed = 3f;

    public Image BatteryPowerBar;

    public float maxBatteryPower = 20;
    public float batteryPower = 20;

    public GameObject youWinText;
    public GameObject beginningText;

    public GameObject explosionEffect;

    private bool hasGamesStart = false;

    private void Start()
    {
        beginningText.SetActive(true);
        hasGamesStart = false;
        rocketSpeed = 0;
    }

    private void Update()
    {
        if (hasGamesStart == false)
        {
            if (Input.anyKeyDown)
            {
                hasGamesStart = true;
                beginningText.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        if (hasGamesStart == true)
        {
            if (batteryPower > 0)
            {
                transform.Translate(Vector2.up * rocketSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.Space))
                {
                    rocketSpeed = Mathf.Lerp(rocketSpeed, maxRocketSpeed, Time.deltaTime * transitionSpeed);
                }
                else
                {
                    rocketSpeed = minRocketSpeed;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
                }

                batteryPower -= Time.deltaTime;
            }
        }

        BatteryPowerBar.fillAmount = (float)batteryPower / (float) maxBatteryPower;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Battery")
        {
            batteryPower += 5;
            if(batteryPower > maxBatteryPower)
            {
                batteryPower = maxBatteryPower;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Goal")
        {
            youWinText.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
