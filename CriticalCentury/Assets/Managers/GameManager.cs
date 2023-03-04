using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public int current_year;
    [SerializeField] PlayerResources player_resources;

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
        // Update Player Resources
        player_resources.UpdateResources();

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
}
