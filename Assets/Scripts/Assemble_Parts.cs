using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble_Parts : MonoBehaviour
{
    [SerializeField]
    private Transform[] assemblePoints;
    [SerializeField]
    private Transform[] assembleObjects;
    [SerializeField]
    private GameObject prize;
    
    private float SnappingDistance = 0.1f;

    private int assembleCount;
    private bool hasSpawnedPrize = false;

    // Start is called before the first frame update
    void Start()
    {
        hasSpawnedPrize = false;
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
                    if (c.transform == assembleObjects[i] && c.gameObject.tag == "Grab")
                    {
                        Debug.Log("This script is firing up and messing everything");
                        c.transform.tag = "Untagged";
                        c.transform.position = assemblePoints[i].position;
                        c.transform.rotation = assemblePoints[i].rotation;
                        c.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        c.transform.SetParent(null);
                        assembleCount++;
                        break;
                    }
                }
            }
        }
        if(assembleCount == assemblePoints.Length && !hasSpawnedPrize)
        {
            Instantiate(prize, transform.transform.position + new Vector3(0, 1, 0), prize.transform.rotation);
            hasSpawnedPrize = true;
        }
    }
}
