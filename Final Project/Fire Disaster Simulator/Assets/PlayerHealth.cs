using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.ImageEffects
{

    public class PlayerHealth : MonoBehaviour
    {
        // Start is called before the first frame update

        private float health;
        public float maxHealth = 150f;
        public float maxBloom;
        public int fireDamage = 1;
        private bool regen = true;
        public FastMobileBloom myscript;
        public TextMeshPro healthtext;
        void Start()
        {
            health = maxHealth;
        }
        void OnTriggerStay(Collider col)
        {
//            Debug.Log("HERE!"+ col.gameObject.tag);
            if (col.gameObject.tag == "Flames")
            {
                regen = false;
                health -= fireDamage * Time.deltaTime;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Flames")
                regen = true;
        }
        private void Update()
        {
            if (health <= 0f)
            {
                GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoader>().PlayerDeathRetry();
                Destroy(transform.parent.gameObject);
                 
            }
            if (regen == true && health <=maxHealth)
                health = health + 5 * Time.deltaTime;

          //  Debug.Log((health - 50f) * -0.038f + 4f);
            myscript.intensity = (health - 50f)* -0.038f + 4f;
           // healthtext.text = "Health :" + health.ToString();
            
        }
    }
}