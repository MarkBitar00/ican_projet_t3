using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChannelPillarState
{
    public GameObject Pillar;
    public bool state;
}

public class ChannelMachineScript : MonoBehaviour
{
    
    public List<ChannelPillarState> pillars;
    [SerializeField] private UnityEvent WhenSolved;

    public void Finished()
    {
        WhenSolved.Invoke();
    }
    public void changeState(GameObject _pillar, bool _state) {
        bool allPillarsAreGood = true;
        foreach(ChannelPillarState pillar in pillars)
        {
            if (pillar.Pillar == _pillar)
            {
                pillar.state = _state;
            }
            if (allPillarsAreGood)
            {
                allPillarsAreGood = pillar.state;
            }
        }
        if (allPillarsAreGood)
        {
            Finished();
        }
    }

    

}
