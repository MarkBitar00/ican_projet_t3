using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ChannelPillarScript : MonoBehaviour
{
    public ChannelMachineScript channelMachine;
    public Dictionary<GameObject,int> charges = new Dictionary<GameObject,int>();
    public int actualPower;
    public int expectedPower;

    public void ActualizeSocket(GameObject _socket)
    {
        charges[_socket] = _socket.GetComponent<XRSocketInteractor>().GetOldestInteractableSelected().transform.gameObject.GetComponent<CrystalScript>().Power;
        ActualizePower();
    }

    public void ResetSocket(GameObject _socket)
    {
        charges[_socket] = 0;
        ActualizePower();
    }

    public void ActualizePower() {
        actualPower = 0;
        foreach (KeyValuePair<GameObject, int> entry in charges)
        {
            actualPower += entry.Value;
        }
        if(actualPower == expectedPower)
        {
            channelMachine.changeState(this.gameObject,true);
        }
        else
        {
            channelMachine.changeState(this.gameObject, false);
        }
    }
}
