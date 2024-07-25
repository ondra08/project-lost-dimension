using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    [TextArea]
    public string itemDescription;
    public StatToChange statToChange = StatToChange.None;
    public int itemPrice;
    public int amountToChangeStat;
    public AttributeToChange attributeToChange = AttributeToChange.None;
    public int amountToChangeAttribute;
    public ItemCategory category;

    public bool UseItem()
    {
        // You can add other item use functionality here if needed
        return false;
    }

    public enum StatToChange
    {
        None,
        Mana,
    };

    public enum AttributeToChange
    {
        None,
        Strength,
        Defense,
        Intelligence,
        Agility
    };

    public enum ItemCategory
    {
        All,
        Item,
        Weapon,
        Equipment,
        Acc,
        KeyItem
    }
}