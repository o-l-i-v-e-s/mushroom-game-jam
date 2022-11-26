using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCharacter : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] GameObject UnstableShroom;
    [field: SerializeField] public int ExplosionLength { get; private set; } = 1;
    [field: SerializeField] public int ExplosionLengthLimit { get; private set; } = 4;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void HandleShroomPlacement()
    {
            Vector3 ShroomPosition = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y,Mathf.RoundToInt(transform.position.z));
            GameObject NewShroom = Instantiate(UnstableShroom, ShroomPosition, Quaternion.identity);
            if(NewShroom != null)
            {
                UnstableShroom us = NewShroom.GetComponent<UnstableShroom>();
                us.ExplosionLength = ExplosionLength;
            }
    }

    public void HandleMovement(Vector3 CalculatedDirection)
    {
        characterController.Move(CalculatedDirection);
    }

    public void HandleGetCollectable()
    {
        Debug.Log("Get collectable");
    }
    public void HandleGetPowerup()
    {
        Debug.Log("Getting powerup");
        // until we have specific powerup types, increment ExplosionLength
        // limit it to 4 for now
        if (ExplosionLength < ExplosionLengthLimit)
        {
            ExplosionLength++;
        }
    }
}
