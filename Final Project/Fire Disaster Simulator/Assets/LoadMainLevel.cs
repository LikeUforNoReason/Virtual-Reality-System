using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("HERE");
        if (other.gameObject.tag == "Lever")
            GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadFireScene();
    }
}
