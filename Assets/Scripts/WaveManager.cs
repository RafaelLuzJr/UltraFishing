using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] Waves;
    [SerializeField] private MenuPausa menuPausa;
    public int wavesCount = 0;
    public int enemyCount = 0;
    
    public void ReduceEnemyCount()
    {
        enemyCount--;
        Debug.Log("Imimigos: " + enemyCount);
        if (enemyCount <= 0)
        {
            ProceedNextWave();
        }
    }
    public void ProceedNextWave()
    {
        wavesCount++;
        Waves[wavesCount-1].SetActive(false);
        if (wavesCount < Waves.Length)
        {
            Waves[wavesCount - 1].SetActive(false);
            Waves[wavesCount].SetActive(true);
        }
        else
        {
            menuPausa.StartCoroutine("EndGameAppears");
        }
    }
}
