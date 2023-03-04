using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCentre : Building
{
    // Upgrade to UNLOCK
    // Then can pay more to increase maximum load
    // Can also decreate load at any time

    [SerializeField] private int coal_load;
    [SerializeField] private int oil_load;
    [SerializeField] private int gas_load;
    [SerializeField] private int nuclear_load;
    [SerializeField] private int wind_load;
    [SerializeField] private int hydro_load;
    [SerializeField] private int solar_load;

    void ChangeLoad()
    {

    }

    void UpdateEnergyValue(int energy)
    {
    }

    public override void TryUpgrade(int upgrade_number) // 1 to 4 based on which upgrade
    {
        return;
    }
}
