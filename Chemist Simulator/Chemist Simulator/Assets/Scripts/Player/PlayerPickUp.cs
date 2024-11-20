using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPickUp : MonoBehaviour
{
    [Header("Pick Up Settings")]
    public Transform PickUpSlot; // The slot where the picked-up item will be held
    public float PickUpRange = 2f; // Maximum range to pick up an item
    public KeyCode PickUpKey = KeyCode.E; // Key to pick up or drop items
    public KeyCode DropToSlotKey = KeyCode.F;

    [HideInInspector] public GameObject Chemical; 
    public static bool isHoldingItem = false; // Check whether the player is holding an item

    public TMP_Text HintText;
    

    private void Update()
    {
        if (Input.GetKeyDown(PickUpKey))
        {
            if (isHoldingItem)
            {
                DropItem();
            }
            else
            {
                TryPickUpItem();
            }
        }
        if (Input.GetKeyDown(DropToSlotKey) && isHoldingItem)
        {
            TryDropToSlot();
        }
    }

    private void TryPickUpItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, PickUpRange))
        {
            // Check if the hit object has the "Chemical" tag
            if (hit.collider.CompareTag("Chemical"))
            {
                PickUpItem(hit.collider.gameObject);
            }
        }
    }


    private void PickUpItem(GameObject item)
    {
        if (isHoldingItem)
        {
            return; // Prevent picking up multiple items
        }

        // Attach the item to the player's pick-up slot
        Chemical = item;
        Chemical.transform.SetParent(PickUpSlot);
        Chemical.transform.localPosition = Vector3.zero; 
        Chemical.transform.localRotation = Quaternion.identity; 

        // Disable item's physics for holding
        Rigidbody rb = Chemical.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider collider = Chemical.GetComponent<Collider>();
        if (collider != null) collider.enabled = false;

        isHoldingItem = true;

        string chemicalName = item.name;
        string hint = GetHintForChemical(chemicalName);
        HintText.text = hint;
    }

    private void DropItem()
    {
        if (!isHoldingItem || Chemical == null) return;

        // Detach the item from the player
        Chemical.transform.SetParent(null);

        // Re-enable physics for the item
        Rigidbody rb = Chemical.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        Collider collider = Chemical.GetComponent<Collider>();
        if (collider != null) collider.enabled = true;

        
        //Drop the item in front of the player
        Chemical.transform.position = transform.position + transform.forward;

        Chemical = null;
        isHoldingItem = false;
    }

    private void TryDropToSlot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, PickUpRange))
        {
            Table_Script table = hit.collider.GetComponent<Table_Script>();
            if (table != null)
            {
                table.DropChemicalToSlot(this);
            }
        }
    }

    private string GetHintForChemical(string chemicalName)
    {
        switch (chemicalName)
        {
            case "H2":
                return "Hint: Mix Hydrogen with Oxygen to create Water.";
            case "Hydrogen":
                return "Hint: Mix Hydrogen and Hydrogen to create H2";
            case "Methane":
                return "Hint: Mix Methane with Oxygen to create Carbon Dioxide.";
            case "Oxygen":
                return "Hint: Oxygen can react with multiple chemicals like Hydrogen and Methane.";
            case "Sodium Hydroxide":
                return "Hint: Mix Sodium Hydroxide with Hydrochloric Acid to create Salt Water.";
            case "Hydrochloric Acid":
                return "Hint: Mix Hydrochloric Acid with Sodium Hydroxide or Zinc.";
            default:
                return "Hint: No specific hint for this chemical. Try experimenting!";
        }
    }

}
