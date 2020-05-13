using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;

public class SecretSong : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource m_audioSource; 
   
    void Start()
    {
        m_audioSource = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void StartSong()
    {
       m_audioSource.enabled = !m_audioSource.enabled;
    }
}
