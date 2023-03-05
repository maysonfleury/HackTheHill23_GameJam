using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Event_Type { Good, Bad, Neutral, Report };
[CreateAssetMenu(fileName = "CriticalCentury/Events", order = 0)]
public class GameEvents : ScriptableObject
{
    [Header("Event Information")]
    public string event_name;
    public string event_description;

    public Event_Type event_type;
}
