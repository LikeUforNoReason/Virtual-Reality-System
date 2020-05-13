using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public int previous;
    public int current;

    public int practice;
    public int fireplay;
    public int special;
    public int practicereplica;
 
    
    void Start()
    {
        previous = -1;
        current = 0;
    }


    public void LoadFireScene()
    {
        current = fireplay;
        previous = practice;
        SceneManager.LoadScene(fireplay);
    }

    public void PlayerDeathRetry()
    {
        current = practice;
        previous = fireplay;
        SceneManager.LoadScene(practicereplica);
    }

    public void LoadFinalScene()
    {
        current = special;
        previous = fireplay;
        SceneManager.LoadSceneAsync(special);
    }

    public void TimeWarp()
    {
        current = practice;
        previous = special;
        SceneManager.LoadScene(practicereplica);
    }
}
