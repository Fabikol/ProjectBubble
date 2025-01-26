using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "Scriptable Objects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string name; 
    [SerializeField] private string description; 
    [SerializeField] private string statToModify; 
    [SerializeField] private float multiplier;

    public string Name => name;
    public string Description => description;
    public string StatToModify => statToModify;
    public float Multiplier => multiplier;
}


