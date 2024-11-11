using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject Chemical;
    public Transform PickUpSlot;

    //public static bool isHoldingItem = false;

    void Start()
    {
        
        Chemical.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Q))
        {
            Drop();
        }
    }

    void Drop()
    { 
        PickUpSlot.DetachChildren();
        Chemical.GetComponent<Rigidbody>().isKinematic = false;
        Chemical.GetComponent<MeshCollider>().enabled = true;
        //isHoldingItem = false;
    }

    void PickUpChemical()
    {
        //isHoldingItem = true;

        Chemical.GetComponent<Rigidbody>().isKinematic = true;
        Chemical.GetComponent<MeshCollider>().enabled = false;

        
        Chemical.transform.position = PickUpSlot.position;
        Chemical.transform.rotation = PickUpSlot.rotation;
        Chemical.transform.SetParent(PickUpSlot);   
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
           
            //if (!isHoldingItem)
            {
                PickUpChemical();
            }
        }
    }
}
