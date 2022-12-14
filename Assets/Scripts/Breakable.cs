using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject Collectable;
    [SerializeField] GameObject Powerup;
    [SerializeField] float SpawnRateCollectable = 0.5f;
    [SerializeField] float SpawnRateInventoryManager = 0.5f;

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
        // don't allow spawning of collectables for now
        bool AllowSpawningOfCollectable = false;
        float RandomNumber = Random.Range(0f, 1f);
        if ((RandomNumber <= (1 * SpawnRateCollectable)) && AllowSpawningOfCollectable)
        {
            return Collectable;
        } else if (RandomNumber <= ((1 * SpawnRateCollectable)+ (1 * SpawnRateInventoryManager)))
        {
            return Powerup;
        } else
        {
            return null;
        }
    }
}
