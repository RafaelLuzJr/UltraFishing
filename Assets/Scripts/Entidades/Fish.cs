using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : MonoBehaviour
{
    public bool isRoped = false;
    private Movement movement;
    [SerializeField] int score;
    AudioManager audioManager;
    
    private void Start()
    {
        movement = GetComponent<Movement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void getRoped()
    {
        isRoped = true;
        transform.GetChild(0).gameObject.SetActive(true);
        movement.followsPlayer = true;
        movement.speed *= 0.8f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && isRoped)
        {
            BarrasHUD.instance.AdicionarValorPowerUp(score);
            ScoreManager.instance.AddScore(score);
            audioManager.PlaySndEffects(audioManager.pickupFish);
            Destroy(gameObject);
        }
    }
}
