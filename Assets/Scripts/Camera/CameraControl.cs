// Written by Sage Mahmud and ELizabeth Castreje and Miguel Aleman


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private GameObject[] m_Targets;
    private float m_ScreenEdgeBuffer = 3f;           // Space between the top/bottom most target and the screen edge.
    private float m_MinSize = 5f;                    // The smallest orthographic size the camera can be.
    private Camera m_Camera;
    private Vector3 m_DesiredPosition;
                                                     // we are going to try to do damping in Cinemachine, but to be determined

    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>(); 
    }
    
    void Start()
    {
        // adding every object with this player tag into the m_Targets list/array/group thingy

        m_Targets = GameObject.FindGameObjectsWithTag("PlayerTag");

    }


    private void FixedUpdate()
    {
        // Move the camera towards a desired position.
        Move();

        // Change the size of the camera to keep both players in frame.
        Zoom();
    }

    private void Move()
    {
        // Find the average position of the targets.
        m_DesiredPosition = FindAveragePosition();
        transform.position = m_DesiredPosition;

    }

    private Vector3 FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();

        for (int i = 0; i < m_Targets.Length; i++)
        {
            // Add to the average and increment the number of targets in the average.
            averagePos += m_Targets[i].transform.position;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (m_Targets.Length > 0)
        {
            averagePos /= m_Targets.Length;
        }

        // Keep the same z value.
        averagePos.z = transform.position.z;

        // The desired position is the average position;
        return averagePos;
    }

    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        m_Camera.orthographicSize = FindRequiredSize();

    }

    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < m_Targets.Length; i++)
        {
        
            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].transform.position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinSize);

        return size;
    }

    void Update()
    {
        
    }
}