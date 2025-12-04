using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawn> enemySpawns;

    [SerializeField] private List<EnemySpawn> section1;
    [SerializeField] private List<EnemySpawn> section2;
    [SerializeField] private List<EnemySpawn> section3;

    [SerializeField] private WaveWall waveWall1;
    [SerializeField] private WaveWall waveWall2;
    [SerializeField] private WaveWall waveWall3;

    [SerializeField] private float Area2Delay;
    [SerializeField] private float Area3Delay;

    [SerializeField] private BaseCharacter[] enemies;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private bool waveActive;

    [SerializeField] private float spawnDelay;
    [SerializeField] private float baseSpawnDelay;
    [SerializeField] private float delayDecrease;
    [SerializeField] private int spawnPerWave;
    [SerializeField] private int basePerWave;

    [SerializeField] private TMP_Text pointsCounter;
    private float playerPoints;

    [SerializeField] private GameObject enemyPrefab;

    private Keyboard keyboard;


    // Start is called before the first frame update
    void Start()
    {

        keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard.rKey.wasPressedThisFrame)
        {
            StartWave();
        }

        if (keyboard.qKey.wasPressedThisFrame)
        {
            EndWave();
        }
    }



    private IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(3);
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

            spawnPerWave++;

            yield return new WaitForSeconds(spawnDelay);
        }      
    }


    public void StartWave()
    {
        Debug.Log("Wave Started");

        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        FindObjectOfType<AudioManager>().PlayBackgroundMusic("MainTheme");

        FindObjectOfType<Player>().ResetHealth();

        waveActive = true;

        spawnDelay = baseSpawnDelay;
        spawnPerWave = basePerWave;
        playerPoints = 0;
        UpdatePoints(0);

        waveActive = true;

        StartCoroutine(WaveTimer());

        StartCoroutine("SectionTimer");
    }

    private IEnumerator SectionTimer()
    {
        waveWall1.SetWall(true);

        OpenSection(section1);

        yield return new WaitForSeconds(Area2Delay);

        waveWall2.SetWall(true);

        OpenSection(section2);

        yield return new WaitForSeconds(Area3Delay);

        waveWall3.SetWall(true);

        OpenSection(section3);
    }
    public void EndWave()
    {
        Debug.Log("Ending Wave");

        waveActive = false;


        FindObjectOfType<AudioManager>().StopBackgroundMusic();
        FindObjectOfType<AudioManager>().PlayBackgroundMusic("Ambiance");

        foreach (GameObject enemy in spawnedEnemies)
        {
            Destroy(enemy);
        }

        spawnedEnemies.Clear();

        StopCoroutine("SectionTimer");

        waveWall1.SetWall(false);
        waveWall2.SetWall(false);
        waveWall3.SetWall(false);

        enemySpawns.Clear();

    }

    public void UpdatePoints(float amount)
    {
        playerPoints += amount;
        pointsCounter.text = playerPoints.ToString();
    }




    public void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, enemySpawns.Count);

        EnemySpawn selectedSpawn = enemySpawns[randomIndex];

        //randomIndex = Random.Range(0, enemies.Length);

        //BaseCharacter selectedEnemy = enemies[randomIndex];



        GameObject spawned = Instantiate(enemyPrefab, selectedSpawn.gameObject.transform.position, selectedSpawn.gameObject.transform.rotation);
        spawnedEnemies.Add(spawned);
    }

    public void OpenSection(List<EnemySpawn> spawns)
    {
        foreach (EnemySpawn spawn in spawns)
        {
            enemySpawns.Add(spawn);
        }
    }
}
