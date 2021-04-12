using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;


public class gun : MonoBehaviour
{
    public float range = 100f;
    public float damage = 10f;
    public float impactForce = 30f;
    public LineRenderer lineRenderer;
    public bool shootAvailable = true;
    [SerializeField] private PlayerController playercontroller;
    
    void Update()
    {
        if (shootAvailable)
        {
            var device = InputManager.Devices[playercontroller.index];
            if (device.RightBumper.IsPressed)
            {
                Shoot();
                StartCoroutine(FireRate());
                //Debug.Log("Shoot");
            }
        }
        
    }
    

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
               // target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * range);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.forward * range);
        }

        StartCoroutine(LineTime());
    }

    IEnumerator FireRate()
    {
        shootAvailable = false;
        yield return new WaitForSeconds(3.00f);
        shootAvailable = true;
    }

    IEnumerator LineTime()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
