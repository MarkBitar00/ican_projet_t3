using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTimeAnchor : MonoBehaviour
{
    public string eventToActivate;
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinMeshRenderer;
    public Animator animator;

    private void Start()
    {
        GeneralManager.instance.animationRegulator.SubscribeToEvent(eventToActivate, this);
        GeneralManager.instance.animationRegulator.SubscribeToEvent(GeneralManager.instance.animationRegulator.nameOfDeativationEvent, this);
        GeneralManager.instance.animationRegulator.SubscribeToEvent(GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent, this);
    }

    public void ReactToEvent(string _event,float _additionalParameter)
    {
        if (_event == eventToActivate)
        {
            if (meshRenderer != null) { meshRenderer.enabled = true; }
            if (skinMeshRenderer != null) { skinMeshRenderer.enabled = true; }
            animator.enabled = true;
        }
        if (_event == GeneralManager.instance.animationRegulator.nameOfModifySpeedEvent)
        {
            GetComponent<Animator>().SetFloat("animationSpeed",_additionalParameter);
        }
        if (_event == GeneralManager.instance.animationRegulator.nameOfDeativationEvent)
        {
            if(meshRenderer != null ) { meshRenderer.enabled = false;}
            if(skinMeshRenderer != null) { skinMeshRenderer.enabled = false; }
            animator.enabled = false;
        }
    }
}
