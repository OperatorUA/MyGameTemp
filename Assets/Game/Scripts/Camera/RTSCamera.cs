using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
    private float screenControlBordersSize = 0.01f;

    [Range(0.1f, 2f)] public float moveSpeedModifier = 1.3f;
    [Range(0.1f, 10f)] public float zoomSpeedModifier = 1f;

    private float moveSpeed = 10f;
    public float rotateSpeed = 10f;

    public float maxHeight = 40f;
    public float minHeight = 2f;

    [SerializeField] private Vector3 defaultPosition;
    void Awake()
    {
        defaultPosition = transform.position;
    }

    public bool lockCameraMovement = false;
    void LateUpdate()
    {
        CalculateMoveSpeed();
        ScreenBordersControl();
        KeyboardControl();
        Zoom();

        SpecialPosibilites();
        CameraRotation();
        Limits(Vector3.zero);
    }

    private void CalculateMoveSpeed()
    {
        moveSpeed = transform.position.y * moveSpeedModifier;
    }

    private void Zoom()
    {
        // убрать подергивания при привешении лимитов
        Vector3 direction = Vector3.zero;
        float mouseScroolWheelInput = Input.GetAxis("Mouse ScrollWheel");

        direction = Vector3.up * mouseScroolWheelInput;

        if (transform.position.y > maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        else if (transform.position.y < minHeight)
        {
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }

        Move(direction, -moveSpeed * zoomSpeedModifier * 10);
    }

    private void Move(Vector3 direction, float speed)
    {
        direction.Normalize();
        if (direction != Vector3.zero && !lockCameraMovement)
        {
            Vector3 targetPosition = transform.position + direction * speed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        }
    }

    private void KeyboardControl()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        Move(direction, moveSpeed);
    }

    private void ScreenBordersControl()
    {
        Vector3 direction = Vector3.zero;
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x < Screen.width * screenControlBordersSize)
        {
            direction += Vector3.left;
        }
        if (mousePosition.x > Screen.width - (Screen.width * screenControlBordersSize))
        {
            direction += Vector3.right;
        }
        if (mousePosition.y < Screen.height * screenControlBordersSize)
        {
            direction += Vector3.back;
        }
        if (mousePosition.y > Screen.height - (Screen.height * screenControlBordersSize))
        {
            direction += Vector3.forward;
        }

        Move(direction, moveSpeed);
    }

    private void CameraRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        }
    }

    private void SpecialPosibilites()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position = defaultPosition;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            lockCameraMovement = !lockCameraMovement;
        }
    }

    private void Limits(Vector3 worldSize)
    {
        if (worldSize == Vector3.zero) return;

        float mapSizeX = worldSize.x;
        float mapSizeY = worldSize.y;

        if (transform.position.x > mapSizeX)
        {
            transform.position = new Vector3(mapSizeX, transform.position.y, transform.position.z);
        }
        if (transform.position.z > mapSizeY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mapSizeY);
        }
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
