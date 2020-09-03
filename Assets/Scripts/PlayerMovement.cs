using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector3 movement;
    Animator animator;
    Rigidbody rb;
    AudioSource audioSource;
    public float turnSpeed = 20f;
    
    Quaternion movementRotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    { 
        Move();
    }

    private void Move()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementY = Input.GetAxis("Vertical");
        movement.Set(movementX, 0f, movementY);
        movement.Normalize();
        bool hasMovementX = !Mathf.Approximately(movementX, 0f);
        bool hasMovementY = !Mathf.Approximately(movementY, 0f);
        bool isWalking = hasMovementX || hasMovementY;
        animator.SetBool("IsWalking", isWalking);
        if (isWalking) 
        {
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
        } else {
            audioSource.Stop();
        }
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        movementRotation = Quaternion.LookRotation(desiredForward);


    }

    void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(movementRotation);
    }
}
