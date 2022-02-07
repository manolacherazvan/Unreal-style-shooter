using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Mouse Look
    [SerializeField] Transform playerBody;
    [SerializeField] Transform camera;
    float mouseSensitivity = 100f;
    float yRotation = 0f;
    //Movement
    float speed= 10f;
    [SerializeField] CharacterController characterContoller;
    //Gravity
    Vector3 velocity;
    float gravity = -9.8f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    //Jump
    float jumpHeight=4f;

    public GameObject[] spawnPoints;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterContoller.Move(velocity * Time.deltaTime);

        //Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -60f, 60f);
        camera.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //Movement

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveVector = transform.right * moveX + transform.forward * moveZ;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f);
        characterContoller.Move(moveVector * speed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(this.gameObject.transform.position.y < -15f)
        {
            Respawn();
        }
    }
    public static void Respawn()
    {
        int index = Random.Range(0, 1);
        GameObject[] spawnP = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnP[index].transform.position;
    }
}
