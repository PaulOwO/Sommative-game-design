using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gun : MonoBehaviour
{
    public float range = 100f;
    public float damage = 10f;

    private bool shooted = false;


    void Update()
    {
     /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }*/

        if (shooted)
        {
            Shoot();
            //Debug.Log("Shoot");
        }
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        shooted = ctx.ReadValue<bool>();
        shooted = ctx.action.enabled;
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
                target.TakeDamage(damage);
            }
        }
    }
}
