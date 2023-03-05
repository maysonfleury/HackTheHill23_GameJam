using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchAndDevelopment : Building
{
    // This building has no functionality outside of Upgrades
    // The upgrades this buildings offer function mostly as prerequisites for other upgrades (Specifically Energy)

    public List<ResearchUpgrade> upgrade_buttons;
    public List<int> queued_upgrades;

    public TextMeshProUGUI building_lvl;

    private void Start() 
    {
        UpgradeBuilding();
        PopulateUpgrades();     
    }

    public override void NextYear()
    {
        foreach(int index in queued_upgrades)
        {
            upgrade_buttons[index].time_remaining -= 1;
            if (upgrade_buttons[index].time_remaining == 0)
            {
                AddUpgrade(available_upgrades[index]);
                available_upgrades.Remove(available_upgrades[index]);          
            }
        }
        PopulateUpgrades();
    }

    public void PopulateUpgrades()
    {
        int index = 0;
        building_lvl.text = "Building Level: " + building_level;
        
        foreach(Upgrade upgrade in available_upgrades)
        {
            if (upgrade_buttons[index].time_remaining == 0)
                queued_upgrades.Remove(index); 
                
            if (queued_upgrades.Contains(index))
            {
                upgrade_buttons[index].PurchaseUpgrade();
                index += 1;
                continue;
            }
            bool canAfford = TryUpgrade(index);
            upgrade_buttons[index].UpdateUpgrade(upgrade, canAfford);
            index += 1;
        }
        while (index < 4)
        {
            upgrade_buttons[index].NoUpgrade();
            index += 1;
        }
    }

    public void TryPurchaseUpgrade(int index)
    {
        if(TryUpgrade(index) && !queued_upgrades.Contains(index))
        {
            PayUpgrade(available_upgrades[index].upgrade_cost);  
            PopulateUpgrades();
            queued_upgrades.Add(index);
            upgrade_buttons[index].PurchaseUpgrade();                   
        }
    }

}
