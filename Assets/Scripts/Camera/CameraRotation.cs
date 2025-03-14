using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // Objeto alrededor del cual girará la cámara
    private float orbitSpeed = 10f; // Velocidad de la traslación
    private float distance = 100f; // Distancia al centro
    private float height = 400f; // Altura de la cámara

    private float angle = 0f;

    void Update()
    {
        if (target == null) return;

        // Incrementar el ángulo con el tiempo para crear movimiento circular
        angle += orbitSpeed * Time.deltaTime;

        // Calcular la nueva posición en círculo
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

        // Aplicar la nueva posición y mantener la altura deseada
        transform.position = new Vector3(target.position.x + x, target.position.y + height, target.position.z + z);

        // Hacer que la cámara mire siempre al centro (target)
        transform.LookAt(target);
    }
}
