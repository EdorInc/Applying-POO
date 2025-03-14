using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cameraTransform; 
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    private float speed = 12f;
    private float gravity = -9.8f;
    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Obtener la dirección basada en la orientación de la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Evitar que el jugador se incline hacia arriba o abajo (solo en plano XZ)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Mover en la dirección en la que mira la cámara
        Vector3 move = (forward * z + right * x).normalized;

        characterController.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
