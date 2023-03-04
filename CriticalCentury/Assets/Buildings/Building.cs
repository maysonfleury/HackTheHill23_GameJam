using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{    
    private GameManager game_manager;
    private PlayerResources player_resources;

    [SerializeField] private int building_level; 
    [SerializeField] private List<Sprite> building_icons;

    private List<Upgrade> available_upgrades;
    [SerializeField] private List<Upgrade> level1_upgrades;
    [SerializeField] private List<Upgrade> level2_upgrades;
    [SerializeField] private List<Upgrade> level3_upgrades;
    [SerializeField] private List<Upgrade> level4_upgrades;

    private void Start() 
    {
        player_resources = FindObjectOfType<PlayerResources>();
    }

    public void UpgradeBuilding()
    {
        building_level += 1;

        if (building_level == 1)
            available_upgrades = new List<Upgrade>(level1_upgrades);
        else if (building_level == 2)
            available_upgrades = new List<Upgrade>(level2_upgrades);
        else if (building_level == 3)
            available_upgrades = new List<Upgrade>(level3_upgrades);
        else if (building_level == 4)
            available_upgrades = new List<Upgrade>(level4_upgrades);
    }

    public void TryUpgrade(int upgrade_number) // 1 to 4 based on which upgrade
    {
        // Check to see if player has sufficient funds
        if (available_upgrades[upgrade_number].upgrade_cost > player_resources.money)
        {
            game_manager.AlertPlayer("Insufficient Funds!");
            return;
        }

        // Check to see if player has the prerequisities 
        foreach(Upgrade upgrade in available_upgrades[upgrade_number].prerequisites)
        {
            if (!player_resources.active_upgrades.Contains(upgrade))
            {
                game_manager.AlertPlayer("Missing the following prerequisites:");
                return;
            }
        }

        player_resources.AddUpgrade(available_upgrades[upgrade_number]);
    }
}
