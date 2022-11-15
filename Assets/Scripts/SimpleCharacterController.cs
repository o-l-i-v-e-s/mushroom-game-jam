using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }

    private void FixedUpdate()
    {
        ProcessActions();
    }

    private void ProcessActions()
    {
        Vector3 MoveDirection = new Vector3(HorizontalInput, 0.0f, VerticalInput);
        Debug.Log(MoveDirection);
        transform.position += MoveDirection * moveSpeed * Time.deltaTime;
    }
}
