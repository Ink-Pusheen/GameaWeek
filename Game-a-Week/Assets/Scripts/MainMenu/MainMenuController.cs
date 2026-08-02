using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] Vector3 camPos; //Position the camera is expected to be

    [SerializeField] GameObject camRef; //Reference to the actual camera

    [SerializeField] float camMoveSpeed; //How fast the camera moves between the games

    [SerializeField] float lowerBounds; //How far the camera can scroll left
    [SerializeField] float upperBounds; //How far the camera can scroll right

    [Header("Controls")]

    private PlayerInput camInput; //Player input for the camera

    private InputAction moveLeft; //Input to scroll left
    private InputAction moveRight; //Input to scroll right
    private InputAction select; //Input to select the highlighted game


    [Header("Games")]

    [SerializeField] string[] gameNames; //Array of game names to reference for scene loading

    private void Awake()
    {
        camPos = camRef.transform.position; //Get the initial position

        camInput = GetComponent<PlayerInput>(); // Grab the player input

        //Get the player input actions
        moveLeft = camInput.actions.FindAction("Left");
        moveRight = camInput.actions.FindAction("Right");
        select = camInput.actions.FindAction("Select");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft.WasPressedThisFrame() && camPos.x > lowerBounds)
        {
            //Debug.Log("Move Left");

            camPos += new Vector3(-15, 0, 0); //Set the new position
        }

        if (moveRight.WasPressedThisFrame() && camPos.x < upperBounds)
        {
            //Debug.Log("Move Right");

            camPos += new Vector3(15, 0, 0); //Set the new position
        }

        if (select.WasPressedThisFrame())
        {
            //Debug.Log("Select Game");
            SelectTitle();
        }

        camRef.transform.position = Vector3.MoveTowards(camRef.transform.position, camPos, camMoveSpeed * Time.deltaTime);

        if (Vector3.Distance(camRef.transform.position, camPos) > 25) camMoveSpeed = 55;

        else camMoveSpeed = 30;
    }



    //Function using a switch case for selecting a game or quitting respectfully
    void SelectTitle()
    {
        switch (camPos.x)
        {
            case (-15f): //Case of hovering the quit game
                Debug.Log("Quit");
                Application.Quit();
                break;

            case (0): //Case of Week 1
                Debug.Log("Week 1");
                SceneManager.LoadScene(gameNames[0]); //Load the specified game
                break;

            case (15): //Case of Week 2
                Debug.Log("Week 2");
                SceneManager.LoadScene(gameNames[1]); //Load the specified game
                break;

            case (30): //Case of Week 3
                Debug.Log("Week 3");
                SceneManager.LoadScene(gameNames[2]); //Load the specified game
                break;

            case (45): //Case of Week 4
                Debug.Log("Week 4");
                SceneManager.LoadScene(gameNames[3]); //Load the specified game
                break;

            case (60): //Case of Week 5
                Debug.Log("Week 5");
                SceneManager.LoadScene(gameNames[4]); //Load the specified game
                break;

            case (75): //Case of Week 6
                Debug.Log("Week 6");
                SceneManager.LoadScene(gameNames[5]); //Load the specified game
                break;

            case (90): //Case of Week 7
                Debug.Log("Week 7");
                SceneManager.LoadScene(gameNames[6]); //Load the specified game
                break;

            case (105): //Case of Week 8
                Debug.Log("Week 8");
                SceneManager.LoadScene(gameNames[7]); //Load the specified game
                break;

            case (120): //Case of Week 9
                Debug.Log("Week 9");
                SceneManager.LoadScene(gameNames[8]); //Load the specified game
                break;

            case (135): //Case of Week 10
                Debug.Log("Week 10");
                SceneManager.LoadScene(gameNames[9]); //Load the specified game
                break;

            case (150): //Case of Week 11
                Debug.Log("Week 11");
                SceneManager.LoadScene(gameNames[10]); //Load the specified game
                break;

            case (165): //Case of Week 12
                Debug.Log("Week 12");
                SceneManager.LoadScene(gameNames[11]); //Load the specified game
                break;

            case (180): //Case of Week 13
                Debug.Log("Week 13");
                //SceneManager.LoadScene(gameNames[12]); //Load the specified game //Uncomment this when the game is created
                break;
        }
    }
}
