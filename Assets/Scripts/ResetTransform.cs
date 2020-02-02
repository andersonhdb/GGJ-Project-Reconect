using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTools;

public class ResetTransform : MonoBehaviour
{
    public Grabing Hand;
    Grabing OtherHand;
    public Transform handler;
    Rigidbody rb;
    Rigidbody rb_Handler;
    bool WasGrabbedByHand;
    bool WasGrabbedByOtherHand;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        WasGrabbedByHand = false;
        WasGrabbedByOtherHand = false;
        OtherHand = Hand.GetComponent<Grabing>().OtherHandReference;
        rb_Handler = handler.GetComponent<Rigidbody>();
    }
    //Reset transformation and make child of the handler
    void CheckHand()
    {
        if (Hand.getGrabbingObject() == null)
        {
            ResetPosition();
            WasGrabbedByHand = false;
        }
    }

    //Put the grabbable handler again in handler
    void ResetPosition()
    {
        this.gameObject.transform.SetParent(handler);
        rb.isKinematic = true;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = handler.transform.position;
        transform.rotation = handler.transform.rotation;
        rb_Handler.velocity = Vector3.zero;
        rb_Handler.angularVelocity = Vector3.zero;
    }
    //Reset transformation and make child of the handler
    void CheckOtherHand()
    {
        if (OtherHand.getGrabbingObject() == null)
        {
            ResetPosition();
            WasGrabbedByOtherHand = false;
        }
    }
     void Update()
    {
        //Check if we grabbed the object
        if (Hand.getGrabbingObject()!=null)
        {
            WasGrabbedByHand = true;
        }
        if (OtherHand.getGrabbingObject() != null)
        {
            WasGrabbedByOtherHand = true;
        }
        //Call the right function to check if the object still in the hand
        if (WasGrabbedByHand==true)
        {
            CheckHand();
        }
        if (WasGrabbedByOtherHand == true)
        {
            CheckOtherHand();
        }
        //If you are holding the door and you are far away
        if (Vector3.Distance(handler.position,transform.position) > .4f)
        {
            ResetPosition();
        }
    }

}