using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CellAttributes
{
    public bool Persist;
    public float HP;
    public float fuel;
    public float moisture;
    public float threshold;
    public float combustionValue;
    public float airTemperature;
    public float propagationSpeed;
    public float cellSize;
}


public class FireGridSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag of the ground")]
    private string m_groundName;

    [SerializeField]
    [Tooltip("Prefab to be used for the fire.")]
    private GameObject m_firePrefab;
    [SerializeField]
    [Tooltip("Number of cells in the gird (width).")]
    private int m_widthCells = 45;
    [SerializeField]
    [Tooltip("Number of cells in the gird (height).")]
    private int m_heightCells = 45;

    [SerializeField] [Tooltip("Max Possible number of cells")]
    private int m_allocatedListSize;
    private SortedList<int, Vector2> m_alightCellIndex;

    private int m_width;
    private int m_height;

    private float m_cellSize;

    private GameObject[,] m_cells;
    private GameObject Manager;
    private FirePropagationManager m_fireManager;

    private float m_airTemperature = 0.0f;
    private float m_visualThreshold = 0.2f;
    private float m_combustionRateValue = 5.0f;

    private float m_bias;
    private bool m_gridCreated = false;
    private bool m_centerCellIgnited = false;
    private int m_fireCellsLit = 0;
    private float m_maxHillDistance;

    private Vector3 m_origin;
    public FireAttribute m_fireAttribute;
    private void Start()
    {
        GameObject manager = GameObject.FindWithTag("Fire");

        if (manager != null)
        {
            m_fireManager = manager.GetComponent<FirePropagationManager>();
            

        }
        else
            Debug.Log("MISSING");
        m_bias = m_fireManager.propagationBias;
        m_maxHillDistance = m_fireManager.maxHillPropagationDistance;

        m_origin = transform.position;

        BuildGrid();
    }

    private void Update()
    {
        if (m_fireManager != null && m_gridCreated)
        {
            if (m_centerCellIgnited == false)
            {
                // Start fire
                Vector2 ArrayCenter = new Vector2((float)m_cells.GetLength(0) / 2.0f, (float)m_cells.GetLength(1) / 2.0f);
                m_cells[(int)ArrayCenter.x, (int)ArrayCenter.y].GetComponent<FireGridCell>().ignitionTemperature = 0.0f;
                m_cells[(int)ArrayCenter.x, (int)ArrayCenter.y].GetComponent<FireGridCell>().HeatsUp();
                m_centerCellIgnited = true;
            }

            Propagation();
        }
    }



    void Propagation()
    {
        m_fireCellsLit = 0;
        
        // Keep within grid boundary for the next step
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Vector2 indexVector = new Vector2(x, y);
                int index = x * m_width + y;
                bool contained = m_alightCellIndex.ContainsKey(index);

                if (m_cells[x, y].GetComponent<FireGridCell>().fireProcessHappening && contained == false)
                {
                    m_alightCellIndex.Add(index, indexVector);
                    
                }
                else if (contained == true)
                {

                   
                    m_alightCellIndex.Remove(index);
                }
            }
        }

        foreach (Vector2 index in m_alightCellIndex.Values)
        {
            int x = (int)index.x;
            int y = (int)index.y;

            FireGridCell firecell = m_cells[x, y].GetComponent<FireGridCell>();
            firecell.GridUpdate();

            if (firecell.isAlight)
            {
                
                // Keep in array bounds
                if (x < m_width - 1)
                {
                   // Debug.Log("PROPAGATING 1 Out");
                    FireGridCell selectedCell = m_cells[x + 1, y].GetComponent<FireGridCell>();
                    int slopeCode = PropagatingOnSlope(firecell.position, selectedCell.position);

                    // If fire can propagate to, heat up the grid
                    if (slopeCode != -1)
                    {
                      //  Debug.Log("PROPAGATING 1");
                        selectedCell.HeatsUp();
                    }
                }

                // Keep in array bounds
                if (y < m_height - 1)
                {
                  //  Debug.Log("PROPAGATING 2 Out");
                    // Is the fire propagating on a slope, if so which direction and time of day
                    FireGridCell selectedCell = m_cells[x, y + 1].GetComponent<FireGridCell>();
                    int slopeCode = PropagatingOnSlope(firecell.position, selectedCell.position);

                    // If fire can propagate to, heat up the grid
                    if (slopeCode != -1)
                    {
                      //  Debug.Log("PROPAGATING 2");
                        selectedCell.HeatsUp();
                    }
                }

                // Keep in array bounds
                if (x > 0)
                {
                  //  Debug.Log("PROPAGATING 3 out");
                    // Is the fire propagating on a slope, if so which direction and time of day
                    FireGridCell selectedCell = m_cells[x - 1, y].GetComponent<FireGridCell>();
                    int slopeCode = PropagatingOnSlope(firecell.position, selectedCell.position);

                    // If fire can propagate to, heat up the grid
                    if (slopeCode != -1)
                    {
                   //     Debug.Log("PROPAGATING 3");
                        selectedCell.HeatsUp();
                    }

                }

                // Keep in array bounds
                if (y > 0)
                {
                 //   Debug.Log("PROPAGATING 4 out");
                    // Is the fire propagating on a slope, if so which direction and time of day
                    FireGridCell selectedCell = m_cells[x, y - 1].GetComponent<FireGridCell>();
                    int slopeCode = PropagatingOnSlope(firecell.position, selectedCell.position);

                    // If fire can propagate to, heat up the grid
                    if (slopeCode != -1)
                    {
                 //       Debug.Log("PROPAGATING 4");
                        selectedCell.HeatsUp();
                    }

                }
            }
        }

        // Number of active fires
        m_fireCellsLit = m_alightCellIndex.Count;
    }

    int PropagatingOnSlope(Vector3 fireOrigin, Vector3 fireTarget)
    {
        // the distance will be + the same as the cell size if propagating on flat terrain
        float distance = Vector3.Distance(fireOrigin, fireTarget);
       // Debug.Log(distance + " " + m_maxHillDistance);
        // make sure the fire cannot propagate over define threshold, so it cannot propagate up/down a cliff for example.
        if (distance <= m_maxHillDistance)
        {
            // is the fire propagating up for down hill?
            if (fireTarget.y > fireOrigin.y) // up hill
            {
                return 1;
            }
            else if (fireTarget.y < fireOrigin.y) // down hill
            {
                return 2;
            }
            else // flat
            {
                return 0;
            }
        }
        else if (Mathf.Abs(fireOrigin.y - fireTarget.y) < m_maxHillDistance)
        {
            return 0; // flat
        }

        return -1; // cannot propagate to target
    }

    public void InitializeGrid(GameObject firePrefab, Vector3 position, int gridWidth, int gridHeight)
    {
        transform.position = position;
        m_widthCells = gridWidth;
        m_heightCells = gridHeight;
        m_firePrefab = firePrefab;

        m_width = gridWidth;
        m_height = gridHeight;

        m_cellSize = 2.5f;
        m_groundName = "Ground";
       
    }

    void BuildGrid()
    {
        m_alightCellIndex = new SortedList<int, Vector2>(m_allocatedListSize);

        float offsetX = 0.0f;
        float offsetY = 0.0f;

        // Get the offset, depending if it is an even or odd width or height
        if (m_width % 2 == 0)
            offsetX = (m_width / 2.0f) * m_cellSize;
        else if (m_width % 2 == 1)
            offsetX = ((m_width - 1) / 2.0f) * m_cellSize;

        if (m_height % 2 == 0)
            offsetY = (m_height / 2.0f) * m_cellSize;
        else if (m_height % 2 == 1)
            offsetY = ((m_height - 1) / 2.0f) * m_cellSize;
        Debug.Log(offsetX + " " + offsetY);
        m_cells = new GameObject[m_width, m_height];
        GameObject tmp = new GameObject();
        tmp.AddComponent<FireGridCell>();
        Quaternion quat = new Quaternion();

        // Create cell data of common data
        CellAttributes cellAttribute;
        cellAttribute.airTemperature = m_airTemperature;
        cellAttribute.threshold = m_visualThreshold;
        cellAttribute.combustionValue = Random.Range(m_fireAttribute.FireBurnRateMin, m_fireAttribute.FireBurnRateMax);
        cellAttribute.cellSize = m_cellSize;

        // Create the cells in the grid
        Debug.Log("Building Grid "+ m_width + " "+ m_height);
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                
                Vector3 index = new Vector3((transform.position.x - offsetX) + (x * m_cellSize), transform.position.y ,(transform.position.z - offsetY) + (y * m_cellSize));
                //Debug.Log(index);
               // Vector3 worldPosition = GetWorldPosition(index);
                Vector3 worldPosition = (index);
           
                GameObject go = (GameObject)Instantiate(tmp, worldPosition, quat, transform);
                
                m_cells[x, y] = go;
                m_cells[x, y].name = "FireCell " + (x * m_width + y).ToString();
                FireGridCell firecell = m_cells[x, y].GetComponent<FireGridCell>();

                cellAttribute.propagationSpeed = GetValuesFromFuelType((int)worldPosition.x, (int)worldPosition.z, out cellAttribute.HP, out cellAttribute.fuel, out cellAttribute.moisture, out cellAttribute.Persist);
                firecell.SetupCell(false, m_firePrefab, cellAttribute, m_groundName, m_fireManager.cellFireSpawnPositions); // relative placementwithin cell

                // Modify the heat of the fire, to make the fire burn slower the greater the distance the fire is from the start position

                //float baisDistance = Vector3.Distance(firecell.position, transform.position);
                //baisDistance /= m_bias;

                // Fire temp could still be modified to minus value by the wind in the propagation step
                //firecell.fireTemperature -= baisDistance;
            }
        }

        DestroyImmediate(tmp);
        m_gridCreated = true;
    }
    public Vector3 GetWorldPosition(Vector2 gridPosition)
    {
        return new Vector3(m_origin.z + (gridPosition.x), m_origin.y, m_origin.x + (gridPosition.y));
    }

    // brief Gets the fire propagation speed based on the terrain fuel type the cell is located on, also fuel, mositure and HP values
    // param int Cell X position
    // param int Cell Y position
    // return float cell Hit Points
    // return float cell fuel
    // return float cell mositure
    float GetValuesFromFuelType(int x, int y, out float hp, out float fuel, out float mositure, out bool Persist)
    {
        float speed = Random.Range(m_fireAttribute.FirePropagationSpeedMin, m_fireAttribute.FirePropagationSpeedMax);
        hp = Random.Range(m_fireAttribute.FireHealthMin, m_fireAttribute.FireHealthMax);
        fuel = Random.Range(m_fireAttribute.FireFuelMin, m_fireAttribute.FireFuelMax);
        mositure = Random.Range(m_fireAttribute.FireMoistureMin, m_fireAttribute.FireMoistureMax);
        Persist = m_fireAttribute.FirePersist;

        return speed;
    }
}
