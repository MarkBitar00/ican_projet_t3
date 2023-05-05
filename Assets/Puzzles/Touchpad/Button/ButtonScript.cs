using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [Header("-------------- Setup Variables --------------")]
    [Space(3)]
    public TouchpadScript touchpadAttachedTo;
    public TextMeshPro keyDisplay;
    public List<Material> materials;
    [Space(3)]
    [Header("---------------- parameters ----------------")]
    public string key;
    private MeshRenderer mesh;


    public void Start()
    {
        keyDisplay.text = key;
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    public void TouchPressed()
    {
        touchpadAttachedTo.AddKeyToPassword(key);
    }

    public void ButtonSelected()
    {
        mesh.material = materials[1];
    }

    public void ButtonUnselected()
    {
        mesh.material = materials[0];
    }
}
