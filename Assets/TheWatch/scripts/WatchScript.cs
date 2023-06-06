using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class WatchScript : MonoBehaviour
{
    public AnimationRegulator echosManager;
    public XRIDefaultInputActions inputMap;
    public string actualMode = "selectionMode"; // modes are : selectionMode, viewMode
    public GameObject watchObject;
    public GameObject hoursNeedle;
    public GameObject minutesNeedle;
    public GameObject watchAnchor;

    public GameObject ghostNeedle;
    public GameObject ghostNeedleAnchor;
    private GameObject _theNeedleHandle;

    public List<int> rotationLocks;

    private int _nextUpdate = 1;
    private bool _isRotating;
    private bool _hadSnapped = true;


    private void Awake()
    {
        inputMap = new XRIDefaultInputActions();
    }
    private void OnEnable()
    {
        inputMap.Enable();
        inputMap.XRILeftHand.AllJoystickInput.performed += AllJoystickInput_performed;
        inputMap.XRILeftHand.ShowWatch.performed += ShowWatch_performed;
    }
    private void OnDisable()
    {
        inputMap.Disable();
    }

    public void Update()
    {
        if (Time.time >= _nextUpdate)
        {
            _nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            UpdateEverySecond();
        }
    }

    private void FixedUpdate()
    {
        if(_theNeedleHandle != null && _theNeedleHandle.transform.localEulerAngles.x > 5 && _theNeedleHandle.transform.localEulerAngles.x < 355  && _theNeedleHandle.GetComponent<XRGrabInteractable>().isSelected)
        {
            float _input = _theNeedleHandle.transform.localEulerAngles.x;
            if (_input > 180)
            {
                _input = (360 - _input) * -1;
            }
            _input *= -1;
            InterceptHandleInteraction(_input / 45);
        }
    }
    public void UpdateEverySecond()
    {
        AutoSnapping();
    }
    private void AllJoystickInput_performed(InputAction.CallbackContext obj)
    {
        Vector2 _input = obj.ReadValue<Vector2>();
        if(actualMode == "selectionMode")
        {
            hoursNeedle.transform.Rotate(0, _input[0], 0);
            if (!_isRotating) { _isRotating = true; }
            if (_hadSnapped) { _hadSnapped = false; }
        }
        if(actualMode == "viewMode")
        {
            if(_input.x != 0)
            {
                GeneralManager.instance.animationRegulator.CallEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, _input.x);
            }
        }
    }

    private void InterceptHandleInteraction(float _inclinaison)
    {
        if (actualMode == "selectionMode")
        {
            hoursNeedle.transform.Rotate(0, _inclinaison, 0);
            if (!_isRotating) { _isRotating = true; }
            if (_hadSnapped) { _hadSnapped = false; }
        }
        if (actualMode == "viewMode")
        {
                GeneralManager.instance.animationRegulator.CallEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, _inclinaison);
        }
    }
    private void ShowWatch_performed(InputAction.CallbackContext obj)
    {
        if (!watchObject.activeSelf)
        {
            ghostNeedleAnchor.transform.position = watchAnchor.transform.position;
            ghostNeedleAnchor.transform.rotation = watchAnchor.transform.rotation;
            _theNeedleHandle = Instantiate<GameObject>(ghostNeedle, ghostNeedleAnchor.transform);
            watchObject.SetActive(true);
            actualMode = "selectionMode";
        }
        else
        {
            Destroy(_theNeedleHandle);
            hoursNeedle.transform.localEulerAngles = Vector3.zero;
            watchObject.SetActive(false);
            GeneralManager.instance.animationRegulator.CallEvent(GeneralManager.instance.animationRegulator.nameOfDeativationEvent);
        }
    }
    private int GetClosestWatchAnchor(float _yRotation)
    {
        float _data = _yRotation % 360;
        int _bestLock = 0;
        foreach (int _rotation in rotationLocks)
        {
            if (Mathf.Abs(_rotation - _yRotation) < Mathf.Abs(_bestLock - _yRotation))
            {
                _bestLock = _rotation;
            }
        }
        return _bestLock;
    }
    private void AutoSnapping()
    {
        if (_isRotating)
        {
            _isRotating = false;
        }
        else if (!_hadSnapped)
        {
            hoursNeedle.transform.localEulerAngles = new Vector3(0, GetClosestWatchAnchor(hoursNeedle.transform.localEulerAngles.y), 0);
            _hadSnapped = true;
            Invoke("ToViewMode", 0.6f);
        }
    }
    private void ToViewMode()
    {
        int _time = ((int)hoursNeedle.transform.localEulerAngles.y) / 30;
        echosManager.CallEvent(_time + "h");
        actualMode = "viewMode";
    }
}
