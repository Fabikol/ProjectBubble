using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Dictionary<string, float> statModifiers = new Dictionary<string, float>();

    public PlayerControl playerControl;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        if (playerControl == null)
            Debug.LogError("PlayerControl component is missing.");
    }

    private void Start()
    {
        statModifiers["maxSpeed"] = playerControl.maxSpeed; // Initialize with actual maxSpeed
    }

    public void Update()
    {
        
    }

    public void ApplyModifier(string statName, float multiplier)
    {
        if (!statModifiers.ContainsKey(statName))
        {
            Debug.LogWarning($"Stat {statName} does not exist in PlayerStats.");
            return;
        }

        statModifiers[statName] *= multiplier;

        switch (statName)
        {
            case "maxSpeed":
                if (playerControl != null) 
                {
                    playerControl.maxSpeed = statModifiers[statName];
                    Debug.Log($"maxSpeed changed to {playerControl.maxSpeed}");
                }
                else
                {
                    Debug.LogError("PlayerControl reference is missing!");
                }
                break;
            default:
                Debug.LogWarning($"Stat {statName} is not mapped in PlayerStats.");
                break;
        }
    }

}

