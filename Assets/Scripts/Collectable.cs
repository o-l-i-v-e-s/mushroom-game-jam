using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    GameObject GameManager;
    InventoryManager inventoryManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        if (GameManager == null)
        {
            Debug.LogError("GameManager game object is null in Collectable");
        }
        else
        {
            inventoryManager = GameManager.GetComponent<InventoryManager>();
            if (inventoryManager == null)
            {
                Debug.LogError("inventoryManager script is null in Collectable");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryManager.HandleGetCollectable();
            Destroy(gameObject);
        }
    }
}
