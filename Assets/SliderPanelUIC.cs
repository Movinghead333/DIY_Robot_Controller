using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPanelUIC : MonoBehaviour
{
    public SerialInterface serialInterface;

    public void OnAxis0Changed(float value)
    {
        serialInterface.OnAxisChanged(0, value);
    }

    public void OnAxis1Changed(float value)
    {
        serialInterface.OnAxisChanged(1, value);
    }

    public void OnAxis2Changed(float value)
    {
        serialInterface.OnAxisChanged(2, value);
    }

    public void OnAxis3Changed(float value)
    {
        serialInterface.OnAxisChanged(3, value);
    }

    public void OnAxis4Changed(float value)
    {
        serialInterface.OnAxisChanged(4, value);
    }

    public void OnAxis5Changed(float value)
    {
        serialInterface.OnAxisChanged(5, value);
    }
}
