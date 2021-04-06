using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;

public class SerialPortSelectionUIC : MonoBehaviour
{
    public TextMeshProUGUI serialPortTextMesh;
    public TextMeshProUGUI serialPortConnected;
    public SerialInterface serialInterface;

    public void OnCycleSerialPortButtonPressed()
    {
        serialInterface.OnCycleSerialPortButtonPressed();
    }

    public void OnConnectToSerialPortButtonPressed()
    {
        serialInterface.OnConnectToSerialPortButtonPressed();
    }

    public void SetSelectedSerialPort(string serialPort)
    {
        serialPortTextMesh.text = serialPort;
    }

    public void SetSerialPortConnected()
    {
        serialPortConnected.text = "connected";
        serialPortConnected.color = Color.green;
    }
}
