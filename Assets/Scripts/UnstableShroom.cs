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
        for (int i = 0; i < sets; i++)
        {
            InstantiateExplostionSet(i);
        }
    }

    private void InstantiateExplostionSet(int setIndex)
    {
        float ExplosionDelay = (setIndex) * SecondsPerExplosionSet;
        StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x + (setIndex + 1)), transform.position.y, Mathf.RoundToInt(transform.position.z)), ExplosionDelay));
        StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z + (setIndex + 1))), ExplosionDelay));
        StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x - (setIndex + 1)), transform.position.y, Mathf.RoundToInt(transform.position.z)), ExplosionDelay));
        StartCoroutine(InstantiateExplosion(new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z - (setIndex + 1))), ExplosionDelay));
    }
    IEnumerator InstantiateExplosion(Vector3 ExplosionPosition, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        GameObject CreatedExplosion = Instantiate(Explosion, ExplosionPosition, Quaternion.identity);
        CreatedExplosion.transform.parent = gameObject.transform;
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(SecondsToDestroySelf);
        Destroy(gameObject);
    }
}
