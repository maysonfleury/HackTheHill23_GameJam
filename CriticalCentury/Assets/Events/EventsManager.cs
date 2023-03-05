using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> event_icons; // 0 - report, 1 - bad, 2 - neutral, 3 - good
    [SerializeField] private Image active_icon;

    [SerializeField] private TextMeshProUGUI event_title;
    [SerializeField] private TextMeshProUGUI event_description;

    public void ReadEvent(GameEvents game_event)
    {
        if(game_event.event_type == Event_Type.Report)
        {
            active_icon.sprite = event_icons[0];
        }
        else if(game_event.event_type == Event_Type.Bad)
        {
            active_icon.sprite = event_icons[1];
        }
        else if(game_event.event_type == Event_Type.Neutral)
        {
            active_icon.sprite = event_icons[2];
        }
        else 
        {
            active_icon.sprite = event_icons[3];
        }

        event_title.text = game_event.event_name;
        event_description.text = game_event.event_description;
    }
}
