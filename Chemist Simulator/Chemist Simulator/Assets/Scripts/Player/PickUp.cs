using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Table_Script tableScript;
    public GameObject Chemical;
    public Transform PickUpSlot;

    private static bool isHoldingItem = false;

    void Start()
    {
        
        Chemical.GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.Q) && isHoldingItem)
        {
            Drop();
        }
    }

    void Drop()
    {
        
        PickUpSlot.DetachChildren();
        Chemical.GetComponent<Rigidbody>().isKinematic = false;
        Chemical.GetComponent<MeshCollider>().enabled = true;

        
        isHoldingItem = false;
    }

    void PickUpChemical()
    {
        
        Chemical.GetComponent<Rigidbody>().isKinematic = true;
        Chemical.GetComponent<MeshCollider>().enabled = false;

        
        Chemical.transform.position = PickUpSlot.position;
        Chemical.transform.rotation = PickUpSlot.rotation;
        Chemical.transform.SetParent(PickUpSlot);

        isHoldingItem = true;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
           
            if (!isHoldingItem)
            {
                PickUpChemical();
                tableScript.Chemical_1_Slot = other.gameObject.transform;
            }
        }
    }
}
