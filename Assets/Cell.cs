using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
    private ArrayList nextCell = new ArrayList();
    private ArrayList list = new ArrayList();
    private Vector3 _cellForce;
    private int _hop;
    private float sizeDiag;
    private bool _underWall, firstTime;

    public bool underWall
    {
        get { return _underWall; }
        set { _underWall = value; }
    }
    public Vector3 cellForce
    {
        get { return _cellForce; }
        set { _cellForce = value; }
    }
    public int hop
    {
        get { return _hop; }
        set { _hop = value; }
    }
    // Use this for initialization
    void Awake()
    {
        _cellForce = Vector3.zero;
        _hop = 0;
        sizeDiag = Mathf.Sqrt(2 * transform.localScale.x * transform.localScale.x);
        underWall = false;
        firstTime = true;
    }
    void Start()
    {
        cellForce = cellForce.normalized;
    }
    void Update()
    {
        //showColor();
        if (underWall && cellForce == Vector3.zero)
        {
            calUnderWall();
            firstTime = false;
        }
    }
    void calDirection()
    {
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
        {
            if (Mathf.Sqrt((transform.position - cell.transform.position).sqrMagnitude) <= sizeDiag && !cell.GetComponent<Cell>().underWall
            && (cell.GetComponent<Cell>().hop == 0 || hop + 1 <= cell.GetComponent<Cell>().hop))
            {
                cell.GetComponent<Cell>().cellForce += (transform.position - cell.transform.position).normalized;
                cell.GetComponent<Cell>().hop = hop + 1;
            }
        }
        cellForce = cellForce.normalized;
        BroadcastMessage("setC", cellForce);
    }
    void showColor()
    {
        if (hop < 5) { GetComponent<Renderer>().material.color = Color.blue; }
        else if (hop < 10) { GetComponent<Renderer>().material.color = Color.green; }
        else if (hop < 15) { GetComponent<Renderer>().material.color = Color.yellow; }
        else if (hop < 20) { GetComponent<Renderer>().material.color = Color.red; }
        else { GetComponent<Renderer>().material.color = Color.black; }
        if (_cellForce == Vector3.zero) { GetComponent<Renderer>().material.color = Color.magenta; }
        //if (underWall) { renderer.material.color = Color.magenta; }
    }
    // Update is called once per frame
  
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            underWall = true;
            foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
            {
                if (Mathf.Sqrt((transform.position - cell.transform.position).sqrMagnitude) <= sizeDiag)
                {
                    list.Add(cell);
                }
            }
        }
    }
    void calUnderWall()
    {
        foreach (GameObject cell in list)
        {
            cellForce += cell.GetComponent<Cell>().cellForce;
        }
        cellForce = cellForce.normalized;
        BroadcastMessage("setC", cellForce);
    }
}
