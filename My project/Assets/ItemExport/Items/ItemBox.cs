using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private GameObject deathEffects;

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
         {


            Debug.Log("Player collided with Itembox");
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                PassiveItem randomitem = inventory.GetRandomItem();
                inventory.AddItem(randomitem);
                Instantiate(deathEffects, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }


    }
}

