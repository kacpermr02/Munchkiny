using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void Pickup()
    {
        Inventory.Instance.Add(Item); // Poprawiona nazwa zmiennej
        Destroy(gameObject);
    }

    private void OnMouseDown() 
    {
        Pickup();
    }
}

