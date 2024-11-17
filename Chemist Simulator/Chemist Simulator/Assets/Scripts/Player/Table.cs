using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Script : MonoBehaviour
{
    public Transform Chemical_1_Slot;
    public Transform Chemical_2_Slot;
    public Transform Result_Slot;
    public GameObject H2Prefab;
    public GameObject WaterPrefab; 
    public GameObject CO2Prefab;
    public GameObject SodiumChloridePrefab;
    public GameObject SodiumSulfatePrefab;
    public GameObject IronSulfatePrefab;
    public GameObject CopperPrefab;
    public GameObject ZincChloridePrefab;
    public GameObject SodiumNitratePrefab;
    public GameObject PotassiumNitratePrefab;
    public GameObject AnhydrousCobaltPrefab;
    public GameObject SilverChloridePrefab;
    public GameObject LeadIodidePrefab;
    public GameObject OxygenPrefab;
    public GameObject CalciumOxidePrefab;
    public GameObject MagnesiumOxidePrefab;



    private PlayerPickUp pickUpScript;

    private void Start()
    {
        
        GameObject chemicalObject = GameObject.FindGameObjectWithTag("Chemical");
        if (chemicalObject != null)
        {
            pickUpScript = chemicalObject.GetComponent<PlayerPickUp>();
        }
    }

    private void Update()
    {
        if (AreBothSlotsFull() && Input.GetKey(KeyCode.Return))
        {
            MixChemicals();
        }
    }

    private bool AreBothSlotsFull()
    {
        // Check if both slots have at least one child
        return Chemical_1_Slot.childCount > 0 && Chemical_2_Slot.childCount > 0;
    }

    private void MixChemicals()
    {
        string chemical1 = Chemical_1_Slot.GetChild(0).name;
        string chemical2 = Chemical_2_Slot.GetChild(0).name;

        //Check combinations
        if ((chemical1 == "Hydrogen" && chemical2 == "Oxygen") || (chemical1 == "Oxygen" && chemical2 == "Hydrogen"))
        {
            Debug.Log("Hydrogen + Oxygen → Water");
            CreateResultingChemical(WaterPrefab); // Water prefab
        }
        else if ((chemical1 == "Hydrogen" && chemical2 == "Hydrogen") || (chemical1 == "Hydrogen (Clone)" && chemical2 == "Hydrogen (Clone)"))
        {
            Debug.Log("Hydrogen + Hydrogen → H2");
            CreateResultingChemical(H2Prefab); // H2 prefab
        }
        else if ((chemical1 == "Methane" && chemical2 == "Oxygen") || (chemical1 == "Oxygen" && chemical2 == "Methane"))
        {
            Debug.Log("Methane + Oxygen → Carbon Dioxide + Water");
            CreateResultingChemical(CO2Prefab); // Prefab for CO2 and water combo
            CreateResultingChemical(WaterPrefab);
        }
        else if ((chemical1 == "Hydrochloric Acid" && chemical2 == "Sodium Hydroxide") || (chemical1 == "Sodium Hydroxide" && chemical2 == "Hydrochloric Acid"))
        {
            Debug.Log("Hydrochloric Acid + Sodium Hydroxide → Sodium Chloride + Water");
            CreateResultingChemical(SodiumChloridePrefab); // Prefab for salt water
            CreateResultingChemical(WaterPrefab);
        }
        else if ((chemical1 == "Sulfuric Acid" && chemical2 == "Sodium Carbonate") || (chemical1 == "Sodium Carbonate" && chemical2 == "Sulfuric Acid"))
        {
            Debug.Log("Sulfuric Acid + Sodium Carbonate → Sodium Sulfate + Carbon Dioxide + Water");
            CreateResultingChemical(SodiumSulfatePrefab);
            CreateResultingChemical(CO2Prefab);
            CreateResultingChemical(WaterPrefab);
        }
        else if ((chemical1 == "Copper Sulfate" && chemical2 == "Iron") || (chemical1 == "Iron" && chemical2 == "Copper Sulfate"))
        {
            Debug.Log("Copper Sulfate + Iron → Iron Sulfate + Copper");
            CreateResultingChemical(IronSulfatePrefab);
            CreateResultingChemical(CopperPrefab);
        }
        else if ((chemical1 == "Zinc" && chemical2 == "Hydrochloric Acid") || (chemical1 == "Hydrochloric Acid" && chemical2 == "Zinc"))
        {
            Debug.Log("Zinc + Hydrochloric Acid → Zinc Chloride + Hydrogen");
            CreateResultingChemical(ZincChloridePrefab);
            CreateResultingChemical(H2Prefab);
        }
        else if ((chemical1 == "Silver Nitrate" && chemical2 == "Sodium Chloride") || (chemical1 == "Sodium Chloride" && chemical2 == "Silver Nitrate"))
        {
            Debug.Log("Silver Nitrate + Sodium Chloride → Silver Chloride + Sodium Nitrate");
            CreateResultingChemical(SilverChloridePrefab);
            CreateResultingChemical(SodiumNitratePrefab);
        }
        else if ((chemical1 == "Lead Nitrate" && chemical2 == "Potassium Iodide") || (chemical1 == "Potassium Iodide" && chemical2 == "Lead Nitrate"))
        {
            Debug.Log("Lead Nitrate + Potassium Iodide → Lead Iodide + Potassium Nitrate");
            CreateResultingChemical(LeadIodidePrefab);
            CreateResultingChemical(PotassiumNitratePrefab);
        }
        else if ((chemical1 == "Calcium Carbonate" && chemical2 == "Heat") || (chemical1 == "Heat" && chemical2 == "Calcium Carbonate"))
        {
            Debug.Log("Calcium Carbonate → Calcium Oxide + Carbon Dioxide");
            CreateResultingChemical(CalciumOxidePrefab);
            CreateResultingChemical(CO2Prefab);
        }
        else if ((chemical1 == "Hydrogen Peroxide" && chemical2 == "Manganese Dioxide") || (chemical1 == "Manganese Dioxide" && chemical2 == "Hydrogen Peroxide"))
        {
            Debug.Log("Hydrogen Peroxide → Water + Oxygen (with catalyst)");
            CreateResultingChemical(WaterPrefab);
            CreateResultingChemical(OxygenPrefab);
        }
        else if ((chemical1 == "Magnesium" && chemical2 == "Oxygen") || (chemical1 == "Oxygen" && chemical2 == "Magnesium"))
        {
            Debug.Log("Magnesium + Oxygen → Magnesium Oxide");
            CreateResultingChemical(MagnesiumOxidePrefab);
        }
        else if ((chemical1 == "Cobalt Chloride (Hydrated)" && chemical2 == "Heat") || (chemical1 == "Heat" && chemical2 == "Cobalt Chloride (Hydrated)"))
        {
            Debug.Log("Cobalt Chloride (Hydrated) → Cobalt Chloride (Anhydrous) + Water");
            CreateResultingChemical(AnhydrousCobaltPrefab);
            CreateResultingChemical(WaterPrefab);
        }
        else
        {
            Debug.Log("The chemicals cannot be mixed.");
        }

        ClearSlots();
    }

    private void CreateResultingChemical(GameObject resultPrefab)
    {
        GameObject newChemical = Instantiate(resultPrefab, Result_Slot.position, Result_Slot.rotation);
        Debug.Log($"Created: {newChemical.name}");
    }

    private void ClearSlots()
    {
        foreach(Transform child in Chemical_1_Slot)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in Chemical_2_Slot)
        {
            Destroy(child.gameObject);
        }
    }

    private void PlaceChemicalInSlot(GameObject chemical, Transform slot, PlayerPickUp pickUpScript)
    {
        chemical.transform.SetParent(slot);
        chemical.transform.position = slot.position;
        chemical.transform.rotation = slot.rotation;

        // Re-enable physics for dropped chemical
        Rigidbody rb = chemical.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        Collider collider = chemical.GetComponent<Collider>();
        if (collider != null) collider.enabled = true;

        // Clear the pick-up script
        pickUpScript.Chemical = null;
        PlayerPickUp.isHoldingItem = false;

        Debug.Log($"Placed {chemical.name} in {slot.name}");
    }

    public void DropChemicalToSlot(PlayerPickUp pickUpScript)
    {
        if (pickUpScript == null || pickUpScript.Chemical == null)
        {
            Debug.LogError("No chemical to drop!");
            return;
        }

        if (Chemical_1_Slot.childCount == 0)
        {
            PlaceChemicalInSlot(pickUpScript.Chemical, Chemical_1_Slot, pickUpScript);
        }
        else if (Chemical_2_Slot.childCount == 0)
        {
            PlaceChemicalInSlot(pickUpScript.Chemical, Chemical_2_Slot, pickUpScript);
        }
        else
        {
            Debug.Log("Both chemical slots are full!");
        }
    }
}
