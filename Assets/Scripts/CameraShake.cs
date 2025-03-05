using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f; // Duración total de la sacudida
    public float shakeMagnitude = 0.2f; // Intensidad de la sacudida

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition; // Guarda la posición original de la cámara
    }

    public void StartShake()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Mathf.PerlinNoise(Time.time * 10f, 0f) * 2 - 1;
            float y = Mathf.PerlinNoise(0f, Time.time * 10f) * 2 - 1;

            transform.localPosition = originalPosition + new Vector3(x, y, 0) * shakeMagnitude;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition; // Restaurar la posición original
    }
}
