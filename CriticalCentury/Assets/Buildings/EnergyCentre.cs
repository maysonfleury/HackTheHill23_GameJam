using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class EnergyCentre : Building
{
    // Upgrade to UNLOCK
    // Then can pay more to increase maximum load
    // Can also decreate load at any time
    
    [SerializeField] public List<int> energy_values;
    [SerializeField] public List<int> emission_values;
    [SerializeField] private List<int> current_loads;
    [SerializeField] private List<int> max_loads;
    [SerializeField] private List<int> load_caps;
    // coal, oil, gas, nuclear, wind, solar

    [SerializeField] public int cost;
    [SerializeField] private List<EnergySource> energy_sources;

    private void Start() 
    {
        UpdateEnergyValues();
    }

    public void IncreaseLoad(int index)
    {
        if (current_loads[index] < max_loads[index])
        {
            current_loads[index]++;
            UpdateEnergyValue(index);
        }
        else if (current_loads[index] < load_caps[index])
        {
            energy_sources[index].UpgradeLoadCapacity(index);
        }
    }

    public void DecreaseLoad(int index)
    {
        if (current_loads[index] > 0)
        {
            current_loads[index]--;
            UpdateEnergyValue(index);
        }
    }

    public void UnlockEnergyType(int index)
    {
        energy_sources[index].UnlockEnergySource(index);
    }

    void UpdateEnergyValues()
    {
        for(int i = 0; i < 6; i++)
        {
            if (load_caps[i] > 0)
                UpdateEnergyValue(i);
        }
    }

    public void PurchaseLoadIncrease()
    {
        if(TryLoadUpgrade(cost))
        {
            PayUpgrade(cost);

            if (cost == 1000)
            {
                max_loads[0]++;
                UpdateEnergyValue(0);
            }
            else if (cost == 1500)
            {
                max_loads[1]++;
                UpdateEnergyValue(1);
            }
            else if (cost == 2500)
            {
                max_loads[2]++;
                UpdateEnergyValue(2);
            }
            else if (cost == 5000)
            {
                max_loads[3]++;
                UpdateEnergyValue(3);
            }
            else if (cost == 7500)
            {
                max_loads[4]++;
                UpdateEnergyValue(4);
            }
            else
            {
                max_loads[5]++; 
                UpdateEnergyValue(5);
            }         
        }
    }

    void UpdateEnergyValue(int index)
    {
        int energy = energy_values[index] * current_loads[index];
        int emission = emission_values[index] * current_loads[index];

        energy_sources[index].UpdateEnergySource(energy, emission, current_loads[index], max_loads[index], load_caps[index]);
    }

    public override bool TryUpgrade(int upgrade_number) // 1 to 4 based on which upgrade
    {
        return false;
    }
}
