using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchUpgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upgrade_title;
    [SerializeField] private TextMeshProUGUI upgrade_description;
    [SerializeField] private TextMeshProUGUI upgrade_status; 
    [SerializeField] private Image upgrade_background;
    [SerializeField] private Button button;
    
    public int time_remaining;

    private void Start() 
    {
        upgrade_background = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        upgrade_title = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        upgrade_description = this.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        upgrade_status = this.gameObject.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        button = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
    }

    public void UpdateUpgrade(Upgrade upgrade, bool canAfford)
    {
        upgrade_title.text = upgrade.upgrade_name;
        upgrade_description.text = upgrade.upgrade_description;
        upgrade_status.text = "$" + upgrade.upgrade_cost + "   " + upgrade.upgrade_duration + " year(s)";
        time_remaining = upgrade.upgrade_duration;

        if (canAfford)
        {
            upgrade_background.color = new Color32(255,255,255,255);
        }
        else
        {
            upgrade_background.color = new Color32(196,196,196,255);
        }
    }

    public void NoUpgrade()
    {
        upgrade_title.text = "";
        upgrade_description.text = "Upgrade your Town Hall to unlock more upgrades!";
        upgrade_status.text = "";
        upgrade_background.color = new Color32(255,255,255,150);
        button.interactable = false;
    }

    public void PurchaseUpgrade()
    {
        upgrade_status.text = "Time remaining: " + time_remaining + " years.";
        upgrade_background.color = new Color32(171,138,138,255);
    }
}
