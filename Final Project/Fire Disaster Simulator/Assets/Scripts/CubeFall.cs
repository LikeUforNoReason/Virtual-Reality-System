using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CubeFall : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody self;
    void Start()
    {
        self = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            self.useGravity = true;
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

    }
}
