using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CriticalCentury/Events", order = 0)]
public class GameEvents : ScriptableObject
{
    [Header("Event Information")]
    public string event_name;
    public string event_description;

}
