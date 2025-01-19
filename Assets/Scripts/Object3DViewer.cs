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
        if (itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);
        }

        itemPrefab = Instantiate(item.prefab, new Vector3(100, 100, 100), Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemPrefab != null)
        {
            itemPrefab.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
        }
    }
}
