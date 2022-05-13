using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isGrappling;
    public bool isDead;
    public bool isFinished;
    
    private bool isGrounded;
    private bool allowedJump;
    
    private float moveX;
    private float moveZ;
    private float rotateHorizontal;
    private float rotateVertical;
    private readonly float speed = 10;
    private readonly float rotateLimit = 90;

    private Rigidbody rb;
    private Grappling grappling;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        grappling = GetComponent<Grappling>();
    }

    void Update()
    {
        //Movement and camera
        if(!isGrounded || isGrappling)
        {
            moveX = Input.GetAxisRaw("Horizontal") * 0.3f;
            moveZ = Input.GetAxisRaw("Vertical") * 0.3f;
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");
        }

        if(!isGrappling)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }

            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }

        rotateHorizontal = Input.GetAxis("Mouse X");
        rotateVertical -= Input.GetAxis("Mouse Y");
        rotateVertical = Mathf.Clamp(rotateVertical, -rotateLimit, rotateLimit);
        
        if(isGrounded || isGrappling)
        {
            allowedJump = true;
        }
        else
        {
            allowedJump = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && allowedJump)
        {
            rb.AddForce(0, 300, 0);
            grappling.StopGrapple();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(moveX * speed * Time.deltaTime, 0, moveZ * speed * Time.deltaTime);

        transform.Rotate(0, rotateHorizontal, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotateVertical, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if(other.CompareTag("Finish"))
        {
            isFinished = true;
        }
        else if(other.CompareTag("Jump"))
        {
            rb.AddForce(0, 1500, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        {
            isDead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
