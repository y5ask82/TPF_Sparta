using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float runSpeed;
    private Vector2 curMovementInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [SerializeField] private Light flashLight;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;
    private PlayerConditions _playerConditions;
    private Animator _animator;

    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";


    public static PlayerController instance;
    private void Awake()
    {

        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _playerConditions = GetComponent<PlayerConditions>();
        _animator = GetComponentInChildren<Animator>();

    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!_playerConditions.isRun)
        {
            Move();
        }
        else Run();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;


        _rigidbody.velocity = dir;
        
    }

    private void Run()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= runSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();

            if (curMovementInput.x > 0.72 || curMovementInput.x < -0.72 || curMovementInput.y < 0.7)
            {
                _animator.SetBool(runParameterName, false);
                _playerConditions.isRun = false;
            }
            _animator.SetBool(walkParameterName, true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            _playerConditions.isRun = false;

            _animator.SetBool(runParameterName, false);
            _animator.SetBool(walkParameterName, false);
        }
    }
    public void OnRunInput(InputAction.CallbackContext context)
    {
        
        if (_playerConditions.isRun == false
            && curMovementInput.x <= 0.71 
            && curMovementInput.x >= -0.71 
            && curMovementInput.y >= 0.71)
        {
            _animator.SetBool(runParameterName, true);
            _playerConditions.isRun = true;
        }
        else if (_playerConditions.isRun == true)
        {
            _animator.SetBool(runParameterName, false);
            _playerConditions.isRun = false;
        }
    }

    public void OnFlashLightInput(InputAction.CallbackContext context)
    {
        if(flashLight.gameObject.activeSelf) flashLight.gameObject.SetActive(false);
        else flashLight.gameObject.SetActive(true);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
