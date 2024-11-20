﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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

    private ObjectiveManager objectiveManager;
    private PlayerPickUp pickUpScript;  // Reference to the PlayerPickUp script
    public TMP_Text HintText;  // Reference to the HintText from PlayerPickUp script

    private Dictionary<string, List<string>> possibleMixes = new Dictionary<string, List<string>>();

    private void Start()
    {
        InitializeMixes();

        GameObject chemicalObject = GameObject.FindGameObjectWithTag("Chemical");
        if (chemicalObject != null)
        {
            pickUpScript = chemicalObject.GetComponent<PlayerPickUp>();
        }
        GameObject managerObject = GameObject.FindGameObjectWithTag("ObjectiveManager");
        if (managerObject != null)
        {
            objectiveManager = managerObject.GetComponent<ObjectiveManager>();
        }

        // Make sure the hint text is empty at the start
        if (HintText != null)
        {
            HintText.text = "";
        }
    }

    private void InitializeMixes()
    {
        // Define possible mixes for each chemical
        possibleMixes["H2"] = new List<string> { "Oxygen" };
        possibleMixes["Oxygen"] = new List<string> { "H2", "Methane", "Magnesium", "Hydrogen Peroxide", "Heat", "Copper Sulfate" };
        possibleMixes["Methane"] = new List<string> { "Oxygen" };
        possibleMixes["Hydrogen"] = new List<string> { "H2" };
        possibleMixes["Hydrogen (Clone)"] = new List<string> { "Hydrogen" };
        possibleMixes["Sodium Hydroxide"] = new List<string> { "Hydrochloric Acid" };
        possibleMixes["Hydrochloric Acid"] = new List<string> { "Sodium Hydroxide", "Zinc", "Silver Nitrate" };
        possibleMixes["Sulfuric Acid"] = new List<string> { "Sodium Carbonate" };
        possibleMixes["Sodium Carbonate"] = new List<string> { "Sulfuric Acid" };
        possibleMixes["Copper Sulfate"] = new List<string> { "Iron" };
        possibleMixes["Iron"] = new List<string> { "Copper Sulfate" };
        possibleMixes["Zinc"] = new List<string> { "Hydrochloric Acid" };
        possibleMixes["Silver Nitrate"] = new List<string> { "Sodium Chloride" };
        possibleMixes["Sodium Chloride"] = new List<string> { "Silver Nitrate" };
        possibleMixes["Lead Nitrate"] = new List<string> { "Potassium Iodide" };
        possibleMixes["Potassium Iodide"] = new List<string> { "Lead Nitrate" };
        possibleMixes["Calcium Carbonate"] = new List<string> { "Heat" };
        possibleMixes["Heat"] = new List<string> { "Calcium Carbonate", "Cobalt Chloride (Hydrated)" };
        possibleMixes["Hydrogen Peroxide"] = new List<string> { "Manganese Dioxide" };
        possibleMixes["Manganese Dioxide"] = new List<string> { "Hydrogen Peroxide" };
        possibleMixes["Magnesium"] = new List<string> { "Oxygen" };
        possibleMixes["Cobalt Chloride (Hydrated)"] = new List<string> { "Heat" };
        possibleMixes["Cobalt Chloride (Anhydrous)"] = new List<string> { };
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
        return Chemical_1_Slot.childCount > 0 && Chemical_2_Slot.childCount > 0;
    }

    private void MixChemicals()
    {
        string chemical1 = Chemical_1_Slot.GetChild(0).name;
        string chemical2 = Chemical_2_Slot.GetChild(0).name;

        if (possibleMixes.ContainsKey(chemical1) && possibleMixes[chemical1].Contains(chemical2) || possibleMixes.ContainsKey(chemical2) && possibleMixes[chemical2].Contains(chemical1))
        {
            ProcessCombination(chemical1, chemical2);
        }
        else
        {
            ShowHintText("These chemicals cannot be mixed.");
        }
    }

    private void ProcessCombination(string chemical1, string chemical2)
    {
        string resultChemical = "";

        if ((chemical1 == "H2" && chemical2 == "Oxygen") || (chemical1 == "Oxygen" && chemical2 == "H2"))
        {
            resultChemical = "Water";
            CreateResultingChemical(WaterPrefab);
            objectiveManager.SetObjective("Great! Now mix Methane and Oxygen to create Carbon Dioxide");
            ShowHintText("You created Water by mixing Hydrogen and Oxygen!");
        }
        else if ((chemical1 == "Hydrogen" && chemical2 == "Hydrogen") || (chemical1 == "Hydrogen (Clone)" && chemical2 == "Hydrogen (Clone)"))
        {
            resultChemical = "H2";
            CreateResultingChemical(H2Prefab);
            objectiveManager.SetObjective("Now mix Methane and Oxygen to create Carbon Dioxide.");
            ShowHintText("You created Hydrogen!");
        }
        else if ((chemical1 == "Methane" && chemical2 == "Oxygen") || (chemical1 == "Oxygen" && chemical2 == "Methane"))
        {
            resultChemical = "Carbon Dioxide + Water";
            CreateResultingChemical(CO2Prefab);
            CreateResultingChemical(WaterPrefab);
            objectiveManager.SetObjective("Great! Now mix Hydrogen and Oxygen to create Water.");
            ShowHintText("You created Carbon Dioxide and Water!");
        }
        else if ((chemical1 == "Hydrochloric Acid" && chemical2 == "Sodium Hydroxide") || (chemical1 == "Sodium Hydroxide" && chemical2 == "Hydrochloric Acid"))
        {
            resultChemical = "Sodium Chloride + Water";
            CreateResultingChemical(SodiumChloridePrefab);
            CreateResultingChemical(WaterPrefab);
            objectiveManager.SetObjective("Next, mix Sulfuric Acid and Sodium Carbonate to create Sodium Sulfate.");
            ShowHintText("You created Sodium Chloride and Water!");
        }
        else if ((chemical1 == "Sulfuric Acid" && chemical2 == "Sodium Carbonate") || (chemical1 == "Sodium Carbonate" && chemical2 == "Sulfuric Acid"))
        {
            resultChemical = "Sodium Sulfate + Carbon Dioxide + Water";
            CreateResultingChemical(SodiumSulfatePrefab);
            CreateResultingChemical(CO2Prefab);
            CreateResultingChemical(WaterPrefab);
            objectiveManager.SetObjective("Great! Now mix Copper Sulfate and Iron to create Iron Sulfate.");
            ShowHintText("You created Sodium Sulfate, Carbon Dioxide, and Water!");
        }
        else if ((chemical1 == "Copper Sulfate" && chemical2 == "Iron") || (chemical1 == "Iron" && chemical2 == "Copper Sulfate"))
        {
            resultChemical = "Iron Sulfate + Copper";
            CreateResultingChemical(IronSulfatePrefab);
            CreateResultingChemical(CopperPrefab);
            objectiveManager.SetObjective("Now, mix Zinc and Hydrochloric Acid to create Zinc Chloride.");
            ShowHintText("You created Iron Sulfate and Copper!");
        }
        else if ((chemical1 == "Zinc" && chemical2 == "Hydrochloric Acid") || (chemical1 == "Hydrochloric Acid" && chemical2 == "Zinc"))
        {
            resultChemical = "Zinc Chloride + Hydrogen";
            CreateResultingChemical(ZincChloridePrefab);
            CreateResultingChemical(H2Prefab);
            objectiveManager.SetObjective("Nice! Mix Lead Nitrate and Potassium Iodide to create Lead Iodide.");
            ShowHintText("You created Zinc Chloride and Hydrogen!");
        }
        else if ((chemical1 == "Lead Nitrate" && chemical2 == "Potassium Iodide") || (chemical1 == "Potassium Iodide" && chemical2 == "Lead Nitrate"))
        {
            resultChemical = "Lead Iodide";
            CreateResultingChemical(LeadIodidePrefab);
            objectiveManager.SetObjective("Awesome! Mix Sodium Chloride and Silver Nitrate to create Silver Chloride.");
            ShowHintText("You created Lead Iodide!");
        }
        else if ((chemical1 == "Sodium Chloride" && chemical2 == "Silver Nitrate") || (chemical1 == "Silver Nitrate" && chemical2 == "Sodium Chloride"))
        {
            resultChemical = "Silver Chloride";
            CreateResultingChemical(SilverChloridePrefab);
            objectiveManager.SetObjective("Nice! Mix Lead Nitrate and Potassium Iodide to create Lead Iodide.");
            ShowHintText("You created Silver Chloride!");
        }
    }

    private void CreateResultingChemical(GameObject prefab)
    {
        Instantiate(prefab, Result_Slot.position, Quaternion.identity, Result_Slot);
    }

    private void ShowHintText(string message)
    {
        if (HintText != null)
        {
            HintText.text = message;
        }
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
