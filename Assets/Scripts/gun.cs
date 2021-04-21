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
    [SerializeField] private PlayerController playercontroller;
    private float bulletSpeed = 10f;

    
    

    void Update()
    {
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
            if (device.LeftBumper.IsPressed)
            {
                ShootB();
                StartCoroutine(FireRateB());
                //Debug.Log("Shoot");
            }
        }
        
        
    }

    void ShootB()
    {
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
