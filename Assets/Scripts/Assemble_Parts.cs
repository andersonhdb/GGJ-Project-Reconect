using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble_Parts : MonoBehaviour
{
    [SerializeField]
    private Transform[] assemblePoints;
    [SerializeField]
    private Transform[] assembleObjects;
    private float SnappingDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < assemblePoints.Length; i++)
        {
            Collider[] colliders = Physics.OverlapSphere(assemblePoints[i].position, SnappingDistance);
            if(colliders.Length>0)
            {
                foreach(Collider c in colliders)
                {
                    if (c.transform == assembleObjects[i])
                    {
                        c.transform.tag = "Untagged";
                        c.transform.position = assemblePoints[i].position;
                        c.transform.rotation = assemblePoints[i].rotation;
                        c.transform.SetParent(null);
                        break;
                    }
                }
            }
        }
        
    }
}
