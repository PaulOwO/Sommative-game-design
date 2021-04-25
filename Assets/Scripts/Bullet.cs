using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isBumpedB;
    

    //Track the bumped state(bullet) of the players
    IEnumerator BumpKillB()
    {
        isBumpedB = true;
        Debug.Log("IsBumpedB");
        yield return new WaitForSeconds(2f);
        isBumpedB = false;
    }
    
    //The Bullet is destroyed if it collides with something else
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(BumpKillB());
        }
    }

}
