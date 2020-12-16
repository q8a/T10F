using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;
    public Animator animator;

    // gravity
    private float gravity = 9.87f;
    private Vector3 camRotation;
    private AudioSource source;
    private Transform cam;
    public AudioClip walkSound;
    public AudioClip jumpSound;
    private Vector3 moveDirection;
    private float walkTime;
    public float jumpSpeed = 1.5f;

    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;

    [SerializeField]
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        source = GetComponent<AudioSource>();
        cam = Camera.main.transform;
    }

    void Update()
    {
        Move();
        Rotate();
        if (Input.GetKey(KeyCode.F))
        {
            speed = 7f;
        }
        else
            speed = 2f;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(horizontalMove, 0, verticalMove);
            moveDirection = transform.TransformDirection(moveDirection);

            if (moveDirection.magnitude > 0.3f && walkTime > 0.4f)
            {
                walkTime = 0;
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(walkSound, 0.3f);
            }
            walkTime += Time.deltaTime;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetTrigger("jumpTrigger");
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(jumpSound, 0.3f);
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        if (verticalMove != 0 && characterController.isGrounded)
        {
            //Debug.Log(verticalMove);
            if (verticalMove > 0)
            {
                animator.SetInteger("condition", 1);
            }
            else if (verticalMove < 0)
            {
                animator.SetInteger("condition", 3);
            }
        }
        else if (verticalMove == 0 || horizontalMove == 0)
        {
            animator.SetInteger("condition", 0);
        }
        // this code is for running right and left animations
        if (horizontalMove != 0 && characterController.isGrounded)
        {
            //Debug.Log(verticalMove);
            if (horizontalMove > 0)
            {
                animator.SetInteger("condition", 5);
            }
            else if (horizontalMove < 0)
            {
                animator.SetInteger("condition", 4);
            }
        }
    }
}
