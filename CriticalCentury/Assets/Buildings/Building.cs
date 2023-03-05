using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{    
    [SerializeField] GameManager game_manager;
    [SerializeField] public PlayerResources player_resources;

    [SerializeField] public int building_level; 

    public List<Upgrade> available_upgrades;
    [SerializeField] private List<Upgrade> level1_upgrades;
    [SerializeField] private List<Upgrade> level2_upgrades;
    [SerializeField] private List<Upgrade> level3_upgrades;
    [SerializeField] private List<Upgrade> level4_upgrades;

    private void Start() 
    {
        player_resources = FindObjectOfType<PlayerResources>();
        game_manager = FindObjectOfType<GameManager>();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        player_resources.AddUpgrade(upgrade);
    }

    public void PayUpgrade(int cost)
    {
        player_resources.Pay(cost);
    }

    public void UpgradeBuilding()
    {
        building_level += 1;

        if (building_level == 1)
            available_upgrades.AddRange(level1_upgrades);
        else if (building_level == 2)
            available_upgrades.AddRange(level2_upgrades);
        else if (building_level == 3)
            available_upgrades.AddRange(level3_upgrades);
        else if (building_level == 4)
            available_upgrades.AddRange(level4_upgrades);
    }

    public virtual void NextYear()
    {
        return;
    }

    public bool TryLoadUpgrade(int cost)
    {
        if (cost > player_resources.money)
        {
            StartCoroutine(game_manager.AlertPlayer("Insufficient Funds!"));
            //Debug.Log("Insufficient Funds!");
            return false;
        }
        return true;
    }

    public virtual bool TryUpgrade(int upgrade_number) // 1 to 4 based on which upgrade
    {
        // Check to see if player has sufficient funds
        if (available_upgrades[upgrade_number].upgrade_cost > player_resources.money)
        {
            //game_manager.AlertPlayer("Insufficient Funds!");
            Debug.Log("Insufficient Funds!");
            return false;
        }

        // Check to see if player has the prerequisities 
        foreach(Upgrade upgrade in available_upgrades[upgrade_number].prerequisites)
        {
            if (!player_resources.active_upgrades.Contains(upgrade))
            {
                StartCoroutine(game_manager.AlertPlayer("Missing the following prerequisite: " + upgrade.upgrade_name));
                return false;
            }
        }

        return true;

        //player_resources.AddUpgrade(available_upgrades[upgrade_number]);
    }
}
