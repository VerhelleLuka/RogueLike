using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float m_SpawnRate = 1f;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool m_CanSpawn = true;

    private float m_xBounds = 19.5f;
    private float m_yBounds = 9.5f;

    static public EnemySpawner instance;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        m_xBounds = (GameManager.instance.playgroundTransform.localScale.x / 2f) - 0.5f;
        m_yBounds = (GameManager.instance.playgroundTransform.localScale.y / 2f) - 0.5f;
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        WaitForSeconds wait = new WaitForSeconds(m_SpawnRate);

        while (true)
        {

            yield return wait;
            Vector3 spawnPos = new Vector3(Random.Range(-m_xBounds, m_xBounds), Random.Range(-m_yBounds, m_yBounds), -1);
            Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity).transform.parent = transform;

            spawnPos = new Vector3(spawnPos.x + Random.Range(-2, 2), spawnPos.y + Random.Range(-2, 2), -1);
            Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity).transform.parent = transform;
            spawnPos = new Vector3(spawnPos.x + Random.Range(-2, 2), spawnPos.y + Random.Range(-2, 2), -1);
            Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity).transform.parent = transform;
        }
    }


}
