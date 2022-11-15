using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private SimpleCharacterController charController;

    private void Awake()
    {
        charController = GetComponent<SimpleCharacterController>();
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        charController.HorizontalInput = horizontal;
        charController.VerticalInput = vertical;
    }
}
