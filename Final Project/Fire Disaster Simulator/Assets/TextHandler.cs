using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject retrytext;
    public GameObject controltext;
    public GameObject dejavu;
    public SceneLoader my_sceneLoader;
    void Start()
    {
        my_sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == my_sceneLoader.practice && my_sceneLoader.previous == -1)
        {
            retrytext.SetActive(false);
            controltext.SetActive(true);
            dejavu.SetActive(false);
        }

        else if (SceneManager.GetActiveScene().buildIndex == my_sceneLoader.practicereplica && my_sceneLoader.previous == 1)
        {
            retrytext.SetActive(true);
            controltext.SetActive(false);
            dejavu.SetActive(false);
        }

        else if (SceneManager.GetActiveScene().buildIndex == my_sceneLoader.practicereplica && my_sceneLoader.previous == 2)
        {
            retrytext.SetActive(false);
            controltext.SetActive(false);
            dejavu.SetActive(true);
        }
    }
}
