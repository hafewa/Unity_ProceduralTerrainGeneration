using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region FIELDS
        [Header("Movement Speed")]
            public float slowSpeed = 25f;
            public float fastSpeed = 50f;
            private float moveSpeed;
        [Header("Mouse Movement Speed")]
            public float mouseSensitivity = 5f;
        [Header("Scroll Wheel Movemet Speed")]
            public float scrollSpeed = 10f;

        private Vector3 velocity;
        private float rotationX;
        private float rotationY;
    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the midle of the game window (it can be unlocked by pressing escape)
        Cursor.visible = false; // hides the cursor (it can be shown again by pressing escape)
    }

    void Update () {
        #region Input For Movement
            moveSpeed = Input.GetKeyDown(KeyCode.LeftShift) ? fastSpeed : slowSpeed; // one line if statement that makes the speed fast if the shift button is pressed
            velocity.x = Input.GetAxisRaw("Horizontal"); // W, S, Up Arrow, Down Arrow, and Joystick Up and Down on controller
            velocity.z = Input.GetAxisRaw("Vertical");   // A, D, Left Arrow, Right Arrow, and Joystick Left and Right on controller
            velocity.y = Input.GetAxis("Mouse ScrollWheel") * 10; // Mouse scroll wheel
        #endregion

        #region Mouse Input
            rotationX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime; // mouse Left and Right
            rotationY += Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime; // mouse Up and Down
            rotationY = Mathf.Clamp(rotationY, -90, 90); // limits the up and down mouse movement from straight up to straight down
        #endregion
    }

    void FixedUpdate()
    {
        #region Player Position
            transform.Translate(velocity * moveSpeed * Time.deltaTime); // moves the camera
            transform.localEulerAngles = new Vector3(-rotationY, rotationX ,0f); // rotates the camera
        #endregion
    }
}
