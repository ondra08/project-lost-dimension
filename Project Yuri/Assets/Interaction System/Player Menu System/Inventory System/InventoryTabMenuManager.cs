using UnityEngine;
using UnityEngine.UI;

public class InventoryTabMenuManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private void Start()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager reference not set in InventoryTabMenuManager");
            return;
        }

        Debug.Log("InventoryTabMenuManager Start method executed, setting up listeners.");

        Transform allButton = transform.Find("AllButton");
        if (allButton != null)
        {
            allButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.All));
            Debug.Log("AllButton listener assigned.");
        }

        Transform itemButton = transform.Find("ItemButton");
        if (itemButton != null)
        {
            itemButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.Item));
            Debug.Log("ItemButton listener assigned.");
        }

        Transform weaponButton = transform.Find("WeaponButton");
        if (weaponButton != null)
        {
            weaponButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.Weapon));
            Debug.Log("WeaponButton listener assigned.");
        }

        Transform equipmentButton = transform.Find("EquipmentButton");
        if (equipmentButton != null)
        {
            equipmentButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.Equipment));
            Debug.Log("EquipmentButton listener assigned.");
        }

        Transform accButton = transform.Find("AccButton");
        if (accButton != null)
        {
            accButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.Acc));
            Debug.Log("AccButton listener assigned.");
        }

        Transform keyItemButton = transform.Find("KeyItemButton");
        if (keyItemButton != null)
        {
            keyItemButton.GetComponent<Button>().onClick.AddListener(() => inventoryManager.FilterItemsByCategory(ItemSO.ItemCategory.KeyItem));
            Debug.Log("KeyItemButton listener assigned.");
        }
    }
}