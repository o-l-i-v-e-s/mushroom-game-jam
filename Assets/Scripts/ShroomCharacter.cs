using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCharacter : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] GameObject UnstableShroom;
    [field: SerializeField] public int ExplosionLength { get; private set; } = 1;
    [field: SerializeField] public int ExplosionLengthLimit { get; private set; } = 4;
    float VelocityMagnitudeThreshold = 0.4f;

    Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        animator = GetComponentInChildren<Animator>();
        if(animator == null)
        {
            Debug.LogError("animator is null on ShroomCharacter");
        }
    }

    private void Update()
    {
        HandleCharacterAnimation();
        HandleCharacterRotation();
    }

    private void HandleCharacterAnimation()
    {
        Debug.Log(characterController.velocity.magnitude);
        if (characterController.velocity.magnitude > VelocityMagnitudeThreshold)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            Debug.Log("IsWalking: FALSE");
            animator.SetBool("IsWalking", false);
        }
    }

    private void HandleCharacterRotation()
    {
        if (characterController.velocity.magnitude > VelocityMagnitudeThreshold)
        {
            float xDirection = characterController.velocity.x;
            float zDirection = characterController.velocity.z;
            if(Mathf.Abs(xDirection) > Mathf.Abs(zDirection))
            {
                // face horizontal direction
                if(xDirection < 0)
                {
                    // left
                    transform.rotation = Quaternion.LookRotation(new Vector3(-1,0,0));
                } else if (xDirection > 0)
                {
                    // right
                    transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
                }
            } else if (Mathf.Abs(xDirection) < Mathf.Abs(zDirection))
            {
                // face vertical direction
                if (zDirection < 0)
                {
                    // down
                    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
                }
                else if (zDirection > 0)
                {
                    // up
                    Debug.Log("UP");
                    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
                }
            }
        }
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
