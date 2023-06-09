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
    [SerializeField] private UnityEvent AllPillarAreGoods; // permet d'avoir la liste des callback à un event
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

    public void Finished() {
        AllPillarAreGoods.Invoke();
    }

}
