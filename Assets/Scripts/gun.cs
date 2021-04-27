using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class Gun : MonoBehaviour
{
    [SerializeField] private float rangeL = 100f;
    [SerializeField] private float impactForceL = 30f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private bool shootLAvailable = true;
    [SerializeField] private Animator camera;
    [SerializeField] private AudioSource lazerAudio;
    [SerializeField] private AudioSource reloadAudio;
    [SerializeField] private PlayerController playerController;
    public bool isBumped = false;


    void Update()
    {
        if (InputManager.Devices.Count <= playerController.index)
        {
            return;
        }

        if (shootLAvailable)
        {
            var device = InputManager.Devices[playerController.index];
            if (device.RightBumper.IsPressed)
            {
                ShootL();
                StartCoroutine(FireRateL());
            }
        } 
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
