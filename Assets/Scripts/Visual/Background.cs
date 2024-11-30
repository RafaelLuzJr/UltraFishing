using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.left * 0.01f * Time.deltaTime);
    }
}
