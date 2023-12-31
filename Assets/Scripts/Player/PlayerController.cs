using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    bool canControllFlash=true;
    [Header("Movement")]
    public float moveSpeed;
    public float moveMaxSpeed;
    public float runSpeed;
    public float runMaxSpeed;
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

    public int flashGrenadeNum;
    public FlashGrenade _flashGrenade;

    [SerializeField] private AudioClip flashLightSFX;
    [SerializeField] private Camera _camera;
     public MonsterCControl targetMonsterC;
    Vector3 viewPos;



    public static PlayerController instance;
    private void Awake()
    {

        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _playerConditions = GetComponent<PlayerConditions>();
        _animator = GetComponentInChildren<Animator>();
        _flashGrenade = GetComponentInChildren<FlashGrenade>();

        flashGrenadeNum = 3;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!_playerConditions.isRun)
        {
            _animator.SetBool(runParameterName, false);
            Move();
        }
        else Run();

        _animator.SetFloat("Blend", 1 - _playerConditions.stamina.GetPercentage());
        moveSpeed = moveMaxSpeed * (1-(1.01f-_playerConditions.stamina.GetPercentage())/2);
        runSpeed = runMaxSpeed * (1-(1.01f-_playerConditions.stamina.GetPercentage())/2);
    }

    private void Update()
    {
        Marking.I.IndexChange(); //�ٰ��� ���� ��ŷ ������ ����

        if (targetMonsterC)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(targetMonsterC.transform.position);

            if (viewPos.x >= -0.2f && viewPos.x <= 1.2f &&
                viewPos.y >= -0.2f && viewPos.y <= 1.2f &&
                viewPos.z > 0 && viewPos.z <= 20 && flashLight.enabled)
            {
                targetMonsterC.agent.isStopped = true;
            }
            else
            {
                targetMonsterC.agent.isStopped = false;
            }
        }
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
        if (canControllFlash)
        {
            SoundManager.instance.PlaySFX(flashLightSFX, 0.5f);
            canControllFlash = false;
            FlashToggle();
            Invoke(nameof(ControllOn), 1f);//지?�함?? nameof ?�수�?문자가 ?�닌 ?�수�?바로 ?�출 
        }
    }
    private void FlashToggle()
    {
        bool active = !flashLight.enabled;
        flashLight.enabled = active;
    }

    public void ControllOn()
    {
        canControllFlash = true;
    }
    public void OnMakringInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 5))
                {
                if (hit.collider.tag == "Marking" && Marking.I.index ==3)
                {
                    Marking.I.RemoveMarkingData(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                    
                }
                else if(Marking.I.index != 3)
                {
                    float x = 0f;
                    float z = 0f;
                    Vector3 wallNormal = hit.normal;
                    Quaternion markRotation = Quaternion.LookRotation(wallNormal);
                    if (Marking.I.player.transform.rotation.eulerAngles.y < 90 && Marking.I.player.transform.rotation.eulerAngles.y >= 0)
                    {
                        z = -0.1f;
                    }
                    else if (Marking.I.player.transform.rotation.eulerAngles.y < 180 && Marking.I.player.transform.rotation.eulerAngles.y >= 90)
                    {
                        x = -0.1f;
                    }
                    else if (Marking.I.player.transform.rotation.eulerAngles.y < 270 && Marking.I.player.transform.rotation.eulerAngles.y >= 180)
                    {
                        z = 0.1f;
                    }
                    else
                    {
                        x = 0.1f;
                    }
                    Vector3 zxOffset = new Vector3(x, 0, z);
                    GameObject test = Instantiate(Marking.I.Markings[Marking.I.index], hit.point + zxOffset, markRotation);
                    Marking.I.SaveMarkingData(test, markRotation);
                }
                }
        }
    }
    public void OnMenuInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            UIManager.Instance.PopPanel();
        }
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    public void OnFlashGrenadeInput(InputAction.CallbackContext context)
    {
        if (flashGrenadeNum > 0 && _flashGrenade != null && !_flashGrenade.Flashing)
        {
            flashGrenadeNum--;
            PlayerUI.instance.FlashGrenadeEffectOn();
            _flashGrenade.Flash();
        }
    }

    internal void AddFlashGrenade()
    {
        if(flashGrenadeNum < 3)
        {
            flashGrenadeNum++;
            PlayerUI.instance.UpdateFlashGrenadeUI();
        }
    }
}

