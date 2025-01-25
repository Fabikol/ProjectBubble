using UnityEngine;

public interface IItem
{
    void Use(GameObject player); // use
    public void Collect(GameObject collector);
}