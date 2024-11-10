using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject Chemical;
    public Transform PickUpSlot;

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
        Chemical.transform.eulerAngles = new Vector3(Chemical.transform.position.x, Chemical.transform.position.z, Chemical.transform.position.y);
        Chemical.GetComponent<Rigidbody>().isKinematic = false;
        Chemical.GetComponent<MeshCollider>().enabled = true;
    }

    void PickUpChemical()
    {
        Chemical.GetComponent<Rigidbody>().isKinematic = true;

        Chemical.transform.position = PickUpSlot.transform.position;
        Chemical.transform.rotation = PickUpSlot.transform.rotation;

        Chemical.GetComponent<MeshCollider>().enabled = false;

        Chemical.transform.SetParent(PickUpSlot);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                PickUpChemical();
            }
        }
        
    }

}
