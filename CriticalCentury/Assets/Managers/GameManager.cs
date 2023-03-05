using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] public int current_year;
    [SerializeField] TextMeshProUGUI year_text;
    [SerializeField] PlayerResources player_resources;

    [SerializeField] List<Building> buildings;

    // Start is called before the first frame update
    void Start()
    {
        current_year = 1950;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextYear()
    {
        current_year += 1;
        year_text.text = "Current Year: " + current_year;

        player_resources.Pay(-1000);

        foreach(Building building in buildings)
        {
            building.NextYear();
        }

        // Update Player Resources
        // player_resources.UpdateResources();

        if (current_year == 2050)
            EndGame();

        // Trigger any events if there is any 
        TriggerEvents();

        // Show a report to the player if there is any
        PlayerReport();
    }

    void TriggerEvents()
    {
        if (player_resources.money < 0)
        {
            return;
        }
    }

    public void AlertPlayer(string alert)
    {

    }

    void PlayerReport()
    {
        return;
    }

    void EndGame()
    {
        if (player_resources.emissions == 0)
        {
          
        }
        else if (player_resources.emissions < 100)
        {

        }
        else if (player_resources.emissions < 1000)
        {

        }
        else 
        {

        }
    }
}
