using UnityEngine;
using System.Collections;

public class People : MonoBehaviour {
    Vector3 peopleExpect, sumForce,cellForce;
    Vector3[] doors;
    int count;
	// Use this for initialization
	void Start () 
    {
        GameObject[] tempDoors = GameObject.FindGameObjectsWithTag("Door");
        doors = new Vector3[tempDoors.Length];
        for (count = 0; count < doors.Length; count++)
        {
            doors[count] = tempDoors[count].transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.position = new Vector3(transform.position.x,0.7f,transform.position.z);

        peopleExpect = doors[0] - transform.position;
        for (count = 1; count < doors.Length; count++)
        {
            if ((doors[count] - transform.position).sqrMagnitude < peopleExpect.sqrMagnitude)
            {
                peopleExpect = doors[count] - transform.position;
            }
        }
        peopleExpect = peopleExpect.normalized;

        sumForce = 2*peopleExpect + cellForce;
        sumForce = sumForce.normalized;

        if (cellForce != Vector3.zero)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(cellForce.x, 0, cellForce.z).normalized * 1.3f;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(peopleExpect.x, 0, peopleExpect.z).normalized * 1.3f;
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cell") { cellForce = other.GetComponent<Cell>().cellForce; }
        //if (other.tag == "Cell") { cellForce = calCellForce(other.gameObject); }

    }
    void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Cell") { cellForce = other.GetComponent<Cell>().cellForce; }
        //if (other.tag == "Cell") { cellForce = calCellForce(other.gameObject); }
    }
    Vector3 calCellForce(GameObject curentCell)
    {
        int count;
        float sizeDiag;
        Vector3 force;
        force = Vector3.zero;
        sizeDiag = Mathf.Sqrt(2 * curentCell.transform.localScale.x * curentCell.transform.localScale.x);
        count = 0;
        foreach (GameObject cell in GameObject.FindGameObjectsWithTag("Cell"))
        {
            if (Mathf.Sqrt((curentCell.transform.position - cell.transform.position).sqrMagnitude) <= sizeDiag && count<3)
            {
                force += cell.GetComponent<Cell>().cellForce;
                count++;
            }
        }
        return force.normalized;
    }
}
