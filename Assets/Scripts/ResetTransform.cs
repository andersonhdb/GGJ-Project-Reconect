using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTools;

public class ResetTransform : MonoBehaviour
{
    public Grabing devices;
    public Transform handler;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
     void Update()
    {
        if (devices.getGrabbingObject())
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.position = handler.transform.position;
            transform.rotation = handler.transform.rotation;
        }

    }

}