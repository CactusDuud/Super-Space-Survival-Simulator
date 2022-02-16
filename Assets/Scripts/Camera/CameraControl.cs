// Written by Sage Mahmud and ELizabeth Castreje


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    /*[HideInInspector]*/ public Transform[] m_Targets;

    private Camera m_Camera;
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector2 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector2 m_DesiredPosition;

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>(); 
    }


    private void FixedUpdate()
    {
        // Move the camera towards a desired position.
        Move();

        // Change the size of the camera based.
        Zoom();
    }

    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();
    }

    private void FindAveragePosition()
    {
        Vector2 averagePos = new Vector2();

        // if we want to in the future we can set target by other means 

        for (int i = 0; i < m_Targets.Length; i++)
        {
            // Add to the average and increment the number of targets in the average.
            averagePos += m_Targets[i].position;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (m_Targets.Length > 0)
        {
            averagePos /= m_Targets.Length;
        }

        // Keep the same y value.
        averagePos.z = transform.position.z;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
    }






    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


// have camerarig identify if number of targets (players)
// moves to average between the targets
// scale camera to distance between targets