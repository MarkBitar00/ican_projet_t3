using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTimeAnchor : MonoBehaviour
{
    public string eventToActivate;

    private void Start()
    {
        GeneralManager.instance.animationRegulator.SubscribeToEvent(eventToActivate, this);
        GeneralManager.instance.animationRegulator.SubscribeToEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, this);
    }

    public void ReactToEvent(string _event,float _additionalParameter)
    {
        if (_event == eventToActivate)
        {
            //activate echo
        }
        if(_event == GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent)
        {
            GetComponent<Animator>().SetFloat("animationSpeed",_additionalParameter);
        }
    }
}
