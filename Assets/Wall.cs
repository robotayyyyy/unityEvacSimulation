using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    bool firstFrame,destroyed;
    // Use this for initialization
    void Start()
    {
        firstFrame = true;
        destroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstFrame && !destroyed) { Destroy(GetComponent<Rigidbody>()); destroyed = true; }
        firstFrame = false;
    }
}
