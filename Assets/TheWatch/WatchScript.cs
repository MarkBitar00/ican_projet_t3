using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class AnimationRegulator : MonoBehaviour
{
    public Dictionary<string,List<AnimationTimeAnchor>> animations;
    public string nameOfModifySpeedEvent;

    public void SubscribeToEvent(string _eventToSubscribeTo, AnimationTimeAnchor _subcriber)
    {
        if (!animations.ContainsKey(_eventToSubscribeTo))
        {
            animations.Add(_eventToSubscribeTo, new List<AnimationTimeAnchor>());
        }
        animations[_eventToSubscribeTo].Add(_subcriber);
    }

    public void CallEvent(string _eventName, float _additionalParameter = 0)
    {
        if (animations.ContainsKey(_eventName))
        {
            foreach (AnimationTimeAnchor _subcribers in animations[_eventName])
            {
                _subcribers.ReactToEvent(_eventName,_additionalParameter);
            }
        }
    }
}
