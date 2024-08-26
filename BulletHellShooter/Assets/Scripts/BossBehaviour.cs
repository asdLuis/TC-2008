using UnityEngine;
using System.Collections;

public class BossBehaviour : MonoBehaviour
{
    public BossSequenceManager sequenceManager;
    public BossAttackSequence attackSequence;
    public BossAttackSequenceSides sideAttackSequence;
    public BossAttackSequenceExplode explodeAttackSequence;
    public float delayBetweenAttacks = 3f;
    public float initialDelay = 2f;
    public GameObject youWinScreen;

    private bool bossSequenceCompleted = false;
    private int attackIndex = 0;

    void Start()
    {
        sequenceManager.OnSequenceComplete += OnBossSequenceCompleted;
    }

    private void OnBossSequenceCompleted()
    {
        bossSequenceCompleted = true;
        StartCoroutine(StartAttackSequences());
    }

    private IEnumerator StartAttackSequences()
    {
        yield return new WaitForSeconds(initialDelay);

        for (int i = 0; i < 10; i++)
        {
            switch (attackIndex)
            {
                case 0:
                    attackSequence.LaunchAsteroidSequence();
                    break;
                case 1:
                    sideAttackSequence.LaunchAsteroidFromBorders();
                    break;
                case 2:
                    explodeAttackSequence.LaunchExplodeSequence();
                    break;
            }

            attackIndex = (attackIndex + 1) % 3;

            yield return new WaitForSeconds(delayBetweenAttacks);
        }

        ShowYouWinScreen();
    }

    private void ShowYouWinScreen()
    {
        if (youWinScreen != null)
        {
            youWinScreen.SetActive(true);
        }
    }

    void OnDestroy()
    {
        if (sequenceManager != null)
        {
            sequenceManager.OnSequenceComplete -= OnBossSequenceCompleted;
        }
    }
}
