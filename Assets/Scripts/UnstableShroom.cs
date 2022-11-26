using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableShroom : MonoBehaviour
{
    GameObject GameManager;
    [SerializeField] float SecondsUntilExplosion = 1f;
    [SerializeField] float SecondsToDestroySelf = 1f;
    [SerializeField] float SecondsPerExplosionSet = 0.1f;
    [SerializeField] GameObject Explosion;
    public int ExplosionLength = 1;

    private void Start()
    {
        StartCoroutine(ExplodeAndDestroySelf());
    }

    IEnumerator ExplodeAndDestroySelf()
    {
        yield return new WaitForSeconds(SecondsUntilExplosion);
        HandleInstantiateExplosions(ExplosionLength);
        StartCoroutine(DestroySelf());
    }

    private void HandleInstantiateExplosions(int sets)
    {
        float upDistance = GetDistanceInDirection(new Vector3(0,0,1));
        float rightDistance = GetDistanceInDirection(new Vector3(1, 0, 0));
        float downDistance = GetDistanceInDirection(new Vector3(0, 0, -1));
        float leftDistance = GetDistanceInDirection(new Vector3(-1, 0, 0));
        for (int i = 0; i < sets; i++)
        {
            InstantiateExplostionSet(i, upDistance > i, rightDistance > i, downDistance > i, leftDistance > i);
        }
    }

    private void InstantiateExplostionSet(int setIndex, bool explodeUp, bool explodeRight, bool explodeDown, bool explodeLeft)
    {
        float ExplosionDelay = (setIndex) * SecondsPerExplosionSet;
        StartCoroutine(InstantiateInvisibleExplosion(new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z)), ExplosionDelay));
        if(explodeUp)
        {
            StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z + (setIndex + 1))), ExplosionDelay));
        }
        if (explodeRight)
        {
            StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x + (setIndex + 1)), transform.position.y, Mathf.RoundToInt(transform.position.z)), ExplosionDelay));
        }
        if (explodeDown)
        {
            StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z - (setIndex + 1))), ExplosionDelay));
        }
        if (explodeLeft)
        {
            StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x - (setIndex + 1)), transform.position.y, Mathf.RoundToInt(transform.position.z)), ExplosionDelay));

        }
    }

    IEnumerator InstantiateInvisibleExplosion(Vector3 ExplosionPosition, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        GameObject CreatedExplosion = Instantiate(Explosion, ExplosionPosition, Quaternion.identity);
        CreatedExplosion.transform.parent = gameObject.transform;
        MeshRenderer meshRenderer = CreatedExplosion.GetComponent<MeshRenderer>();
        Debug.Log(meshRenderer);
        if(meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    IEnumerator InstantiateExplosion(Vector3 ExplosionPosition, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        GameObject CreatedExplosion = Instantiate(Explosion, ExplosionPosition, Quaternion.identity);
        CreatedExplosion.transform.parent = gameObject.transform;
    }

    private float GetDistanceInDirection(Vector3 Direction)
    {
        float Distance = 0f;
        if(Direction != new Vector3(0,0,0))
        {
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, Direction, out hit, 1000f, ~6))
            {
                Distance = hit.distance;
            }
        }
        return Distance;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SecondsToDestroySelf);
        Destroy(gameObject);
    }
}
