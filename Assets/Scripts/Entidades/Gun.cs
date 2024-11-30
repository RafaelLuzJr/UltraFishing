using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Projectile swordfish;
    public Projectile ropeHarpoon;
    public RopeController ropeController;
    private SpriteRenderer sprite;
    private Gun gun;
    [SerializeField]private AudioManager audioManager;
    public int municao = 10;
    public int municaoMax = 10;
    public int damage = 1;

    //Variável para mudar entre os tipos de disparo
    public int tiposTiro = 0;

    //Variáveis cronômetro
    private float autoTimer = 0f;
    private float autoTime;
    public float autoSetTime;
    public float overchargeSetTime;
    public float tEndSupercharge;

    private void Start()
    {
        autoTime = autoSetTime;
        sprite = GetComponent<SpriteRenderer>();
        gun = GetComponent<Gun>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        AutoFire();
        Jellyfish();
        Swordfish();
        RopeHarpoon();
    }

    //Região de comportamento dos tipos de disparo
    #region Tipos de Tiro

    //Método para tiro primário
    void AutoFire()
    {
        autoTimer -= Time.deltaTime;
        if (ropeHarpoon == null && Input.GetButton("Fire1") /*&& municao > 0*/ && autoTimer <= 0 /*& !reloadTimerAtivo*/)
        {
            autoTimer = autoTime;
            DispararProjetil(projectilePrefab, damage);
        }
    }

    void Swordfish()
    {
        if (ropeHarpoon == null && Input.GetButtonDown("Fire2") && BarrasHUD.instance.valorPU >= 150)
        {
            BarrasHUD.instance.ZerarPowerUp();
            DispararProjetil(swordfish, 12, true);

        }
    }

    void Jellyfish()
    {
        if (ropeHarpoon == null && Input.GetButtonDown("Fire3") && BarrasHUD.instance.valorPU >= 150)
        {
            BarrasHUD.instance.ZerarPowerUp();
            autoTime = overchargeSetTime;
            sprite.color = Color.yellow;
            audioManager.PlaySndEffects(audioManager.powerUpFilled);
            StartCoroutine(EndSupercharge());
        }
    }

    void RopeHarpoon()
    {
        if (ropeHarpoon == null && Input.GetKeyDown(KeyCode.E) && autoTimer <= 0)
        {
            DispararProjetil(projectilePrefab, 1, true, 3.75f,ropeController, gun);
        }
    }
    #endregion

    #region Sobrecargas de Disparo
    void DispararProjetil(Projectile projectile, int damage)
    {
        Projectile _projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        _projectile.damage = damage;
        audioManager.PlaySndEffects(audioManager.normalShoot);
    }

    void DispararProjetil(Projectile projectile, int damage, bool unstopabble)
    {
        Projectile _projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        _projectile.damage = damage;
        _projectile.unstopabble = true;
        audioManager.PlaySndEffects(audioManager.swordShoot);
    }

    void DispararProjetil(Projectile projectile, int damage, bool roped, float speed, RopeController ropeController, Gun gunReference)
    {
        Projectile _projectile = Instantiate(projectile, transform.position, Quaternion.identity);
        _projectile.damage = damage;
        _projectile.speed = speed;
        _projectile.roped = roped;
        _projectile.ropeController = ropeController;
        _projectile.gun = gunReference;
        ropeController.points[1] = _projectile.transform;
        ropeHarpoon = _projectile;
    }
    #endregion
    IEnumerator EndSupercharge()
    {
        yield return new WaitForSeconds(tEndSupercharge);
        sprite.color = Color.white;
        autoTime = autoSetTime;
    }
}
