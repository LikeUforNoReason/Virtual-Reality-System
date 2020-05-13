using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Trigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] ToActivate;
    public GameObject[] ToDeactivate;

    public int triggerbehaviour;
    public TextMeshPro playerUI;
    private GameObject[] my_list;

    public FastMobileBloom my_script;
    // Update is called once per frame


    private void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (triggerbehaviour == 0)
            {
                for (int i = 0; i < ToActivate.Length; i++)
                    ToActivate[i].SetActive(true);
            }
            else if (triggerbehaviour == 1)
            {
                for (int i = 0; i < ToDeactivate.Length; i++)
                    ToDeactivate[i].SetActive(false);
            }

            else if (triggerbehaviour == 2)
            {
                for (int i = 0; i < ToActivate.Length; i++)
                    ToActivate[i].SetActive(true);

                for (int i = 0; i < ToDeactivate.Length; i++)
                    ToDeactivate[i].SetActive(false);
            }

            else if (triggerbehaviour == 4)
            {
                my_script.enabled = false;
                for (int i = 0; i < ToActivate.Length; i++)
                    ToActivate[i].SetActive(true);

                StartCoroutine(MyCoroutine());

                //GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadFinalScene();
            }

            else if (triggerbehaviour == -1)
            {
                my_list = new GameObject[10];
                my_list = GameObject.FindGameObjectsWithTag("FireGridSystem");
                ToDeactivate = my_list;
                for (int i = 0; i < ToDeactivate.Length; i++)
                    ToDeactivate[i].SetActive(false);
            }
        }
    }
    IEnumerator MyCoroutine()
    {

        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().LoadFinalScene();
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}

