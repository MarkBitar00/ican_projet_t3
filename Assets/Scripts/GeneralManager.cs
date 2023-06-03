using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;
    public AnimationRegulator animationRegulator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
