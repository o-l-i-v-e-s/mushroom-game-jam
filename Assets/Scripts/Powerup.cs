using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    GameObject GameManager;
    PlayerDataManager playerDataManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        if (GameManager == null)
        {
            Debug.LogError("GameManager game object is null in Powerup");
        }
        else
        {
            playerDataManager = GameManager.GetComponent<PlayerDataManager>();
            if (playerDataManager == null)
            {
                Debug.LogError("playerDataManager script is null in Powerup");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDataManager.HandleGetPowerup();
            Destroy(gameObject);
        }
    }
}
