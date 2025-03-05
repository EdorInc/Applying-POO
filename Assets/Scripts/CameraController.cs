using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 5f; // Sensibilidad del ratón
    public Transform playerBody;   // Referencia al jugador

    private float xRotation = 0f; // Control de la rotación vertical

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor al centro
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar el jugador en el eje Y (izquierda/derecha)
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotar la cámara en el eje X (arriba/abajo)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar rotación para evitar voltear

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
