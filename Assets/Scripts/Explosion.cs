using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Breakable"))
        {
            Breakable breakable = other.gameObject.GetComponent<Breakable>();
            if(breakable != null)
            {
                breakable.HandleBreak();
            } else
            {
                Debug.LogError("other should contain Breakable script, but it is null");
            }
        }
    }
}
