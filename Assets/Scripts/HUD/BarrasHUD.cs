using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarrasHUD : MonoBehaviour
{
    public static BarrasHUD instance;

    public Slider sliderHP;
    public Slider sliderPU;

    public int valorPU;

    private void Awake()
    {
        instance = this;
    }

    public void AtualizarBarraVida(int health)
    {
        sliderHP.value = health/3f;
    }
    public void AdicionarValorPowerUp(int valor)
    {
        valorPU += valor;
        sliderPU.value = valorPU / 150f;
        if (valorPU >= 150)
        {
            sliderPU.value = 1;
            return;
        }
    }
    public void ZerarPowerUp()
    {
        valorPU = 0;
        sliderPU.value = 0;
    }
}
