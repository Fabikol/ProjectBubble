using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using System.Collections; // Make sure this is at the top

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
        
            Debug.Log("Replacing current item with a new one.");
            currentItem = item;
        
        
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
    if (currentItem != null)
    {
        currentItem.ApplyModifier(playerStats);
        Debug.Log("Itembutton pressed, applying values");

        StartCoroutine(ResetItemAfterDuration(5f)); // 5 seconds duration
    }
}

private IEnumerator ResetItemAfterDuration(float duration)
{
    yield return new WaitForSeconds(duration);
    currentItem = null;
    Debug.Log("Item effect expired, inventory cleared.");
}






private void OnUseItem(InputValue value)
    {

            Debug.Log("Pressign E worked");
            TriggerItem();
    }
}


   
   
    



