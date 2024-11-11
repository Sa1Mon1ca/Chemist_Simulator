using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Table_Script : MonoBehaviour
{
    
    public Transform Chemical_1_Slot;
    public Transform Chemical_2_Slot;

    private PickUp pickUpScript;

    private void Start()
    {
        pickUpScript = FindObjectOfType<PickUp>();
        //pickUpScript = GameObject.Find("Cube").GetComponent<PickUp>();
        //pickUpScript = GameObject.Find("Cube (1)").GetComponent<PickUp>();

        if (pickUpScript == null)
        {
            Debug.LogError("PickUp script not found in the scene.");
        }
    }

    private void Update()
    {

    }

    private void DropOnTable()
    {
        
    }

    private void PlaceChemicalInSlot(Transform slot)
    {
        if (pickUpScript != null && pickUpScript.Chemical != null)
        {
            pickUpScript.Chemical.transform.position = slot.position;
            pickUpScript.Chemical.transform.rotation = slot.rotation;


            pickUpScript.Chemical.GetComponent<Rigidbody>().isKinematic = false;
            pickUpScript.Chemical.GetComponent<MeshCollider>().enabled = true;

            pickUpScript.Chemical.transform.SetParent(slot);

            //PickUp.isHoldingItem = false;
        }
        else
        {
            Debug.LogError("pickUpScript or Chemical is not assigned.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (pickUpScript.PickUpSlot.childCount == 1)
                {
                    Transform itemInSlot = pickUpScript.PickUpSlot.GetChild(0);
                    if (itemInSlot == pickUpScript.Chemical.transform)
                    { 
                        if (Chemical_1_Slot.childCount == 0)
                        {
                            PlaceChemicalInSlot(Chemical_1_Slot);
                        }
                        else if (Chemical_2_Slot.childCount == 0)
                        {
                            PlaceChemicalInSlot(Chemical_2_Slot);
                        }
                        else
                        {
                            Debug.Log("Full");
                        }
                    }  
                }
            }
        }

    }
}
