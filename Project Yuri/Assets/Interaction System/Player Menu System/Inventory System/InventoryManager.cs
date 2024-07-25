using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    public GameObject inventoryPanel;
    public GameObject inventoryTabMenu; // Reference to the Inventory Tab Menu GameObject
    public ItemSlot[] itemSlots;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        inventoryPanel.SetActive(false);
        inventoryTabMenu.SetActive(false); // Ensure it's hidden initially
        Time.timeScale = 1;

        // Clear all slots at the start
        foreach (var slot in itemSlots)
        {
            slot.ClearSlot();
        }
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryTabMenu.SetActive(true); // Show the inventory tab menu when the inventory is shown
        Time.timeScale = 0;
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryTabMenu.SetActive(false); // Hide the inventory tab menu when the inventory is hidden
        Time.timeScale = 1;
    }

    public void UseItem(string itemName)
    {
        ItemSO itemSO = ItemManager.Instance.GetItemSOByName(itemName);
        if (itemSO != null && itemSO.UseItem())
        {
            UpdateItemQuantity(itemName, -1);
        }
    }

    public int AddItem(string itemName, int itemQuantity, Sprite itemSprite, string itemDescription)
    {
        foreach (var slot in itemSlots)
        {
            if (!slot.isFull && (slot.itemName == itemName || slot.itemQuantity == 0))
            {
                int leftOverItems = slot.AddItem(itemName, itemQuantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

                return leftOverItems;
            }
        }

        // If no suitable slot is found, return the remaining item quantity
        return itemQuantity;
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.selectedShader.SetActive(false);
            slot.thisItemSelected = false;
        }
    }

    private void UpdateItemQuantity(string itemName, int quantityChange)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemName == itemName)
            {
                slot.itemQuantity += quantityChange;
                if (slot.itemQuantity <= 0)
                {
                    slot.ClearSlot();
                }
                else
                {
                    slot.UpdateQuantityText();
                }
                break;
            }
        }
    }

    public void FilterItemsByCategory(ItemSO.ItemCategory category)
    {
        // Clear all slots before filtering
        foreach (var slot in itemSlots)
        {
            slot.ClearSlot();
        }

        // Filter items based on category and add them to the slots
        foreach (var itemSO in ItemManager.Instance.GetItemsByCategory(category))
        {
            AddItem(itemSO.itemName, 1, itemSO.itemSprite, itemSO.itemDescription);
        }
    }

    public void BuyItem(ItemSO itemSO, int quantity)
    {
        // Deduct the currency and add the item to inventory
        int remainingQuantity = AddItem(itemSO.itemName, quantity, itemSO.itemSprite, itemSO.itemDescription);
        while (remainingQuantity > 0)
        {
            remainingQuantity = AddItem(itemSO.itemName, quantity, itemSO.itemSprite, itemSO.itemDescription);
        }
    }
}