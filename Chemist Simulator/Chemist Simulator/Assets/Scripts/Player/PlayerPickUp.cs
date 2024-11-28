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

    public string GetHintForChemical(string chemicalName)
    {
        switch (chemicalName)
        {
            case "H2":
                return "Mixable with Oxygen.";
            case "Hydrogen":
                return "Mixable with Hydrogen.";
            case "Methane":
                return "Mixable with Oxygen.";
            case "Oxygen":
                return "Mixable with Hydrogen, Methane, Magnesium, and more.";
            case "Sodium Hydroxide":
                return "Mixable with Hydrochloric Acid.";
            case "Hydrochloric Acid":
                return "Mixable with Sodium Hydroxide, Zinc.";
            case "Sulfuric Acid":
                return "Mixable with Sodium Carbonate.";
            case "Sodium Carbonate":
                return "Mixable with Sulfuric Acid.";
            case "Copper Sulfate":
                return "Mixable with Iron.";
            case "Iron":
                return "Mixable with Copper Sulfate.";
            case "Zinc":
                return "Mixable with Hydrochloric Acid.";
            case "Silver Nitrate":
                return "Mixable with Sodium Chloride.";
            case "Sodium Chloride":
                return "Mixable with Silver Nitrate.";
            case "Lead Nitrate":
                return "Mixable with Potassium Iodide.";
            case "Potassium Iodide":
                return "Mixable with Lead Nitrate.";
            case "Calcium Carbonate":
                return "Heating.";
            case "Heat":
                return "Heatable with Calcium Carbonate, Cobalt Chloride (Hydrated).";
            case "Hydrogen Peroxide":
                return "Mixable with Manganese Dioxide.";
            case "Manganese Dioxide":
                return "Mixable with Hydrogen Peroxide.";
            case "Magnesium":
                return "Mixable with Oxygen.";
            case "Cobalt Chloride (Hydrated)":
                return "Mixable with Heat.";
            case "Cobalt Chloride (Anhydrous)":
                return "Hint: No known mixes.";
            default:
                return "Hint: No specific hint for this chemical. Try experimenting!";
        }
    }

}
