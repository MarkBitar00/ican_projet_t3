using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
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
    public List<GameObject> hoursFragments;

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
        if (_theNeedleHandle != null && _theNeedleHandle.transform.localEulerAngles.x > 5 && _theNeedleHandle.transform.localEulerAngles.x < 355 && _theNeedleHandle.GetComponent<XRGrabInteractable>().isSelected)
        {
            float _input = _theNeedleHandle.transform.localEulerAngles.x;
            if (_input > 180)
            {
                _input = (360 - _input) * -1;
            }
            _input *= -1;
            InterceptHandleInteraction(_input / 45);
        }
        else if(echosManager.animationTime != 0) {
            InterceptHandleInteraction(0);
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
            hoursNeedle.transform.Rotate(-_input[0], 0, 0);
            if (!_isRotating) { _isRotating = true; }
            if (_hadSnapped) { _hadSnapped = false; }
        }
        if(actualMode == "viewMode")
        {
            if(_input.x != 0)
            {
                minutesNeedle.transform.Rotate(-_input[0], 0, 0);
                GeneralManager.instance.animationRegulator.CallEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, -_input.x);
            }
        }
    }

    private void InterceptHandleInteraction(float _inclinaison)
    {
        if (actualMode == "selectionMode")
        {
            hoursNeedle.transform.Rotate(-_inclinaison, 0, 0);
            if (!_isRotating) { _isRotating = true; }
            if (_hadSnapped) { _hadSnapped = false; }
        }
        if (actualMode == "viewMode")
        {
            minutesNeedle.transform.Rotate(-_inclinaison, 0, 0);
            GeneralManager.instance.animationRegulator.CallEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, _inclinaison);
        }
    }

    private void ShowWatch_performed(InputAction.CallbackContext obj)
    {
        if (!watchObject.activeSelf)
        {
            ghostNeedleAnchor.transform.position = watchAnchor.transform.position;
            ghostNeedleAnchor.transform.eulerAngles = new Vector3(0, watchAnchor.transform.eulerAngles.y, 0);
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

    private int GetClosestWatchAnchor(Transform _needleTransform)
    {
        float _data = FixWeirdRotation(_needleTransform);
        
        int _bestLock = 0;
        foreach (int _rotation in rotationLocks)
        {
            if (Mathf.Abs(_rotation - _data) < Mathf.Abs(_bestLock - _data))
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
            hoursNeedle.transform.localEulerAngles = new Vector3(GetClosestWatchAnchor(hoursNeedle.transform),0, 0);
            _hadSnapped = true;
            Invoke("ToViewMode", 0.6f);
        }
    }

    private float FixWeirdRotation(Transform _weirdRotation)
    {
        float _data = 0;
        if (_weirdRotation.localEulerAngles.y == 0)
        {
            _data = _weirdRotation.localEulerAngles.x;
        }
        else
        {
            _data = ((360 - _weirdRotation.localEulerAngles.x) + 180)%360;
        }
        return _data;
    }

    private void ToViewMode()
    {
        int _time = 12 - ((int)FixWeirdRotation(hoursNeedle.transform) / 30);
        echosManager.CallEvent(_time + "h");
        actualMode = "viewMode";
    }

    public void AddHoursToWatch(int _hour)
    {
        rotationLocks.Add(360 - (_hour * 30));
        foreach (GameObject _fragment in hoursFragments) {
            if(_fragment.name =="heure_" + _hour)
            {
                _fragment.SetActive(true);
            }
        }
    }
}
