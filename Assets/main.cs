using UnityEngine;
using System.Collections;

public class main : MonoBehaviour
{
    ArrayList nextCell = new ArrayList();
    int score,oScore;
    float firstExit,fisishTime;
    bool firstTime, destroyed;
    public GameObject cellTemplete;
    public GameObject people;
    public int amountOfPeople,tileX,tileY;
    
    // Use this for initialization
    void Start()
    {
        score=oScore = 0;
        setup(tileX, tileY);
        spawn(amountOfPeople);
        firstTime = true;
        destroyed = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstTime && !destroyed) 
        {
            calCellVector();
            destroyed = true;
        }
        firstTime = false;
    }
    void updateScore()
    {
        score++;
        if (score == 1) { firstExit = Time.realtimeSinceStartup; }
        else if (score == amountOfPeople) { fisishTime = Time.realtimeSinceStartup; }
    }
    void OnGUI()
    {
        if (score < amountOfPeople)
        {
            GUI.Label(new Rect(10, 10, 250, 30), "Time : " + Time.realtimeSinceStartup.ToString() + " sec");
            GUI.Label(new Rect(10, 130, 250, 30), "average flow rate : " + (score / (Time.realtimeSinceStartup - firstExit)).ToString() + " persec");
        }
        else
        {
            GUI.Label(new Rect(10, 10, 250, 30), "Time : " + fisishTime.ToString() + " sec");
            GUI.Label(new Rect(10, 130, 250, 30), "average flow rate : " + (score / (fisishTime - firstExit)).ToString() + " persec");  
        }
        GUI.Label(new Rect(10, 50, 250, 30), "Exit : " + score.ToString());
        GUI.Label(new Rect(10, 90, 250, 30), "First exit : " + firstExit.ToString() + " sec");
        oScore = score;
    }
    void spawn(int number)
    {
        int count;
        float x,z;
        Vector3 position;
        Vector3 center = GameObject.Find("SpawnPlane").transform.position;
        Vector3 scale = GameObject.Find("SpawnPlane").transform.localScale;
        for (count = 0; count < number;count++ )
        {
            x = Random.Range(center.x - scale.x * 5, center.x + scale.x * 5);
            z = Random.Range(center.z - scale.z * 5, center.z + scale.z * 5);
            position = new Vector3(x, 0.7f, z);
            Instantiate(people, position, Quaternion.identity);
            /*if (!Physics.CheckSphere(position, 0.25f))
            {
                
            }
            else { }//count--; }*/
        }
    }
    void setup(int roll, int col)
    {
        Vector2 sPosition = new Vector2(-roll * cellTemplete.transform.localScale.x / 2, -col * cellTemplete.transform.localScale.x / 2);
        Vector3 position = new Vector3(sPosition.x, 0, sPosition.y);
        int i, j;
        //setup cell
        for (i = 0; i < roll; i++)
        {
            for (j = 0; j < col; j++)
            {
                GameObject nob = Instantiate(cellTemplete, position, Quaternion.identity) as GameObject;
                position.x += cellTemplete.transform.localScale.x;
            }
            position.x = sPosition.x;
            position.z += cellTemplete.transform.localScale.x;
        }
    }
    void calCellVector()//calculate all of cell
    {
        int count;
        //find cell next to door

        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
            {
                if (Mathf.Sqrt((door.transform.position - cell.transform.position).sqrMagnitude) < 2)
                {
                    //nextDoor.Add(cell);
                    cell.GetComponent<Cell>().cellForce = (door.transform.position - cell.transform.position).normalized;
                    cell.GetComponent<Cell>().hop = 1;
                }
            }
        }
        //calculate other cell
        for (count = 1; count < 1000; count++)
        {
            foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
            {
                if (cell.GetComponent<Cell>().hop == count) { nextCell.Add(cell); }
            }
            foreach (GameObject cell in nextCell)
            {
                cell.SendMessage("calDirection");
            }
            nextCell.Clear();
        }
    }   
}
