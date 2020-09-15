using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Enemy;
    public float timeToSpawn = 5f;

    private void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        for (;;)
        {
            Instantiate(Enemy, transform.position, transform.parent.rotation);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
}
