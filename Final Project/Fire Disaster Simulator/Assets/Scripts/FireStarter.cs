using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;

public class FireStarter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Width of the fire grid, fire starts in the center of the grid")]
    private int m_gridWidth = 10;
    [SerializeField]
    [Tooltip("Height of the fire grid, fire starts in the center of the grid")]
    private int m_gridHeight = 10;
    [SerializeField]
    [Tooltip("Prefab of the fire to use")]
    private GameObject m_firePrefab;
    [SerializeField]
    [Tooltip("Delete this GameObject when there is a collision with it and the terrain or another GameObject?")]
    private bool m_destroyOnCollision = false;
    private bool m_gridSpawned  = false;
    private Rigidbody m_rigidbody;
    // Update is called once per frame
    public int key;
    
    void Start()
    {
        if (m_firePrefab == null)
        {
            Debug.LogError("No Fire Prefab set on Fire Igniter.");
        }

        if (m_gridWidth < 1 || m_gridHeight < 1)
        {
            Debug.LogError("Invalid Grid Size");
        }

        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        

    }

    public void Update()
    {
        
          
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
          m_rigidbody.useGravity = true;
        }

    }

    public void OnCollision()
    {
        GameObject fireGrid = new GameObject();
        fireGrid.name = "FireGridSystem";
        FireGridSystem grid = fireGrid.AddComponent<FireGridSystem>();
        grid.tag = "FireGridSystem"; 
        grid.m_fireAttribute = gameObject.GetComponent<FireAttribute>();
        grid.InitializeGrid(m_firePrefab, gameObject.transform.position, m_gridWidth, m_gridHeight);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (m_gridSpawned == false)
        {
            OnCollision();
            m_gridSpawned = true;

            if (m_destroyOnCollision)
                Destroy();
        }
    }

    // brief Destroy this object
    void Destroy()
    {
        Destroy(gameObject);
    }
}
