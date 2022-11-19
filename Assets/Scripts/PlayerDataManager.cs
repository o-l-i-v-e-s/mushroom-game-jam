using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [field: SerializeField] public int ExplosionLength { get; private set; } = 1;
    [field: SerializeField] public int ExplosionLengthLimit { get; private set; } = 4;
    public void HandleGetPowerup()
    {
        // until we have specific powerup types, increment ExplosionLength
        // limit it to 4 for now
        if(ExplosionLength < ExplosionLengthLimit)
        {
            ExplosionLength++;
        }
    }
}
