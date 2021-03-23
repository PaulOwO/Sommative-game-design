using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    //private float speed = 50f;
    private void Update()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        /*yield return new WaitForSeconds(10f);

        if (GameObject.FindWithTag("Obstacles"))
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }*/
        
        yield return new WaitForSeconds(60f);
 
        Destroy(GameObject.FindWithTag("Obstacles"));
    }

   
}
