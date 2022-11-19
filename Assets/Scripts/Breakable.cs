using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject Collectable;
    [SerializeField] GameObject Powerup;
    [SerializeField] float SpawnRateCollectable = 0.5f;
    [SerializeField] float SpawnRateInventoryManager = 0.5f;

    GameObject GameManager;
    PlayerDataManager playerDataManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        if(GameManager == null)
        {
            Debug.LogError("gameManager is null on Breakable script");
        } else
        {
            playerDataManager = GameManager.GetComponent<PlayerDataManager>();
            if (playerDataManager == null)
            {
                Debug.LogError("playerDataManager script is null in Breakable script");
            }
        }
    }

    public void HandleBreak()
    {
        GameObject gameObjectToSpawn = GetGameObjectToSpawn();
        if(gameObjectToSpawn != null)
        {
            Vector3 PowerupPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            Instantiate(gameObjectToSpawn, PowerupPosition, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private GameObject GetGameObjectToSpawn()
    {
        float RandomNumber = Random.Range(0f, 1f);
        if (RandomNumber <= (1 * SpawnRateCollectable))
        {
            return Collectable;
        } else if (RandomNumber <= ((1 * SpawnRateCollectable)+ (1 * SpawnRateInventoryManager)))
        {
            if(playerDataManager.ExplosionLength >= playerDataManager.ExplosionLengthLimit)
            {
                return null;
            }
            return Powerup;
        } else
        {
            return null;
        }
    }
}
