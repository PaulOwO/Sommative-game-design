
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class Target : MonoBehaviour
{
    [SerializeField] private Gun gun;
    //[SerializeField] private Bullet bullet;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject endScreenUI;
    [SerializeField] private GameObject p1WinUI;
    [SerializeField] private GameObject p2WinUI;
    [SerializeField] private bool restartEnable = false;
    [SerializeField] private PlayerController playercontroller;
    public bool isBumpedB = false;
    


    void Update()
    {
        if (restartEnable)
        {
            var device = InputManager.Devices[playercontroller.index];
            if (device.Action1.WasPressed)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    //Destroy the player gameobject
    void Death()
    {
        endScreenUI.SetActive(true);
        deathSound.Play();
        Destroy(gameObject);
        restartEnable = true;
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
            if (gameObject == player2)
            {
                DeathP1();
            }
            else
            {
                DeathP2();
            }
        }

        /*if ((bullet.isBumpedB) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            if (other == player2)
            {
                DeathP2();
            }

            if (other == player1)
            {
                DeathP1();
            }
        }*/
    }

    //Kill the player if he got hit by a shoot when currently colliding with a deathwall
    private void OnTriggerStay(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
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
