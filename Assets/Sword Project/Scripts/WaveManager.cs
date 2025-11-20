using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawn[] enemySpawns;

    [SerializeField] private BaseCharacter[] enemies;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private bool waveActive;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float baseSpawnDelay;
    [SerializeField] private float delayDecrease;
    [SerializeField] private int spawnPerWave;

    [SerializeField] private TMP_Text pointsCounter;
    private float playerPoints;

    [SerializeField] private GameObject enemyPrefab;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawns = FindObjectsOfType<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaveTimer()
    {
        while (waveActive == true)
        {
            for (int i = 0; i < spawnPerWave; i++)
            {
                Debug.Log("Wave Spawn");
                SpawnRandomEnemy();
                yield return new WaitForSeconds(0.5f);
            }
            

            spawnDelay -= delayDecrease;

            spawnDelay = Mathf.Max(1, spawnDelay);

            yield return new WaitForSeconds(spawnDelay);
        }      
    }


    public void StartWave()
    {
        Debug.Log("Wave Started");
        waveActive = true;

        spawnDelay = baseSpawnDelay;
        playerPoints = 0;
        UpdatePoints(0);

        waveActive = true;

        StartCoroutine(WaveTimer());
    }
    public void EndWave()
    {
   

        waveActive = false;

 

        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }
    }

    public void UpdatePoints(float amount)
    {
        playerPoints = amount;
        pointsCounter.text = playerPoints.ToString();
    }




    public void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemySpawns.Length);

        EnemySpawn selectedSpawn = enemySpawns[randomIndex];

        randomIndex = Random.Range(0, enemies.Length);

        BaseCharacter selectedEnemy = enemies[randomIndex];



        GameObject spawned = Instantiate(enemyPrefab, selectedSpawn.gameObject.transform.position, selectedSpawn.gameObject.transform.rotation);
        spawnedEnemies.Add(spawned);
    }
}
