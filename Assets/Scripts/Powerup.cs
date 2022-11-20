using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter Powerup");
        Debug.Log(other.tag);
        if (other.CompareTag("ShroomCharacter"))
        {
            Debug.Log("is shroom char");
            ShroomCharacter shroomCharacter = other.GetComponent<ShroomCharacter>();
            if(shroomCharacter != null)
            {
                shroomCharacter.HandleGetPowerup();
            }
            Destroy(gameObject);
        }
    }
}
