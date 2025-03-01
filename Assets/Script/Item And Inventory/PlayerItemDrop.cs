﻿using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's drop")]
    [SerializeField] private float chanceToLooseItems;
    [SerializeField] private float changeToLooseMaterials;
    [SerializeField] private float changeToLooseInventory;

    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;

        List<InventoryItem> currentEquipment = inventory.GetEquipmentList();
        List<InventoryItem> itemsToUnequip = new List<InventoryItem>();

        List<InventoryItem> materialToUnequip = new List<InventoryItem>();
        List<InventoryItem> currentStash = inventory.GetStashList();

        List<InventoryItem> inventoryToUnequip = new List<InventoryItem>();
        List<InventoryItem> currentInventory = inventory.GetInventory();

        // Rớt trang bị
        foreach (InventoryItem item in currentEquipment)
        {
            if (Random.Range(0, 100) < chanceToLooseItems)
            {
                DropItemMultipleTimes(item);
                itemsToUnequip.Add(item);
            }
        }
        foreach (var item in itemsToUnequip)
        {
            inventory.UnequipItem(item.data as ItemData_Equipment);
        }

        // Rớt nguyên liệu
        foreach (InventoryItem item in currentStash)
        {
            if (Random.Range(0, 100) < changeToLooseMaterials)
            {
                DropItemMultipleTimes(item);
                materialToUnequip.Add(item);
            }
        }
        foreach (var item in materialToUnequip)
        {
            inventory.RemoveItem(item.data);
        }

        // Rớt đồ trong inventory
        foreach (InventoryItem item in currentInventory)
        {
            if (Random.Range(0, 100) < changeToLooseInventory)
            {
                DropItemMultipleTimes(item);
                inventoryToUnequip.Add(item);
            }
        }
        foreach (var item in inventoryToUnequip)
        {
            inventory.RemoveItem(item.data);
        }

    }

    // Rớt đúng số lượng item
    private void DropItemMultipleTimes(InventoryItem item)
    {
        for (int i = 0; i < item.stackSize; i++)
        {
            DropItem(item.data);
        }
    }
}
