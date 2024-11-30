using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;

    private float dirChangeTimer = 0;
    public float dirChangeTime = 1.2f;
    private float noMovTimer = 0f;
    public float noMovTime = 2f;

    //Variáveis que definem os comportamentos que as entidades terão.
    public bool startsStill;
    public bool entityTurns;
    public bool entityWaves;
    public bool enemyEntity;
    public bool followsPlayer;
    public bool isFish;
    public Transform posPlayer;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            posPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        //Está definindo se a entidade é um jogador ou outra entidade
        if (!enemyEntity)
        { PlayerMovement(); }
        else
        {
            //Esse if define se a entidade vai começar parada
            if (!startsStill)
            {
                StraightMovement();
                WavingMovement();
                TurningMovement();
                FollowsPlayer();
            }
            else
            {
                noMovTimer += Time.deltaTime;
                if (noMovTimer > noMovTime)
                {
                    startsStill = false;
                }
            }
        }
    }

    //Método que pega o input do jogador e transforma em movimento
    void PlayerMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        if (transform.position.y >= 0.75f && vertical > 0)
        {
            transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, 0));
        }
        else
        {
            transform.Translate(Time.deltaTime * speed * new Vector3(horizontal, vertical));
        }
    }

    //Adiciona movimento reto para inimigos ou entidades.
    void StraightMovement()
    {
        if (!followsPlayer)
        {
            transform.Translate(Time.deltaTime * speed * Vector3.left);
        }
    }

    //Adiciona movimento de curva para um lado a inimigos ou entidades.
    void TurningMovement()
    {
        if (entityTurns && !entityWaves && !followsPlayer)
        {
            transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
    }

    //Adiciona movimento ondular a inimigos ou entidades.
    void WavingMovement()
    {
        if (entityWaves && !followsPlayer)
        {
            dirChangeTimer += Time.deltaTime;
            if (dirChangeTimer > dirChangeTime)
            {
                dirChangeTimer = -dirChangeTime;
                rotationSpeed *= -1;
            }
            transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        }
    }
    void FollowsPlayer()
    {
        if (posPlayer != null && followsPlayer && (posPlayer.position.x < transform.position.x || isFish))
        {
            Vector3 rotation = transform.position - posPlayer.position;
            transform.position = Vector3.MoveTowards(transform.position, posPlayer.position, speed*Time.deltaTime);
            float fRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,fRotation);
        }
        else if (followsPlayer)
        {
            posPlayer = null;
            transform.Translate(Time.deltaTime * speed * Vector3.left);
        }
    }

}
