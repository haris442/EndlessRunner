using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField] private Transform targetToFollow;
    [SerializeField] private float cameraVerticalHeight = 5.5f;
    private Vector3 offset;
    private Vector3 newPositionZ;
    public float xPositionCameraMove = 0;
    float xPositionCameraMoveValue;
    [SerializeField] private float forSmoothingCameraFollow=10f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        offset = transform.position - targetToFollow.position;
        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        xPositionCameraMoveValue = Mathf.Lerp(transform.position.x, xPositionCameraMove, Time.deltaTime * forSmoothingCameraFollow);
           newPositionZ = new Vector3(xPositionCameraMoveValue, targetToFollow.position.y+ cameraVerticalHeight, offset.z+targetToFollow.position.z);
        
        transform.position = newPositionZ;
    }

 
}
