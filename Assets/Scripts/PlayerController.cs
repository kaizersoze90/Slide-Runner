using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] float runningSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float touchSensitivity;
    [SerializeField] float screenLimitX;

    public bool canMove = true;

    float _currentSpeed;
    float _newPositionX;

    void Start()
    {
        _currentSpeed = runningSpeed;
    }

    void Update()
    {
        if (canMove)
        {
            ProcessTouch();
            ProcessMove();
        }
    }

    void ProcessMove()
    {
        Vector3 newPosition = new Vector3(_newPositionX,
                                          transform.position.y,
                                          transform.position.z
                                          + _currentSpeed * Time.deltaTime);
        transform.position = newPosition;
    }

    void ProcessTouch()
    {
        float touchDeltaX = 0;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchDeltaX = Input.GetTouch(0).deltaPosition.x / touchSensitivity;
        }
        else if (Input.GetMouseButton(0))
        {
            touchDeltaX = Input.GetAxis("Mouse X");
        }

        _newPositionX = transform.position.x + turnSpeed * touchDeltaX * Time.deltaTime;
        _newPositionX = Mathf.Clamp(_newPositionX, -screenLimitX, screenLimitX);
    }
}
