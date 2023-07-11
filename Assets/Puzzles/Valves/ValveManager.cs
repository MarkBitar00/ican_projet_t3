using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ValveProperties
{
    public GameObject valve;
    public float TargetRotation;
}
public class ValveManager : MonoBehaviour
{
    public List<ValveProperties> Valves;
    public float accuracy;
    [SerializeField] private UnityEvent WhenSolved;
    public void checkValuesOfValves()
    {
        int _valvesGood = 0;
        foreach (var valve in Valves)
        {
            if(Mathf.Abs(valve.valve.transform.rotation.eulerAngles.x - valve.TargetRotation) < accuracy)
            {
                _valvesGood++;
            }
        }
        if (_valvesGood == Valves.Count)
        {
            Finished();
        }
    }
    public void Finished()
    {
        WhenSolved.Invoke();
    }
}
