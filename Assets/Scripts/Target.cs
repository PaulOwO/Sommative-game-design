
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private Gun gun;
    [SerializeField] private Bullet bullet;
    [SerializeField] private AudioSource deathSound;
    

    //Destroy the player gameobject
    void Death()
    {
        Destroy(gameObject);
        deathSound.Play();
    }
    
    //Kill the player if he got hit by a shoot and bumped into a deathwall
    private void OnTriggerEnter(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Death();
        }

        if ((bullet.isBumpedB) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Death();
        }
    }

    //Kill the player if he got hit by a shoot when currently colliding with a deathwall
    private void OnTriggerStay(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Death();
            SceneManager.LoadScene("SampleScene");
        }

        if ((bullet.isBumpedB) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Death();
            SceneManager.LoadScene("SampleScene");
        }
    }
}
