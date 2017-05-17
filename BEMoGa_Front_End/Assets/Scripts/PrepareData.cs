using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Prepares data for transfer
/// </summary>
public class PrepareData
{
    // wrapper functions here 


    public void writeMeasurment(MonoBehaviour mono, string data)
    {
        var temp = DataStoring.Instance.getServerURL() + DataStoring.Instance.getNameDB();
        Debug.Log(temp);
        SendData sd = new SendData();
        var bytes = strToByte(data);
        mono.StartCoroutine(sd.InfluxRequest(temp, bytes));
    }

    private byte[] strToByte(string data)
    {
        return System.Text.Encoding.UTF8.GetBytes(data);
    }
}
