using UnityEngine;
using System.Collections;

public class cube : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void setC(Vector3 V)
    {
        transform.localPosition = new Vector3(V.x,-1,V.z).normalized;
        //if (V != Vector3.zero) { renderer.material.color = Color.yellow; }
        //else { renderer.material.color = Color.clear; }
    }
}
