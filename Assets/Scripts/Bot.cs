using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    ShroomCharacter shroomCharacter;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float SecondsToPlaceShroom = 1f;
    private bool CanPlaceShroom = true;
    private NavMeshAgent agent;

    void Start()
    {
        Debug.Log("BotInput");
        shroomCharacter = gameObject.GetComponent<ShroomCharacter>();
        if (shroomCharacter == null)
        {
            Debug.LogError("character is null on Bot");
        }
        agent = gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("agent is null on Bot");
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleShroomPlacement();
    }

    private void HandleMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Clicked");
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePosition, out var hitInfo))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void HandleShroomPlacement()
    {
    /* Place an unstable shroom as soon as it's possible, every time*/
    /*  if (CanPlaceShroom)
        {
            shroomCharacter.HandleShroomPlacement();
            CanPlaceShroom = false;
            StartCoroutine(UpdateCanPlaceShroom(true));
        }
    */
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
