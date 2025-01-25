using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<PassiveItem> availableItems;
    private List<PassiveItem> passiveItems = new List<PassiveItem>();
    private PlayerStats playerStats;

    private PassiveItem currentItem;



    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null)
            Debug.LogError("PlayerStats component is missing."); 
    }

    public void AddItem(PassiveItem item)
    {
        if (item == null) return;

        if (currentItem == null)
        {
            Debug.Log("Replacing current item with a new one.");
            currentItem = item;
        }
        
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

    public void TriggerItem()
    {
        
            currentItem.ApplyModifier(playerStats);
            Debug.Log("Itembutton pressed, applying values");
  
    }

    


    private void OnUseItem(InputValue value)
    {
        Debug.Log("Pressign E worked");
        TriggerItem();
    }
}


   
   
    



