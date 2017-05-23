using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InfluxBemoga;
using Utilities;
using System;

///<summary>
/// Wrapper for the development 
///</summary>
public class SendData
{
    /// <summary>
    /// 
    /// </summary>
    string text = null;

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string,string> headerResponse = null;

    bool usePrint = false;

    /// <summary>
    /// Create a data with usePrint set as false
    /// </summary>
    public SendData()
    {
        this.headerResponse = new Dictionary<string, string>();
    }

    /// <summary>
    /// Create a SendData with usePrint
    /// </summary>
    /// <param name="usePrint"></param>
    public SendData(bool usePrint)
    {
        this.usePrint = usePrint;
        this.headerResponse = new Dictionary<string, string>();
    }

    /// <summary>
    /// Request an JTTP. PRint if usePrint is true.
    /// </summary>
    /// <param name="url"> Url of the targeted Influx DB</param>
    /// <param name="rawData">Data that we send to the Influx DB</param>
    /// <returns></returns>
    public IEnumerator httpRequest(string url, byte[] rawData)
    {
        // Post a request to an URL with our custom headers
        WWW www = new WWW(url, rawData);
        yield return www;

        if (this.usePrint)
        {
            if (www.responseHeaders.Count > 0 && www.responseHeaders != null)
            {
                foreach (var entry in www.responseHeaders)
                {
                    Debug.Log(entry.Key + "=" + entry.Value);
                }
            }

            if (!String.IsNullOrEmpty(www.text))
            {
                Debug.Log(www.text);
            }
        }


    }


    /// <summary>
    /// Debug purpose
    /// </summary>
    public void printHeader()
    {
        if(this.headerResponse.Count > 0 && this.headerResponse != null)
        {
            foreach (var entry in this.headerResponse)
            {
                Debug.Log(entry.Key + "=" + entry.Value);
            }
        }

    }

    /// <summary>
    /// Debug Purpose
    /// </summary>
    public void printText()
    {
        if(!String.IsNullOrEmpty(text))
        {
            Debug.Log(text);
        }
    }

    /// <summary>
    /// Debug Purpose
    /// </summary>
    public void printData()
    {
        printHeader();
        printText();
    }
}

