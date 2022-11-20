using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShroomCharacter"))
        {
            ShroomCharacter shroomCharacter = other.GetComponent<ShroomCharacter>();
            if (shroomCharacter != null)
            {
                shroomCharacter.HandleGetCollectable();
            }
            Destroy(gameObject);
        }
    }
}
