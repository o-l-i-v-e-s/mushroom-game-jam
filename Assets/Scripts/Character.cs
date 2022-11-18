using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] GameObject UnstableShroom;
    [SerializeField] float SecondsToPlaceShroom = 1f;

    private bool CanPlaceShroom = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleShroomPlacement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            HandlePlayerDeath();
        }
    }

    void HandleShroomPlacement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CanPlaceShroom)
        {
            Vector3 ShroomPosition = new Vector3(Mathf.RoundToInt(transform.position.x),transform.position.y,Mathf.RoundToInt(transform.position.z));
            Instantiate(UnstableShroom, ShroomPosition, Quaternion.identity);
            CanPlaceShroom = false;
            StartCoroutine(UpdateCanPlaceShroom(true));
        }
    }

    IEnumerator UpdateCanPlaceShroom(bool Boolean)
    {
        yield return new WaitForSeconds(SecondsToPlaceShroom);
        CanPlaceShroom = Boolean;
    }

    void HandleMovement()
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
        characterController.Move(calculatedDirection);
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Played died!");
        Destroy(gameObject);
    }

}
