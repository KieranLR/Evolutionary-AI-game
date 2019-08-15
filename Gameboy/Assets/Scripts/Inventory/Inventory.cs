using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    List<InventoryItem> items;
    public GameObject holder; // Whoever owns the inventory, Player, Chest, etc...
}
