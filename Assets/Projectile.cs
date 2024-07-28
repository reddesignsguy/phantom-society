using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _timeToLive = 10f;

    private Coroutine _returnToPoolTimerCoroutine;

    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(returnToPoolAfterTime());
    }

    private IEnumerator returnToPoolAfterTime()
    {
        // Destroy this projectile after a certain time interval
        float elapsedTime = 0.0f;
        while (elapsedTime < _timeToLive)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        print("10 seconds is up!");
        ObjectPoolManager.returnObjectToPool(gameObject);
    }
}
