using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class Gun : MonoBehaviour
{
    [SerializeField] private float rangeL = 100f;
    [SerializeField] private float damageL = 10f;
    [SerializeField] private float impactForceL = 30f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private bool shootLAvailable = true;
    [SerializeField] private bool shootBAvailable = true;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Animator camera;
    [SerializeField] private AudioSource lazerAudio;
    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private AudioSource reloadAudio;
    [SerializeField] private PlayerController playercontroller;
    public bool isBumped;


    void Update()
    {
        if (InputManager.Devices.Count <= playercontroller.index)
        {
            return;
        }

        if (shootLAvailable)
        {
            var device = InputManager.Devices[playercontroller.index];
            if (device.RightBumper.IsPressed)
            {
                ShootL();
                StartCoroutine(FireRateL());
            }
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
    
    //Shoot a straight line laser which strongly bump if it hits the opponent
    void ShootL()
    {
        lazerAudio.Play();
        camera.Play("Shake");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangeL))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                StartCoroutine(BumpKill());
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForceL, ForceMode.Impulse);
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * rangeL);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * rangeL);
        }
        
        StartCoroutine(LineTime());
    }

    //Add a cooldown for the laser shoot
    IEnumerator FireRateL()
    {
        shootLAvailable = false;
        yield return new WaitForSeconds(3.00f);
        shootLAvailable = true;
        reloadAudio.Play();
    }

    //Add a very short cooldown to avoid spamming the bullet shoot
    IEnumerator FireRateB()
    {
        shootBAvailable = false;
        yield return new WaitForSeconds(0.25f);
        shootBAvailable = true;
    }

    //The laser animation stay for a few time
    IEnumerator LineTime()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }

    //Track the bumped state(laser) of the players
    IEnumerator BumpKill()
    {
        isBumped = true;
        Debug.Log("IsBumped");
        yield return new WaitForSeconds(2f);
        isBumped = false;
    }
}
