// Written by Sage Mahmud

using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera cam;
    [SerializeField] Transform focus;

    
    [Header("Statistics")]
    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    float distanceFromFocus => transform.position.z - focus.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromFocus > 0? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromFocus) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startZ);
    }
}
