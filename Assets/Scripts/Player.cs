using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;

    public bool is_grappling;
    public bool finish = false;
    public bool dead;
    
    float move_x;
    float move_z;
    readonly float speed = 10;

    float rotate_horizontal;
    float rotate_vertical;
    readonly float rotate_limit = 90;
    bool is_grounded;
    bool allowed_jump;

    Grappling grappling;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        grappling = GetComponent<Grappling>();
    }

    void Update()
    {
        //Movement and camera

        if(!is_grounded || is_grappling)
        {
            move_x = Input.GetAxisRaw("Horizontal") * 0.3f;
            move_z = Input.GetAxisRaw("Vertical") * 0.3f;
        }
        else
        {
            move_x = Input.GetAxisRaw("Horizontal");
            move_z = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        transform.Translate(move_x * speed * Time.deltaTime, 0, move_z * speed * Time.deltaTime);

        rotate_horizontal = Input.GetAxis("Mouse X");

        transform.Rotate(0, rotate_horizontal, 0);
        
        rotate_vertical -= Input.GetAxis("Mouse Y");
        rotate_vertical = Mathf.Clamp(rotate_vertical, -rotate_limit, rotate_limit);
        Camera.main.transform.localRotation = Quaternion.Euler(rotate_vertical, 0, 0);

        if(is_grounded || is_grappling)
        {
            allowed_jump = true;
        }
        else
        {
            allowed_jump = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && allowed_jump)
        {
            rb.AddForce(0, 300, 0);
            grappling.Stop_grapple();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            is_grounded = true;
        }
        else if(other.CompareTag("Finish"))
        {
            finish = true;
        }
        else if(other.CompareTag("Jump"))
        {
            rb.AddForce(0, 1500, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spikes"))
        {
            dead = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        is_grounded = false;
    }
}
