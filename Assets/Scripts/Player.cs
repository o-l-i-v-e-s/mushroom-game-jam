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
    }

    private void Update()
    {
        HandleMovement();
        HandleShroomPlacement();
    }

    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 calculatedDirection = moveDirection * moveSpeed * Time.deltaTime;
        if (calculatedDirection.magnitude > maxSpeed)
        {
            Debug.Log("Max speed reached");
            calculatedDirection = calculatedDirection.normalized * maxSpeed;
        }
        shroomCharacter.HandleMovement(calculatedDirection);
    }

    private void HandleShroomPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanPlaceShroom)
        {
            shroomCharacter.HandleShroomPlacement();
            CanPlaceShroom = false;
            uiManager.SetCanPlaceShroom(CanPlaceShroom);
            StartCoroutine(UpdateCanPlaceShroom(true));
        }
    }

    IEnumerator UpdateCanPlaceShroom(bool Boolean)
    {
        yield return new WaitForSeconds(SecondsToPlaceShroom);
        CanPlaceShroom = Boolean;
        uiManager.SetCanPlaceShroom(CanPlaceShroom);
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
        Debug.Log("Played died!");
        uiManager.ShowLoseMenu();
        Destroy(gameObject);
    }
}
