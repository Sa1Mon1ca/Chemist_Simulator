using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Table_Script : MonoBehaviour
{
    public PickUp pickUpScript;
    public Transform Chemical_1_Slot;
    public Transform Chemical_2_Slot;

    public bool hasChemical = true;

    private void Update()
    {

    }

    private void DropOnTable()
    {
       Transform targetSlot = Chemical_1_Slot.childCount == 0 ? Chemical_1_Slot : Chemical_2_Slot;
       pickUpScript.Chemical.transform.position = Chemical_1_Slot.transform.position;
       pickUpScript.Chemical.transform.rotation = Chemical_1_Slot.transform.rotation;

       pickUpScript.Chemical.GetComponent<Rigidbody>().isKinematic = false;
       pickUpScript.Chemical.GetComponent<MeshCollider>().enabled = true;

       pickUpScript.Chemical.transform.SetParent(targetSlot);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.Q))
            {
                DropOnTable();
            }
        }

    }
}
