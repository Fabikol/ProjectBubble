using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<PassiveItem> availableItems;
    private List<PassiveItem> passiveItems = new List<PassiveItem>();
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null)
            Debug.LogError("PlayerStats component is missing.");
    }

    public void AddItem(PassiveItem item)
    {
        if (item == null || passiveItems.Contains(item)) return;

        passiveItems.Add(item);
        item.ApplyModifier(playerStats);
    }

    public PassiveItem GetRandomItem()
    {
        if (availableItems == null || availableItems.Count == 0)
        {
            Debug.LogWarning("No items available in inventory list.");
            return null;
        }
        return availableItems[Random.Range(0, availableItems.Count)];
    }
}


   
   
    



