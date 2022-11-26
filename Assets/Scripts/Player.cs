using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ShroomCharacter shroomCharacter;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float SecondsToPlaceShroom = 1f;
    [SerializeField] GameObject UiManagerGameObject;
    [SerializeField] string PlayerType;
    UiManager uiManager;
    private bool CanPlaceShroom = true;

    void Start()
    {
        Debug.Log("PlayerInput");
        shroomCharacter = gameObject.GetComponent<ShroomCharacter>();
        if(shroomCharacter == null)
        {
            Debug.LogError("character is null on Player");
        }
        if (UiManagerGameObject == null)
        {
            Debug.LogError("UiManagerGameObject is null on Player script");
        }
        uiManager = UiManagerGameObject.GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("uiManager script is null on Player script");
        }
        if(PlayerType == null)
        {
            Debug.Log("Player type is null");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShroomPlacement();
    }

    private void HandleMovement()
    {
        float horizontalMovement = 0f;
        float verticalMovement = 0f;
        if (PlayerType == "P1")
        {
            horizontalMovement = Input.GetAxis("P1_Horizontal");
            verticalMovement = Input.GetAxis("P1_Vertical");
        } else if (PlayerType == "P2")
        {
            horizontalMovement = Input.GetAxis("P2_Horizontal");
            verticalMovement = Input.GetAxis("P2_Vertical");
        }
        if(horizontalMovement != 0 || verticalMovement != 0)
        {
            Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
            Vector3 calculatedDirection = moveDirection * moveSpeed * Time.deltaTime;
            if (calculatedDirection.magnitude > maxSpeed)
            {
                Debug.Log("Max speed reached");
                calculatedDirection = calculatedDirection.normalized * maxSpeed;
            }
            shroomCharacter.HandleMovement(calculatedDirection);
        }
    }

    private void HandleShroomPlacement()
    {
        bool InputPlaceBomb = false;
        if(PlayerType == "P1")
        {
            InputPlaceBomb = Input.GetKeyDown(KeyCode.Space);
        } else if (PlayerType == "P2")
        {
            InputPlaceBomb = Input.GetKeyDown(KeyCode.RightControl);
        }
        if (InputPlaceBomb && CanPlaceShroom)
        {
            shroomCharacter.HandleShroomPlacement();
            CanPlaceShroom = false;
            uiManager.SetCanPlaceShroom(CanPlaceShroom, PlayerType);
            StartCoroutine(UpdateCanPlaceShroom(true));
        }
    }

    IEnumerator UpdateCanPlaceShroom(bool Boolean)
    {
        yield return new WaitForSeconds(SecondsToPlaceShroom);
        CanPlaceShroom = Boolean;
        uiManager.SetCanPlaceShroom(CanPlaceShroom, PlayerType);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            HandlePlayerDeath();
        }
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player " + PlayerType + " died!");
        uiManager.ShowEndingMenu(PlayerType);
        Destroy(gameObject);
    }
}
