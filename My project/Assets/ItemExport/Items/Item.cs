using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveItem", menuName = "Scriptable Objects/Passive Item")]
public class PassiveItem : ScriptableObject
{
    [SerializeField] private ItemScriptableObject item;

    public void ApplyModifier(PlayerStats playerStats)
    {
        if (item == null) return;
        playerStats.ApplyModifier(item.StatToModify, item.Multiplier);
    }

    public string GetName() => item?.Name ?? "Unknown Item";
    public string GetDescription() => item?.Description ?? "No Description Available";
}

