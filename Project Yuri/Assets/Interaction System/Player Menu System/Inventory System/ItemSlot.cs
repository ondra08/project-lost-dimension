using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] private int maxNumberOfItems;

    [SerializeField] private TMP_Text itemQuantityText;
    [SerializeField] private Image itemImage;

    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    public int AddItem(string itemName, int itemQuantity, Sprite itemSprite, string itemDescription)
    {
        if (isFull && this.itemName != itemName)
            return itemQuantity;

        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;

        this.itemQuantity += itemQuantity;
        if (this.itemQuantity >= maxNumberOfItems)
        {
            int extraItems = this.itemQuantity - maxNumberOfItems;
            this.itemQuantity = maxNumberOfItems;
            isFull = true;
            itemQuantityText.text = maxNumberOfItems.ToString();
            itemQuantityText.enabled = true;
            return extraItems;
        }

        itemQuantityText.text = this.itemQuantity.ToString();
        itemQuantityText.enabled = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected)
            inventoryManager.UseItem(itemName);

        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if (itemDescriptionImage.sprite == null)
            itemDescriptionImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        // Implement any right-click functionality if needed
    }

    public void ClearSlot()
    {
        itemName = "";
        itemQuantity = 0;
        itemSprite = emptySprite;
        itemDescription = "";
        isFull = false;
        itemImage.sprite = emptySprite;
        itemQuantityText.enabled = false;
    }

    public void UpdateQuantityText()
    {
        itemQuantityText.text = itemQuantity.ToString();
        itemQuantityText.enabled = true;
    }
}