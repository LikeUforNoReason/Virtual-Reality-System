using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLevelMusic : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip darksouls;
    public AudioClip doom;
    public AudioClip halo;
    public AudioClip skyrim;
    public AudioClip witcher;
    public AudioClip halflife;
    public int currentclip;
    public AudioSource m_audiosource;
    void Start()
    {
        m_audiosource.loop = false;
        currentclip = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_audiosource.isPlaying)
            currentclip = -1;
    }

    public void PlayAudio(int s)
    {

        Debug.Log("COMING HERE!");
       switch(s)
        {
            case 1: if (currentclip != s)
                    {
                        m_audiosource.clip = darksouls;
                        m_audiosource.Play();
                        currentclip = 1;
                    }
                    break;
            case 2:
                if (currentclip != s)
                {

                    m_audiosource.clip = doom;
                    m_audiosource.Play();
                    currentclip = 2;
                }

                break;
            case 3:
                if (currentclip != s)
                {

                    m_audiosource.clip = skyrim;
                    m_audiosource.Play();
                    currentclip = 3;
                }
                break;

            case 4:
                if (currentclip != s)
                {

                    m_audiosource.clip = halflife;
                    m_audiosource.Play();
                    currentclip = 4;
                }

                break;

            case 5:
                if (currentclip != s)
                {

                    m_audiosource.clip = halo;
                    m_audiosource.Play();
                    currentclip = 5;
                }

                break;

            case 6:
                if (currentclip != s)
                {

                    m_audiosource.clip = witcher;
                    m_audiosource.Play();
                    currentclip = 6;
                }

                break;
        }
    }
}
