
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private gun gun;
    [SerializeField] private Bullet bullet;
    [SerializeField] private ArenaManager arenamanager;
    [SerializeField] AudioSource deathSound;


    /*public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }*/

    /*void Die()
    {
        Destroy(gameObject);
    }*/
    

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

    void Death()
    {
        Destroy(gameObject);
        deathSound.Play();
    }




}
