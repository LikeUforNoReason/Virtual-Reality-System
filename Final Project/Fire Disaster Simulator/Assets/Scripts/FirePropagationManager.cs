using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePropagationManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Relative position of fire spawning in the cell (0 -> 1). If left empty default value is 0.5 on X and Y.")]
    private Vector2[] m_cellFireSpawnPositions;
    [SerializeField]
    [Tooltip("Smaller the value the more random/less uniformed the propagation.")]
    private float m_propagationBias = 0.4f;
    [SerializeField]
    [Tooltip("The largest height distance fire can propagate on terrain. Stops fire propagating up large cliffs.")]
    private float m_maxHillPropagationDistance = 2f;
    [SerializeField]
    [Tooltip("At what point the extinguish particle systems should be active, is a percentage (0 -> 1). Doesn't affect simulation, only visuals.")]
    private float m_visualExtinguishThreshold = 0.2f;
    // Start is called before the first frame update

    public Vector2[] cellFireSpawnPositions { get { return m_cellFireSpawnPositions; } }
    public float propagationBias
    {
        get { return m_propagationBias; }
        set { m_propagationBias = value; }
    }
    public float maxHillPropagationDistance
    {
        get { return m_maxHillPropagationDistance; }
        set { m_maxHillPropagationDistance = value; }
    }

    public float visualExtinguishThreshold
    {
        get { return m_visualExtinguishThreshold; }
        set { m_visualExtinguishThreshold = value; }
    }

    void Awake()
    {
        if (m_cellFireSpawnPositions.Length == 0)
            m_cellFireSpawnPositions = new Vector2[1] { new Vector2(0.5f, 0.5f) };
    }
}
