using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CriticalCentury/Upgrade", order = 0)]
public class Upgrade : ScriptableObject {

    [Header("Upgrade Information")]
    public readonly string upgrade_name;
    public readonly string upgrade_description;
    public readonly int upgrade_cost;
    public readonly int upgrade_duration;
    public readonly List<Upgrade> prerequisites; 
    
    [Header("Upgrade Effects")]
    public readonly int money_output; //could be positive or negative
    public readonly int energy_output;
    public readonly int emission_output;
    public readonly int happiness_output;
    public readonly GameEvents triggered_event; // happens year following buying the upgrade - events can only be triggered ONCE! 
}

