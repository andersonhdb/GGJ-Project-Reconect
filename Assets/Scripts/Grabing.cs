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
        [SerializeField]
        Grabing OtherHandReference;

        private float closeness;

        [SerializeField]
        private string grabTag = "Grab";
        [SerializeField]
        private float grabDistance = 0.05f;
        [SerializeField]
        private float throw_multiplier = 1f;

        private Vector3 _lastFramePosition;
        private Transform _currentGrabObject;
        private bool _isGrabbing;



        void Start()
        {
            devices = new List<InputDevice>();
            _lastFramePosition = transform.position;
            _isGrabbing = false;
            _currentGrabObject = null;
        }

        // Update is called once per frame
        void Update()
        {
            InputDevices.GetDevicesWithRole(chosenRole, devices);

            foreach (InputDevice device in devices)
            {
                device.TryGetFeatureValue(CommonUsages.trigger, out closeness);
                handController.SetFloat("Closeness", closeness);

            }

            if (_currentGrabObject == null)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, grabDistance);
                if (colliders.Length > 0)
                {
                    if(closeness>0 && colliders[0].transform.CompareTag(grabTag))
                    {
                        if (_isGrabbing)
                        {
                            return;
                        }
                        _isGrabbing = true;

                        colliders[0].transform.SetParent(transform);
                        
                        if (colliders[0].GetComponent<Rigidbody>() == null)
                        {
                            colliders[0].gameObject.AddComponent<Rigidbody>();
                        }

                        colliders[0].GetComponent<Rigidbody>().isKinematic = true;

                        _currentGrabObject = colliders[0].transform;

                        if (OtherHandReference.getGrabbingObject() != null)
                        {
                            OtherHandReference.setGrabbingObject(null);

                        }

                    }
                }

            }
            else
            {
                if (closeness < 0.1)
                {
                    //release the the object (unparent it)

                    Rigidbody _objectRGB = _currentGrabObject.GetComponent<Rigidbody>();
                    _objectRGB.isKinematic = false;

                    //calculate the hand's current velocity
                    Vector3 CurrentVelocity = (transform.position - _lastFramePosition) / Time.deltaTime;

                    //set the grabbed object's velocity to the current velocity of the hand
                    _objectRGB.velocity = CurrentVelocity * throw_multiplier;

                    _currentGrabObject.SetParent(null);

                    //release reference to object
                    _currentGrabObject = null;

                }
            }

            if (closeness < 0.1 && _isGrabbing)
            {
                _isGrabbing = false;
            }

            _lastFramePosition = transform.position;

        }

        public void setGrabbingObject(Transform target)
        {
            _currentGrabObject = target;
        }

        public Transform getGrabbingObject()
        {
            return _currentGrabObject;
        }
    }
}