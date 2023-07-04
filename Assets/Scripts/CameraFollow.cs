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
    public float xPositionCameraMove;
    float xPositionCameraMoveValue;
    float yPositionCameraMoveValue;
    float cameraInitialYPosition;
    [SerializeField] private float forSmoothingCameraFollow = 10f;
    CharacterController playerCharacterController;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        playerCharacterController = targetToFollow.GetComponent<CharacterController>();
        PlayerPrefs.SetFloat("CameraXInitialPosition", transform.position.x);
        // PlayerPrefs.SetFloat("CameraYInitialPosition", transform.position.y);
        cameraInitialYPosition = transform.position.y;
        offset = transform.position - targetToFollow.position;
        xPositionCameraMove = PlayerPrefs.GetFloat("PlayerInitialXPosition");
    }


    // Update is called once per frame
    void LateUpdate()
    {


        if (playerCharacterController.isGrounded)
        {
            xPositionCameraMoveValue = Mathf.Lerp(transform.position.x, xPositionCameraMove, Time.deltaTime * forSmoothingCameraFollow);
            newPositionZ = new Vector3(xPositionCameraMoveValue, targetToFollow.position.y + cameraVerticalHeight, offset.z + targetToFollow.position.z);

        }
        else
        {
            xPositionCameraMoveValue = Mathf.Lerp(transform.position.x, xPositionCameraMove, Time.deltaTime * forSmoothingCameraFollow);

            yPositionCameraMoveValue = Mathf.Lerp(transform.position.y, cameraInitialYPosition, Time.deltaTime * 10f);

            newPositionZ = new Vector3(xPositionCameraMoveValue, yPositionCameraMoveValue, offset.z + targetToFollow.position.z);

        }
        transform.position = newPositionZ;
    }


}
