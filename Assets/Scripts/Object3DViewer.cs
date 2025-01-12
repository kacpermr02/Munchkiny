using UnityEngine;
using UnityEngine.EventSystems;

public class Object3DViewer : MonoBehaviour, IDragHandler
{
    [SerializeField] private Inventory inventory;
    private Transform itemPrefab;

    void Start()
    {
        inventory.OnItemSelected += Inventory_OnItemSelected;
    }

    private void Inventory_OnItemSelected(object sender, Item item)
    {
        // Sprawdź, czy obiekt istnieje, a jeśli tak, zniszcz go
        if (itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);  // Zniszcz cały obiekt, a nie tylko transform
        }

        // Zainstancjonuj nowy obiekt
        itemPrefab = Instantiate(item.prefab, new Vector3(100, 100, 100), Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            // Obracaj obiekt
            itemPrefab.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
        }
    }
}
