using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitch : MonoBehaviour
{
    public string sceneload;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("on the trigger");
        SceneManager.LoadScene(sceneload);
    }
}
