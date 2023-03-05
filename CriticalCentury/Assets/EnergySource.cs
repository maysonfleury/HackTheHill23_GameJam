using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergySource : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energy_title;
    [SerializeField] private TextMeshProUGUI energy_output;
    [SerializeField] private TextMeshProUGUI emission_output;
    [SerializeField] private Image energy_background;

    [SerializeField] private GameObject fuel_load_parent;
    [SerializeField] private TextMeshProUGUI fuel_load;
    [SerializeField] private Button increase_load;
    [SerializeField] private Button decrease_load;

    [SerializeField] private GameObject confirm_purchase;
    [SerializeField] private TextMeshProUGUI purchase_text;

    [SerializeField] EnergyCentre energy_centre;

    private void Start() 
    {
        energy_centre = FindObjectOfType<EnergyCentre>();
        energy_background = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        energy_title = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        energy_output = this.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        emission_output = this.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        fuel_load_parent = this.gameObject.transform.GetChild(4).gameObject;
        fuel_load = fuel_load_parent.GetComponent<TextMeshProUGUI>();
        decrease_load = fuel_load_parent.transform.GetChild(0).gameObject.GetComponent<Button>();
        increase_load = fuel_load_parent.transform.GetChild(1).gameObject.GetComponent<Button>();
        purchase_text = confirm_purchase.transform.GetChild(4).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void UnlockEnergySource(int energy_type)
    {
        string energy_text;
        
        if (energy_type == 2)
            energy_text = "Natural Gas";
        else if (energy_type == 3)
            energy_text = "Nuclear";
        else if (energy_type == 4)
            energy_text = "Wind";
        else 
            energy_text = "Solar";

        energy_background.color = new Color32(255,255,255,255);
        energy_title.text = energy_text;
        energy_output.text = "Current Output:\n0 kWh";
        emission_output.text = "0 kg CO2";
        fuel_load.text = "Load 0/0";
        fuel_load_parent.SetActive(true);
    }

    public void UpgradeLoadCapacity(int energy_type)
    {
        string energy_text;
        int upgrade_cost;

        if (energy_type == 0)
        {
            energy_text = "Coal";
            upgrade_cost = 1000;
        }   
        else if (energy_type == 1)
        {
            energy_text = "Crude Oil";
            upgrade_cost = 1500;
        }
        else if (energy_type == 2)
        {
            energy_text = "Natural Gas";
            upgrade_cost = 2500;
        }
        else if (energy_type == 3)
        {
            energy_text = "Nuclear";
            upgrade_cost = 5000;
        }
        else if (energy_type == 4)
        {
            energy_text = "Wind";
            upgrade_cost = 7500;
        }
        else
        {
            energy_text = "Solar";
            upgrade_cost = 10000;
        }

        energy_centre.cost = upgrade_cost;
        confirm_purchase.SetActive(true);
        purchase_text.text = "1 " + energy_text + " factory for $" + upgrade_cost;

    }

    public void UpdateEnergySource(int energy, int emission, int load, int max_load, int load_cap)
    {
        energy_output.text = "Current Output: \n" + energy + " kWh";
        emission_output.text = emission + " kg CO2";
        fuel_load.text = "Load " + load + "/" + max_load;

        if (load == 0)
        {
            decrease_load.GetComponent<Image>().color = new Color32(175,175,175,205);
        }
        else 
        {
            decrease_load.GetComponent<Image>().color = new Color32(255,255,255,255);
        }

        if (load_cap > max_load || max_load > load)
        {
            increase_load.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        else
        {
            increase_load.GetComponent<Image>().color = new Color32(175,175,175,205);
        }
    }
}
