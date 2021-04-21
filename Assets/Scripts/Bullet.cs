using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private gun gun;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(gameObject);
        //gun.StartCoroutine(BumpKill());
    }




   
}
