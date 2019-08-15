using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
    bool stackable; // Can only have one of such item in a stack
    int num; // Current Number of item
    int maxnum; // Maximum number of item in a stack
    int ItemID; // Id of the item. (0 - 255)
    string name; // Name of the item
    string description; // Quick description of the item. 

    public InventoryItem(int n, int id, string name_) {
        stackable = false;
        num = n;
        maxnum = 99;
        ItemID = id;
        name = name_;
        description = "No Description for this Item is Available";
    }
}
