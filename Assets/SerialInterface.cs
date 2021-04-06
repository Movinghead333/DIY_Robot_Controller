using UnityEngine;
using System.IO.Ports;
using System;

public class SerialInterface : MonoBehaviour
{
    #region Singleton
    public static SerialInterface Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public SerialPortSelectionUIC serialPortSelectionUIC;

    // Serial communication fields
    SerialPort serialPort;
    string[] serialPortNames;
    int currentIndex = 0;

    // fields for updates per second mechanic
    float updatesPerSecond = 100;
    float currentTimer = 0;

    // message buffer for outgoing messages
    byte[] message = new byte[2];

    // initialize serialport on startup
    void Start()
    {
        serialPortNames = SerialPort.GetPortNames();
        if (serialPortNames.Length > 0)
        {
            serialPortSelectionUIC.SetSelectedSerialPort(serialPortNames[currentIndex]);
        }
    }

    // update the timer for the updates per second mechanic
    private void Update()
    {
        currentTimer -= Time.deltaTime;
        currentTimer = Mathf.Clamp(currentTimer, 0.0f, 1.0f);
    }

    // public callback called from the slider of the different axes
    public void OnAxisChanged(byte axis, float value)
    {
        if (serialPort == null || !serialPort.IsOpen)
        {
            return;
        }

        // only allow <updatesPerSecond> updates via serial communication
        // per second to limit traffic
        if (currentTimer > 0) return;
        currentTimer = 1f / updatesPerSecond;

        // this yields value from 250-255 for the six axes of the robot
        message[0] = (byte)(250 + axis);
        // angle from of the sliders controlling the different axes
        message[1] = (byte)Math.Round(value, 0);

        // write message and flush the stream to send it via serialport
        serialPort.Write(message, 0, 2);
        serialPort.BaseStream.Flush();
        Debug.Log("Message sent");
    }

    public void OnCycleSerialPortButtonPressed()
    {
        if (serialPortNames.Length > 0)
        {
            // cycle index in length of serialPortNames array
            currentIndex = (currentIndex + 1) % serialPortNames.Length;
            serialPortSelectionUIC.SetSelectedSerialPort(serialPortNames[currentIndex]);
        }
    }

    public void OnConnectToSerialPortButtonPressed()
    {
        if (serialPortNames.Length > 0)
        {
            serialPort = new SerialPort(serialPortNames[currentIndex], 9600);
            serialPort.ReadTimeout = 1;
            serialPort.Open();
            if (serialPort.IsOpen)
            {
                serialPortSelectionUIC.SetSerialPortConnected();
            }
            Debug.Log("Serial Port opened.");
        }
    }


    // test code
    byte servoID = 0;
    byte servoAngle = 0;

    public void TestButtonPressed()
    {
        Debug.Log("Test button pressed");
        message[0] = servoID;
        message[1] = servoAngle;
        serialPort.Write(message, 0, 2);
        serialPort.BaseStream.Flush();
    }
}
