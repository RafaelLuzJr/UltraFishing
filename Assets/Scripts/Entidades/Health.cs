using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int valorPontos;
    public GameObject efeitoMorte;
    public MenuPausa telaGameOver;
    public WaveManager waveManager;
    [SerializeField] AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    bool fishActive = false;
    public bool isPlayer = false;
    private bool isInvulnerable = false;
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            health -= damage;
            StartCoroutine(DamagedEffect());
            
            if (isPlayer)
            {
                BarrasHUD.instance.AtualizarBarraVida(health);
                isInvulnerable=true;
                StartCoroutine(EndInvulnerability());
                audioManager.PlaySndEffects(audioManager.getHit);
            }
            if (health < 1)
            {
                Instantiate(efeitoMorte, transform.position, Quaternion.identity);
                BarrasHUD.instance.AdicionarValorPowerUp(valorPontos);
                ScoreManager.instance.AddScore(valorPontos);
                if (!isPlayer)
                {
                    waveManager.ReduceEnemyCount();
                    fishActive = false;
                }
                else
                {
                    telaGameOver.StartCoroutine("GameOverAppears");
                }
                audioManager.PlaySndEffects(audioManager.death);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DamagedEffect()
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sprite.color = Color.white;
        }
    }

    IEnumerator EndInvulnerability()
    {
        yield return new WaitForSeconds(0.625f);
        isInvulnerable = false;
    }



    private void OnBecameVisible()
    {
        fishActive = true;
    }
    private void OnBecameInvisible()
    {
        if (fishActive & waveManager != null)
        {
            waveManager.ReduceEnemyCount();
            Destroy(gameObject);
        }
    }
}
