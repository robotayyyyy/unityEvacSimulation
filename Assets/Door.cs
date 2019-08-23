using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "People") 
        {
            Destroy(other.gameObject);
            GameObject.FindWithTag("MainCamera").GetComponent<main>().SendMessage("updateScore");
        }
    }
}
