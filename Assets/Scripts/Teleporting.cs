using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Teleporting : MonoBehaviour
{
    private List<InputDevice> devices;
    
    [SerializeField]
    private InputDeviceRole chosenRole;

    [SerializeField]
    private Transform player;

    private bool buttonValue = false;
    private bool previousButtonValue;

    [SerializeField]
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        previousButtonValue = buttonValue;
        devices = new List<InputDevice>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithRole(chosenRole, devices);
        
        foreach (InputDevice device in devices)
        {
            device.TryGetFeatureValue(CommonUsages.primaryButton, out buttonValue);
        }

        if (buttonValue)
        {
            
            int layerMask = 1 << 8;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log("There is a hit");
                if (!target.GetComponent<MeshRenderer>().enabled)
                {
                    target.GetComponent<MeshRenderer>().enabled = true;
                }
                target.transform.position = hit.point + Vector3.up * 0.01f;
            }
        }
        else
        {
            if (previousButtonValue)
            {
                Debug.Log("The buttom is relesed");
                player.position = target.transform.position;
                target.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        previousButtonValue = buttonValue;
    }
}
