using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForward = 10f;
    [SerializeField] private float laneDistance = 1f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float smoothTransitionValue;

    [SerializeField] private List<float> playerSpeedDistance; // After how much distance player speed will increase
    [SerializeField] private List<float> playerSpeedCoinAmount; // After how much coins player speed will increase
    [SerializeField] private List<float> playerSpeedMultiplier; // After how much speed is applied to the player forward movement

    //[SerializeField] private float laneSwitchSmoothing = 3f;
    private float gravity;
    private Vector3 direction;


    private Transform playerInitialPosition;
    private int currentSpeedDistanceIndex=0; // Speed index for Distance List
    private int currentSpeedCoinAmountIndex = 0; // Speed index for Coin Amount List
    private int currentSpeedMultiplierIndex = 0; // Speed index for Multiplier List
    private CoinCollect coinCollect;
    private float smoothTransition;
  
    private float newPos;
    private enum CurrentLane
    {
        Left,
        Mid,
        Right
    };
    private CurrentLane currentLane;
    private void Awake()
    {
        
    }
    private void Start()
    {
        playerInitialPosition = transform;
        PlayerPrefs.SetFloat("PlayerInitialXPosition", playerInitialPosition.position.x);
        PlayerPrefs.SetFloat("PlayerInitialYPosition", playerInitialPosition.position.y);
        coinCollect = GetComponent<CoinCollect>();
        currentLane = CurrentLane.Mid;
       // smoothTransition = playerInitialPosition.position.x; // Initialize smoothTransition
        newPos = playerInitialPosition.position.x;
    }


    void Update()
    {

        GetUserInput();
    }

    private void FixedUpdate()
    {
       

        PlayerMovement();
        IncreasePlayerSpeedByDistance();
        IncreasePlayerSpeedByCoins();

    }
    private void PlayerMovement()
    {
         direction.z = moveForward;
      
        smoothTransition = Mathf.Lerp(smoothTransition, newPos, Time.fixedDeltaTime * smoothTransitionValue);
        characterController.Move(new Vector3(smoothTransition-transform.position.x,direction.y * Time.fixedDeltaTime,direction.z * Time.fixedDeltaTime) );
        //characterController.Move(new Vector3(playerInitialPosition.position.x, direction.y * Time.fixedDeltaTime, direction.z * Time.fixedDeltaTime));
    }
    private void GetUserInput()
    {
        if (characterController.isGrounded)
        {
            direction.y = -1f;

            // Jump Input
            if (Input.GetKeyDown(KeyCode.UpArrow) )
            {
                direction.y = jumpHeight;
                
            }

          
        }
        else
        {
            gravity = -jumpHeight * 2;
            direction.y += gravity*Time.deltaTime;
        }

        if(!characterController.isGrounded)
        {
            // Downward Input
            if (Input.GetKeyDown(KeyCode.DownArrow) )
            {
                direction.y = -jumpHeight; // Move downwards
            }
        }
     
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane == CurrentLane.Mid)
            {
                // Move to the right lane
              //   characterController.Move(laneToMoveTransform - transform.position);
                newPos = laneDistance+transform.position.x;
                CameraFollow.instance.xPositionCameraMove = laneDistance / 2 + PlayerPrefs.GetFloat("CameraXInitialPosition");
                currentLane = CurrentLane.Right;
            }

            else if (currentLane == CurrentLane.Left)
            {
                // Move to the middle lane
               // characterController.Move(laneToMoveTransform - transform.position);

                newPos = PlayerPrefs.GetFloat("PlayerInitialXPosition");
                CameraFollow.instance.xPositionCameraMove = PlayerPrefs.GetFloat("PlayerInitialXPosition");
                currentLane = CurrentLane.Mid;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane == CurrentLane.Mid)
            {
                // Move to the left lane

                //characterController.Move(laneToMoveTransform - transform.position);
                newPos = -laneDistance+ transform.position.x;
                CameraFollow.instance.xPositionCameraMove = -laneDistance / 2 + PlayerPrefs.GetFloat("CameraXInitialPosition");
                currentLane = CurrentLane.Left;
            }


            else if (currentLane == CurrentLane.Right)
            {
                // Move to the middle lane
               //  characterController.Move(laneToMoveTransform - transform.position);
                newPos = PlayerPrefs.GetFloat("PlayerInitialXPosition");
                CameraFollow.instance.xPositionCameraMove = PlayerPrefs.GetFloat("PlayerInitialXPosition");

                currentLane = CurrentLane.Mid;
            }
        }

        

        /*
    if(transform.position==laneToMoveTransform)
        {
            return;
        }
        Vector3 difference = laneToMoveTransform - transform.position;
        Vector3 moveToLane = difference.normalized * laneSwitchSmoothing * Time.deltaTime;
    if(moveToLane.sqrMagnitude<difference.sqrMagnitude)
        {
            characterController.Move(moveToLane);
        }
    else
        {
            characterController.Move(difference);
        }
    */
    }

    private void IncreasePlayerSpeedByDistance()
    {
        if(currentSpeedDistanceIndex < playerSpeedDistance.Count && playerInitialPosition.position.z>playerSpeedDistance[currentSpeedDistanceIndex] && currentSpeedMultiplierIndex<playerSpeedMultiplier.Count)
        {
            moveForward *= playerSpeedMultiplier[currentSpeedMultiplierIndex];
            currentSpeedDistanceIndex++;
            currentSpeedMultiplierIndex++;
        }
    }

    private void IncreasePlayerSpeedByCoins()
    {
        if (currentSpeedCoinAmountIndex < playerSpeedCoinAmount.Count && coinCollect.GetCollectedCoins() > playerSpeedCoinAmount[currentSpeedCoinAmountIndex] && currentSpeedMultiplierIndex < playerSpeedMultiplier.Count)
        {
            moveForward *= playerSpeedMultiplier[currentSpeedMultiplierIndex]; ;
            currentSpeedCoinAmountIndex++;
            currentSpeedMultiplierIndex++;

        }
    }

   
}
