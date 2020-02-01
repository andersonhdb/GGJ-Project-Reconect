using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace VRTools { 
    public class Grabing : MonoBehaviour
    {
        List<InputDevice> devices;
        // Start is called before the first frame update

        [SerializeField]
        Animator handController;
        [SerializeField]
        InputDeviceRole chosenRole;
        float closeness;

        void Start()
        {
            devices = new List<InputDevice>();
        }

        // Update is called once per frame
        void Update()
        {
            InputDevices.GetDevicesWithRole(chosenRole, devices);

            foreach(InputDevice device in devices)
            {
                device.TryGetFeatureValue(CommonUsages.trigger, out closeness);
                handController.SetFloat("Closeness", closeness);
                
            }
        }
    }
}