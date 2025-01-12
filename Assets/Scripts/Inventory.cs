using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject targetObject; // Obiekt, który ma być aktywowany
    public event EventHandler<Item> OnItemSelected;

    private Dictionary<Item, GameObject> itemGameObjectMap;

    private void Awake() 
    {
        Instance = this;
        itemGameObjectMap = new Dictionary<Item, GameObject>();

        // Subskrybuj zdarzenie OnItemSelected
        OnItemSelected += HandleItemSelected;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        itemGameObjectMap.Clear();

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            itemGameObjectMap[item] = obj;

            obj.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));

            var selectedIndicator = obj.transform.Find("Selected");
            if (selectedIndicator != null)
            {
                selectedIndicator.gameObject.SetActive(false);
            }
        }
    }

    private void SelectItem(Item selectedItem)
    {
        foreach (var pair in itemGameObjectMap)
        {
            var selectedIndicator = pair.Value.transform.Find("Selected");
            if (selectedIndicator != null)
            {
                selectedIndicator.gameObject.SetActive(false);
            }
        }

        if (itemGameObjectMap.ContainsKey(selectedItem))
        {
            var selectedIndicator = itemGameObjectMap[selectedItem].transform.Find("Selected");
            if (selectedIndicator != null)
            {
                selectedIndicator.gameObject.SetActive(true);
            }
        }

        OnItemSelected?.Invoke(this, selectedItem);
    }

    private void HandleItemSelected(object sender, Item selectedItem)
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Aktywuj obiekt
        }
    }
}
