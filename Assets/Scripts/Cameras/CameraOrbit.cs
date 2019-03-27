using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Camera attachedCamera;
    [Header("orbit")]
    public float xspeed = 120f, yspeed = 120f;
    public float yMinLimit = -20f, yMaxLimit = 80f;
    [Header("Collision")]
    public bool cameraCollision = true; //Is Camera Collision enabled?
    public bool ignoreTriggers = true; // Will the SphereCast ignore triggers?
    public float castRadius = .3f; // Radius of sphere to cast
    public float castDistance = 1000f; // Distance the cast travels
    public LayerMask hitLayers; // Layers that casting will hit

    private float originalDistance; // Record starting distance of camera
    private float distance; // Current distance of camera
    private float x, y; // X and Y Mouse Roatation

    // Use this for initialization
    void Start()
    {
        //Set original distance 
        originalDistance = Vector3.Distance(transform.position, attachedCamera.transform.position);
        //set X and Y degrees to current camera rotation
        x = transform.eulerAngles.y;
        y = transform.eulerAngles.x;


    }

    // Update is called once per frame
    void Update()
    {
        //Is Right Mouse Button pressed?
        if (Input.GetMouseButton(1))
        {
            //Disable Cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Orbit with Input
            x += Input.GetAxis("Mouse X") * xspeed * Time.deltaTime;
            y += Input.GetAxis("Mouse Y") * yspeed * Time.deltaTime;

            //Restricting with Y Limits
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            //Rotate the transform using Euler angles ('pronounced Ew-ler')
            transform.rotation = Quaternion.Euler(y, x, 0);
        }
        else
        {
            // Enabled Cursor 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void FixedUpdate()
    {
        // Set distance to original distance
        distance = originalDistance;

        // Change distance to what we hit
        // Is camera collision enabled?
        if (cameraCollision)
        {
            // Create a ray starting from orbit position and going in the direction of the camera
            Ray camRay = new Ray(transform.position, -transform.forward);
            RaycastHit hit; // Stores the hit information after cast
            // Shoot a sphere behind the camera
            if (Physics.SphereCast(camRay, // Ray in the directon of camera
                                   castRadius, // How thicc the sphere is
                                   out hit, // The hit information collected
                                   castDistance, // How far the cast goes
                                   hitLayers, // What layers we're allowed to hit
                                   ignoreTriggers ? // Ignore triggers?
                                    QueryTriggerInteraction.Ignore // Ignore it!
                                   : // Else
                                    QueryTriggerInteraction.Collide)) // Don't ignore
            {
                // Set distance  to distance of hit
                distance = hit.distance;
            }
        }

        // Apply distance to cameras
        attachedCamera.transform.position = transform.position - transform.forward * distance;
    }
}