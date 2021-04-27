using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class GunBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private bool shootBAvailable = true;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerController playercontroller;
   



    void Update()
    {
        if (InputManager.Devices.Count <= playercontroller.index)
        {
            return;
        }

        if (shootBAvailable)
        {

            var device = InputManager.Devices[playercontroller.index];
            if (device.RightTrigger.IsPressed)
            {
                ShootB();
                StartCoroutine(FireRateB());
            }
        }
    }

    //Shoot a straight line bullet which slightly bump if it hits the opponent
    void ShootB()
    {
        shootAudio.Play();
        GameObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.transform.position = transform.position;
        Rigidbody body = bulletInstance.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }

    //Add a very short cooldown to avoid spamming the bullet shoot
    IEnumerator FireRateB()
    {
        shootBAvailable = false;
        yield return new WaitForSeconds(0.25f);
        shootBAvailable = true;
    }
}
