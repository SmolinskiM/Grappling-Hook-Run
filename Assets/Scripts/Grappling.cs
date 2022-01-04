using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    /* Note
     * zielony - platformy do skakania
     * czerwony - dead
     * zółty - przyciąganie
     * niebieski - huśtanie 
    */
    bool is_scrubber;
    float range = 20;

    public LayerMask rocker;
    public LayerMask scrubber;
    public Transform gun_tip, camera, player;
    Vector3 grapple_point;
    SpringJoint joint;
    Player is_grappling;
    Rigidbody rb;
    LineRenderer lr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        is_grappling = GetComponentInParent<Player>();
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {      
        if (Input.GetMouseButtonDown(0))
        {
            Start_grapple();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            is_grappling.is_grappling = false;
            Stop_grapple();
        }


        if(is_scrubber)
        {
            rb.velocity = (grapple_point - transform.position).normalized * 10;

            if (Vector3.Distance(player.position, grapple_point) <= 0.5f)
            {
                Stop_grapple();
            }
        }
    }

    private void LateUpdate()
    {
        Draw_rope();
    }

    private void Start_grapple()
    {

        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, range, rocker))
        {
            is_grappling.is_grappling = true;
            grapple_point = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapple_point;

            float distance_form_point = Vector3.Distance(player.position, grapple_point);

            joint.maxDistance = distance_form_point * 0.8f;
            joint.minDistance = distance_form_point * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
        else if(Physics.Raycast(camera.position, camera.forward, out hit, range, scrubber))
        {
            is_grappling.is_grappling = true;
            grapple_point = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapple_point;

            lr.positionCount = 2;
            is_scrubber = true;
            
        }
    }

    public void Stop_grapple()
    {
        lr.positionCount = 0;
        is_scrubber = false;
        Destroy(joint);
    }

    void Draw_rope()
    {
        if(!joint)
        {
            return;
        }

        lr.SetPosition(0, gun_tip.position);
        lr.SetPosition(1, grapple_point);
    }
}
