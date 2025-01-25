using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject thisPlayer;
    public GameObject otherPlayer;
    public Vector3 cameraOffset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find centerpoint between the two players
        Vector3 trackingPoint = (thisPlayer.transform.position + otherPlayer.transform.position) / 2;

        Vector3 viewVector = (trackingPoint - transform.position);
        viewVector.y = 0;
        viewVector.Normalize();
        Vector3 rotatedCameraOffset = Quaternion.LookRotation(viewVector) * cameraOffset;
        

        Vector3 finalCameraOffset = thisPlayer.transform.position + rotatedCameraOffset;

        transform.position = finalCameraOffset;
        
        transform.LookAt(trackingPoint);

        transform.position = thisPlayer.transform.position + transform.rotation * cameraOffset;

        transform.LookAt(trackingPoint);
    }
}
