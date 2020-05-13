using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HERE");
        if(other.gameObject.tag == "Hands")
         GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().TimeWarp();
    }   


}
