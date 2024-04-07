using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Health: MonoBehaviour
{
    public NetPlayerController player;
    public int health;
    public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            UpdateLife();
            UpdateLifeText();
        }
        else
        {
            player = FindObjectOfType<NetPlayerController>();
        }
    }
    public void UpdateLife()
    {
        if (player.lifePoints.Value > 0)
        {
            health = player.lifePoints.Value;
        }
        else
        {
            health = 0;
        }
    }

    public void UpdateLifeText()
    {
        healthText.text = "Health : " + health.ToString();
    }
}
