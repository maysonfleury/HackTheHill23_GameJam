using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResources : MonoBehaviour
{
    public int money;
    public int energy;
    public int emissions;
    public int happiness; 

    [SerializeField] private TextMeshProUGUI money_text;

    public List<Upgrade> active_upgrades;
    [SerializeField] private EnergyCentre energy_centre;

    private void Start() 
    {
        money_text.text = "Money: $" + money;
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        active_upgrades.Add(upgrade);
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        active_upgrades.Remove(upgrade);
    }

    public void Pay(int cost)
    {
        money -= cost;
        money_text.text = "Money: $" + money;
    }

    public void UpdateResources()
    {
        foreach(Upgrade upgrade in active_upgrades)
        {
            money += upgrade.money_output;
            energy += upgrade.energy_output; // exclusively for losing energy
            emissions += upgrade.emission_output;
            happiness += upgrade.happiness_output;
        }
    }
}
