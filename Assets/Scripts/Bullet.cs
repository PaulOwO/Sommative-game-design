using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    
    //The Bullet is destroyed if it collides with something else
    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("hit");
        Destroy(gameObject);
    }
    
    
    
    
    /*
                                                    //Unused code//

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
           // other.transform.isBumpedB = true;
           // StartCoroutine(BumpKillB());
        }
    }
    
    //Track the bumped state(bullet) of the players
    IEnumerator BumpKillB()
    {
        gameManager.isBumpedB = true;
        Debug.Log("IsBumpedB");
        yield return new WaitForSeconds(2f);
        gameManager.isBumpedB = false;
    }*/

}
