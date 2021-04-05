using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisSliderUIC : MonoBehaviour
{
    public TextMeshProUGUI sliderValueText;

    public void OnValueChanged(float value)
    {
        sliderValueText.text = value.ToString();
    }
}
