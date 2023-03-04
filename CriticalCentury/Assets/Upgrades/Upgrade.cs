using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "CriticalCentury/Upgrade", order = 0)]
public class Upgrade : ScriptableObject {

    [Header("Upgrade Information")]
    public string upgrade_name;
    public string upgrade_description;
    public int upgrade_cost;
    public int upgrade_duration;
    public List<Upgrade> prerequisites; 
    
    [Header("Upgrade Effects")]
    public int money_output; //could be positive or negative
    public int energy_output;
    public int emission_output;
    public int happiness_output;
    public GameEvents triggered_event; // happens year following buying the upgrade - events can only be triggered ONCE! 
}

