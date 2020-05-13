using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource firecrackle;
    void Start()
    {
        firecrackle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (firecrackle.volume < 1.0)
            firecrackle.volume += Time.deltaTime / 30;

        firecrackle.pitch = Random.Range(0.9f, 1.1f);
    }
}
