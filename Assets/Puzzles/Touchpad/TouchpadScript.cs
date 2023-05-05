using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchpadScript : MonoBehaviour
{
    public string password;
    public TextMeshPro screenToDisplay;
    private string _enteredPassword = "";

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
        if (password ==  _enteredPassword)
        {
            screenToDisplay.color = Color.green;
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
}
