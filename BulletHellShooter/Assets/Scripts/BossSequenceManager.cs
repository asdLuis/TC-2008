using UnityEngine;
using System.Collections;
using System;

public class BossSequenceManager : MonoBehaviour
{
    public GameObject bossObject;
    public float movementDistance = 3f;
    public float movementDuration = 2f;

    public event Action OnSequenceComplete;

    public void TriggerBossSequence()
    {
        StartCoroutine(MoveBossForward());
    }

    private IEnumerator MoveBossForward()
    {
        Vector3 startPosition = bossObject.transform.position;
        Vector3 endPosition = startPosition + Vector3.back * movementDistance;

        float elapsedTime = 0f;
        while (elapsedTime < movementDuration)
        {
            bossObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / movementDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bossObject.transform.position = endPosition;

        OnSequenceComplete?.Invoke();
    }
}
