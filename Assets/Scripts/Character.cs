using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float maxSpeed = 0.05f;
    [SerializeField] float moveMagnitudeThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
        Vector3 calculatedDirection = moveDirection * moveSpeed * Time.deltaTime;
        //Debug.Log("Magnitude: " + calculatedDirection.magnitude + ". Max speed " + maxSpeed);
        float moveMagnitude = calculatedDirection.magnitude;
        Debug.Log("moveMagnitude: " + moveMagnitude);
        Debug.Log(calculatedDirection.magnitude > maxSpeed);
        if (calculatedDirection.magnitude > maxSpeed)
        {
            Debug.Log("Max speed reached");
            calculatedDirection = calculatedDirection.normalized * maxSpeed;
        }
        Debug.Log("Moving");
        if (moveMagnitude <= moveMagnitudeThreshold)
        {
            characterController.SimpleMove(Vector3.zero);
        } else
        {
            characterController.Move(calculatedDirection);
        }
    }
}
