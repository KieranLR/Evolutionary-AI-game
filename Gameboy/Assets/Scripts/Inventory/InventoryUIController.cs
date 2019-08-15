using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {
    public GameObject InventoryMenu;
    bool isActive = true;
    private void Start() {
        isActive = true;
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("tab")) {
            if (isActive) {
                InventoryMenu.SetActive(false);
                isActive = false;
            }
            else {
                InventoryMenu.SetActive(true);
                isActive = true;
            }
        }
	}
}
