using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private gun gun;
    public bool isBumpedB;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(gameObject);
        StartCoroutine(BumpKillB());
    }

    IEnumerator BumpKillB()
    {
        isBumpedB = true;
        Debug.Log("IsBumped");
        yield return new WaitForSeconds(2f);
        isBumpedB = false;
    }
}
