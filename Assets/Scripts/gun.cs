using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class gun : MonoBehaviour
{
    public float rangeL = 100f;
    public float damageL = 10f;
    public float impactForceL = 30f;
    public LineRenderer lineRenderer;
    public bool shootAvailable = true;
    public bool isBumped;
    public bool shootLAvailable = true;
    public bool shootBAvailable = true;
    public GameObject bulletPrefab;
    [SerializeField] AudioSource lazerAudio;
    [SerializeField] AudioSource shootAudio;
    [SerializeField] AudioSource reloadAudio;

    [SerializeField] private PlayerController playercontroller;
    private float bulletSpeed = 10f;
    public Animator camera;

    
    

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
                //Debug.Log("Shoot");
            }
        }

        if (shootBAvailable)
        {
            
            var device = InputManager.Devices[playercontroller.index];
            if (device.RightTrigger.IsPressed)
            {
                ShootB();
                StartCoroutine(FireRateB());
                //Debug.Log("Shoot");
            }
        }
        
        
    }

    void ShootB()
    {
        shootAudio.Play();
        GameObject bulletInstance = Instantiate(bulletPrefab);
        bulletInstance.transform.position = transform.position;
        //bulletInstance.transform.position += transform.forward;
        //Rigidbody body_ = bulletInstance.GetComponent<Rigidbody>();
        //body_.AddForce(impactForceL, ForceMode.Impulse);
        //Rigidbody bulletInstance = GetComponent<Rigidbody>();
        Rigidbody body = bulletInstance.GetComponent<Rigidbody>();
        body.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        //0f, 0f, 1f,
    }
    

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

    IEnumerator FireRateL()
    {
        shootLAvailable = false;
        yield return new WaitForSeconds(3.00f);
        shootLAvailable = true;
        reloadAudio.Play();
    }

    IEnumerator FireRateB()
    {
        shootBAvailable = false;
        yield return new WaitForSeconds(0.25f);
        shootBAvailable = true;
    }

    IEnumerator LineTime()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }

    IEnumerator BumpKill()
    {
        isBumped = true;
        Debug.Log("IsBumped");
        yield return new WaitForSeconds(2f);
        isBumped = false;
    }
    
}
