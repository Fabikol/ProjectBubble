using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Dictionary<string, float> statModifiers = new Dictionary<string, float>();

    public float maxSpeed = 10f; // Default value

    private void Start()
    {
        // Initialize base stats
        statModifiers["maxSpeed"] = maxSpeed;
    }

    public void Update()
    {
        Debug.Log("current maxSpeed" + maxSpeed);
    }

    public void ApplyModifier(string statName, float multiplier)
    {
        if (!statModifiers.ContainsKey(statName))
        {
            Debug.LogWarning($"Stat {statName} does not exist in PlayerStats.");
            return;
        }

        // Modify and apply the stat
        statModifiers[statName] *= multiplier;

        switch (statName)
        {
            case "maxSpeed":
                maxSpeed = statModifiers[statName];
                break;
            default:
                Debug.LogWarning($"Stat {statName} is not mapped in PlayerStats.");
                break;
        }
    }
}

