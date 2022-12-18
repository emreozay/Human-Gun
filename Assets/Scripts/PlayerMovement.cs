using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;

    [SerializeField]
    private float xSpeed = 0.4f;
    [SerializeField]
    private float moveForwardSpeed = 4.5f;
    private float lastMousePoint;
    private bool isMouseDown = false;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            lastMousePoint = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    }

    private void MovePlayer()
    {
        if (isMouseDown)
        {
            float difference = Input.mousePosition.x - lastMousePoint;

            float xPos = transform.position.x + difference * Time.deltaTime * xSpeed;
            xPos = Mathf.Clamp(xPos, -1.6f, 1.6f);

            playerRigidbody.MovePosition(new Vector3(xPos, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));

            lastMousePoint = Input.mousePosition.x;
        }
        else
        {
            playerRigidbody.MovePosition(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveForwardSpeed * Time.fixedDeltaTime));
        }
    }
}