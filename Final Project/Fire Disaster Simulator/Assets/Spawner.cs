using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_sceneLoader;
    void Awake()
    {
        GameObject go = GameObject.FindGameObjectWithTag("SceneLoader");
        if (go == null)
            Instantiate(m_sceneLoader);
    }


}
