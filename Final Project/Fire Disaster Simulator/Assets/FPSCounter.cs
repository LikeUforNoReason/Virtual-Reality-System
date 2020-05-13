using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int FPS { get; private set; }
    public TextMeshPro fpscounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float current = (int)1f / Time.deltaTime;
        if (Time.frameCount % 50 == 0)
            fpscounter.text = current.ToString() + " FPS";
    }
}
