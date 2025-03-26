using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [Header("External references")]
    public TextMeshProUGUI waveText;
    public LayerMask whatIsPlayer;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject medKitPrefab;

    [Header("General properties")]
    [SerializeField] private int currentRound = 0;
    [SerializeField] private int enemiesToSpawn = 12;
    [SerializeField] private Vector3 randomPosition;
    [SerializeField] private int distanceFromPlayer;
    public int enemiesAlive;

    void Start()
    {
        waveText.enabled = false;
        Invoke("SpawnFirstWave", 10f);
    }
    private void SpawnFirstWave()
    {
        StartCoroutine(SpawnItems());
        StartCoroutine(SpawnEnemyWave());
    }
    public void CheckForNextWave()
    {
        if (enemiesAlive == 0)
        {
            StartCoroutine(SpawnItems());
            StartCoroutine(SpawnEnemyWave());
        }
    }
    private void GetRandomNavMeshPosition()
    {
        float randomX = Random.Range(-5, 5);
        randomX *= 5;
        float randomZ;
        if (randomX > -5 && randomX < 5)
        {
            randomZ = Random.Range(-5, 5);
            randomZ *= 5;
        }
        else
        {
            randomZ = Random.Range(-5, 5);
        }
        randomPosition = new Vector3(randomX, transform.position.y, randomZ);
        NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 10f, NavMesh.AllAreas);
        randomPosition = hit.position;
    }
    private IEnumerator SpawnEnemyWave()
    {
        waveText.enabled = true;
        currentRound++;
        waveText.text = "Wave " + currentRound;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GetRandomNavMeshPosition();
            if (!Physics.CheckSphere(randomPosition, distanceFromPlayer, whatIsPlayer)){
                Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                enemiesAlive++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                i--;
            }
        }
        enemiesToSpawn += 2;
        waveText.enabled = false;
    }
    private IEnumerator SpawnItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GetRandomNavMeshPosition();
            Instantiate(ammoPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 2; i++)
        {
            GetRandomNavMeshPosition();
            Instantiate(medKitPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}