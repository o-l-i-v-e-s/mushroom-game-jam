using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    ShroomCharacter shroomCharacter;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float SecondsToPlaceShroom = 1f;
    private bool CanPlaceShroom = true;

    void Start()
    {
        Debug.Log("BotInput");
        shroomCharacter = gameObject.GetComponent<ShroomCharacter>();
        if (shroomCharacter == null)
        {
            Debug.LogError("character is null on BotInput");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShroomPlacement();
    }

    private void HandleMovement()
    {
/*        Debug.Log("Handling player movement from PlayerInput.cs");
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 calculatedDirection = moveDirection * moveSpeed * Time.deltaTime;
        if (calculatedDirection.magnitude > maxSpeed)
        {
            Debug.Log("Max speed reached");
            calculatedDirection = calculatedDirection.normalized * maxSpeed;
        }
        shroomCharacter.HandleMovement(calculatedDirection);*/
    }

    private void HandleShroomPlacement()
    {
        if (CanPlaceShroom)
        {
            shroomCharacter.HandleShroomPlacement();
            CanPlaceShroom = false;
            StartCoroutine(UpdateCanPlaceShroom(true));
        }
    }

    IEnumerator UpdateCanPlaceShroom(bool Boolean)
    {
        yield return new WaitForSeconds(SecondsToPlaceShroom);
        CanPlaceShroom = Boolean;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Bot died!");
        Destroy(gameObject);
    }
}
