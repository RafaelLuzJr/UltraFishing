using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    public enum Entities { SWORDFISH, BJELLYFISH, PJELLYFISH, SHARK, ANGLERFISH, REDFISH }

    [SerializeField] float[] tBeforeSpawn;
    public Entities[] entityTypes;
    public float[] speed;
    public float[] rotationSpeed;
    public float[] dirChangeTime;

    WaveManager waveManager;


    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("Gerenciador").GetComponent<WaveManager>();
        waveManager.enemyCount += entityTypes.Length;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i< entityTypes.Length; i++)
        {
            yield return new WaitForSeconds(tBeforeSpawn[i]);
            int tipoInimigo = (int)entityTypes[i];
            GameObject _enemy = Instantiate(enemies[tipoInimigo], transform.position, Quaternion.identity);
            _enemy.GetComponent<Movement>().speed = speed[i];
            _enemy.GetComponent<Movement>().rotationSpeed = rotationSpeed[i];
            _enemy.GetComponent<Movement>().dirChangeTime = dirChangeTime[i];
            _enemy.GetComponent<Health>().waveManager = waveManager;
        }
    }

}
