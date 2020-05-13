using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public int musicIndex;

    public SpecialLevelMusic my_file;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hands")
            my_file.PlayAudio(musicIndex);

    }
}
