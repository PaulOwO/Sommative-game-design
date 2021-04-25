using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    private void Update()
    {
        StartCoroutine(ExecuteAfterTime(1));
    }

    //Obstacles despawn of the arena after a quite long time
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(60f);
 
        Destroy(GameObject.FindWithTag("Obstacles"));
    }
}
