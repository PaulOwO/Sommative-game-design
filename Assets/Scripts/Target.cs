
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class Target : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource victoryMusic;
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject endScreenUI;
    [SerializeField] private GameObject p1WinUI;
    [SerializeField] private GameObject p2WinUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private bool deathEnable = true;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private bool isBumpedB = true;
    




    void Update()
    {
        
        if (gameManager.restartEnable == true)
        {
            var device = InputManager.Devices[playerController.index];
            if (device.Action1.WasPressed)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    //Destroy the player gameobject
    void Death()
    {
        gameMusic.Stop();
        victoryMusic.Play();
        deathEnable = false;
        endScreenUI.SetActive(true);
        deathSound.Play();
        gameManager.restartEnable = true;
        Destroy(gameObject);
    }

    void DeathP2()
    {
        Debug.Log("player 2 death");
        p2WinUI.SetActive(true);
        Death();
    }

    void DeathP1()
    {
        Debug.Log("player 1 death");
        p1WinUI.SetActive(true);
        Death();
    }
    
    //Kill the player if he got hit by a shoot and bumped into a deathwall
    private void OnTriggerEnter(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            if (deathEnable == true)
            {
                if (gameObject == player2)
                {
                    DeathP1();
                }
                else
                {
                    DeathP2();
                }
            }
        }
    }

    //Kill the player if he got hit by a shoot when currently colliding with a deathwall
    private void OnTriggerStay(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            if (deathEnable == true)
            {
                if (gameObject == player2)
                {
                    DeathP1();
                }
                else
                {
                    DeathP2();
                }
            }
        }
    }
}
