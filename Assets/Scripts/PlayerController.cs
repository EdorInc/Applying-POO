using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float playerSpeed = 10f;
    private float jumpForce = 8f;
    public LayerMask groundLayer; // Asigna en el Inspector la capa del suelo
    public LayerMask wallLayer; // Asigna en el Inspector la capa de las paredes

    private bool isGrounded;
    private bool canMoveForward = true;
    private bool canMoveBackward = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar paredes antes de moverse
        CheckWalls();

        //MOVIMIENTO JUGADOR SOLO SI NO HAY PAREDES
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;

        if (moveVertical > 0 && canMoveForward) // Adelante
            movement += Vector3.forward;
        if (moveVertical < 0 && canMoveBackward) // Atrás
            movement += Vector3.back;
        if (moveHorizontal < 0 && canMoveLeft) // Izquierda
            movement += Vector3.left;
        if (moveHorizontal > 0 && canMoveRight) // Derecha
            movement += Vector3.right;

        transform.Translate(movement * playerSpeed * Time.deltaTime, Space.Self);

        //SALTO DEL JUGADOR Y QUE SOLO PUEDA SALTAR CUANDO ESTÁ EN EL SUELO
        isGrounded = CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Método para comprobar si el jugador está tocando el suelo
    bool CheckGround()
    {
        float groundDistance = 1.1f;

        // Raycast desde el centro del jugador
        bool centerRay = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
        bool leftRay = Physics.Raycast(transform.position + Vector3.left * 0.5f, Vector3.down, groundDistance, groundLayer);
        bool rightRay = Physics.Raycast(transform.position + Vector3.right * 0.5f, Vector3.down, groundDistance, groundLayer);

        return centerRay || leftRay || rightRay;
    }

    // Método para comprobar si hay paredes alrededor del jugador
    void CheckWalls()
    {
        float wallDistance = 1f; // Ajusta según el tamaño del jugador
        Vector3 position = transform.position;

        // Direcciones cardinales (adelante, atrás, izquierda, derecha)
        Vector3[] cardinalDirections = {
        transform.forward, -transform.forward, -transform.right, transform.right };

        // Estados de movimiento correspondientes
        bool[] movementStates = { canMoveForward, canMoveBackward, canMoveLeft, canMoveRight };

        // Raycast para direcciones cardinales
        for (int i = 0; i < cardinalDirections.Length; i++)
        {
            movementStates[i] = !Physics.Raycast(position, cardinalDirections[i], wallDistance, wallLayer);
            Debug.DrawRay(position, cardinalDirections[i] * wallDistance, movementStates[i] ? Color.green : Color.red);
        }

        // Asignamos los valores actualizados
        canMoveForward = movementStates[0];
        canMoveBackward = movementStates[1];
        canMoveLeft = movementStates[2];
        canMoveRight = movementStates[3];

        // Direcciones diagonales
        Vector3[] diagonalDirections = {
        (transform.forward + -transform.right).normalized,  // Adelante-Izquierda
        (transform.forward + transform.right).normalized,   // Adelante-Derecha
        (-transform.forward + -transform.right).normalized, // Atrás-Izquierda
        (-transform.forward + transform.right).normalized   // Atrás-Derecha
    };

        // Raycast para diagonales
        bool[] diagonalStates = new bool[4];
        for (int i = 0; i < diagonalDirections.Length; i++)
        {
            diagonalStates[i] = !Physics.Raycast(position, diagonalDirections[i], wallDistance, wallLayer);
            Debug.DrawRay(position, diagonalDirections[i] * wallDistance, diagonalStates[i] ? Color.cyan : Color.magenta);
        }

        // Si alguna diagonal está bloqueada, bloqueamos su dirección principal correspondiente
        canMoveForward &= diagonalStates[0] && diagonalStates[1];
        canMoveBackward &= diagonalStates[2] && diagonalStates[3];
        canMoveLeft &= diagonalStates[0] && diagonalStates[2];
        canMoveRight &= diagonalStates[1] && diagonalStates[3];
    }


}
