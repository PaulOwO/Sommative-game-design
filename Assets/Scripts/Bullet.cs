using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
  
    private const float bulletSpeed = 0.1f;
    [SerializeField] private GameObject firePoint;



    void Update()
    {
        //transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        transform.position += firePoint.transform.forward * bulletSpeed;
    }
}
