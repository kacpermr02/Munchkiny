using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Itemy")]
    public List<Item> Items = new List<Item>();

    [Header("UI")]
    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject targetObject;

    public event EventHandler<Item> OnItemSelected;

    private Dictionary<Item, GameObject> itemGameObjectMap;
    private Item selectedItem;

    private void Awake()
    {
        Instance = this;
        itemGameObjectMap = new Dictionary<Item, GameObject>();
        OnItemSelected += HandleItemSelected;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Naciśnięto O – próba załadowania sceny...");
            LoadSelectedItemScene();
        }
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
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        itemGameObjectMap.Clear();

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            itemGameObjectMap[item] = obj;

            obj.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));

            Transform selectedIndicator = obj.transform.Find("Selected");
            if (selectedIndicator != null)
                selectedIndicator.gameObject.SetActive(false);
        }
    }

    private void SelectItem(Item item)
    {
        selectedItem = item;
        Debug.Log($"Wybrano item: {item.itemName}");

        foreach (var pair in itemGameObjectMap)
        {
            Transform selectedIndicator = pair.Value.transform.Find("Selected");
            if (selectedIndicator != null)
                selectedIndicator.gameObject.SetActive(false);
        }

        if (itemGameObjectMap.TryGetValue(item, out var obj))
        {
            Transform selectedIndicator = obj.transform.Find("Selected");
            if (selectedIndicator != null)
                selectedIndicator.gameObject.SetActive(true);
        }

        OnItemSelected?.Invoke(this, item);
    }

    private void HandleItemSelected(object sender, Item item)
    {
        if (targetObject != null)
            targetObject.SetActive(true);
    }

    public void LoadSelectedItemScene()
    {
        if (selectedItem == null)
        {
            Debug.LogWarning("Nie wybrano żadnego itema!");
            return;
        }

        if (string.IsNullOrEmpty(selectedItem.sceneName))
        {
            Debug.LogWarning($"Item '{selectedItem.itemName}' nie ma przypisanej nazwy sceny!");
            return;
        }

        Debug.Log($"Ładowanie sceny: {selectedItem.sceneName}");
        SceneManager.LoadScene(selectedItem.sceneName);
    }
}
