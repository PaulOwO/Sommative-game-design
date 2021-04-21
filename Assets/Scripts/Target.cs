
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    [SerializeField] private gun gun;
    [SerializeField] private Bullet bullet;


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
            Destroy(gameObject);
        }

       if ((bullet.isBumpedB) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((gun.isBumped) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Destroy(gameObject);
        }

        if ((bullet.isBumpedB) && (other.gameObject.layer == LayerMask.NameToLayer("Killingzone")))
        {
            Destroy(gameObject);
        }
    }




}
