using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TouchpadScript : MonoBehaviour
{
    public string password;
    public TextMeshPro screenToDisplay;
    private string _enteredPassword = "";
    public int _aditionalCondition;
    public HingeJoint interactableToUnlock;
    [SerializeField] private UnityEvent WhenSolved;

    public void Finished()
    {
        WhenSolved.Invoke();
    }

    public void AddKeyToPassword(string _key)
    {
        if(_enteredPassword.Length< password.Length)
        {
            _enteredPassword += _key;
            screenToDisplay.text = _enteredPassword;
            if (_enteredPassword.Length == password.Length)
            {
                CheckPassword();
            }
        }
    }
    private void CheckPassword()
    {
        if (password ==  _enteredPassword && _aditionalCondition <= 0)
        {
            screenToDisplay.color = Color.green;
            if(interactableToUnlock != null)
            {
                JointLimits _limit = interactableToUnlock.limits;
                _limit.max = 45;
                interactableToUnlock.limits = _limit;
            }
        }
        else
        {
            screenToDisplay.color = Color.red;
            Invoke("ResetPassword",1);
        }
    }
    public void ResetPassword()
    {
        screenToDisplay.color = Color.black;
        _enteredPassword = "";
        screenToDisplay.text = _enteredPassword;
    }
    public void CheckAditionalCondition(int _aditionnalConditionChecked)
    {
        _aditionalCondition -= _aditionnalConditionChecked;
    }
}