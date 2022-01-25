using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    /* Note
     * green - jump pad
     * red - dead
     * yellow - scrubber
     * blue - rocker
    */
    [SerializeField] private LayerMask rocker;
    [SerializeField] private LayerMask scrubber;

    [SerializeField] private Transform gunTip, camera, player;
    
    private bool isScrubber;
    
    private readonly float range = 20;

    private Rigidbody rb;
    
    private LineRenderer lr;
    
    private SpringJoint joint;
    
    private Vector3 grapplePoint;

    private Player playerScr;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        playerScr = GetComponentInParent<Player>();
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
            StopGrapple();
        }


        if(isScrubber)
        {
            rb.velocity = (grapplePoint - transform.position).normalized * 10;

            if (Vector3.Distance(player.position, grapplePoint) <= 0.5f)
            {
                StopGrapple();
            }
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void Start_grapple()
    {

        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, range, rocker))
        {

            CreativeJoint(hit);
            float distanceFormPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFormPoint * 0.8f;
            joint.minDistance = distanceFormPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

        }
        else if(Physics.Raycast(camera.position, camera.forward, out hit, range, scrubber))
        {
            CreativeJoint(hit);
            isScrubber = true;
            
        }
    }
    public void CreativeJoint(RaycastHit hit)
    {
        playerScr.isGrappling = true;
        grapplePoint = hit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;
        lr.positionCount = 2;
    }

    public void StopGrapple()
    {
        lr.positionCount = 0;
        isScrubber = false;
        playerScr.isGrappling = false;
        Destroy(joint);
    }

    void DrawRope()
    {
        if(!joint)
        {
            return;
        }

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }
}
