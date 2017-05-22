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
    Dictionary<string,string> headerResponse = null;


    /// <summary>
    /// 
    /// </summary>
    public SendData()
    {
        this.headerResponse = new Dictionary<string, string>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="rawData"></param>
    /// <returns></returns>
    public IEnumerator httpRequest(string url, byte[] rawData)
    {
        // Post a request to an URL with our custom headers
        WWW www = new WWW(url, rawData);
        yield return www;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="rawData"></param>
    /// <returns></returns>
    public IEnumerator httpRequestWithPrint(string url, byte[] rawData)
    {
        // Post a request to an URL with our custom headers
        WWW www = new WWW(url, rawData);
        yield return www;

        if (www.responseHeaders.Count > 0)
        {
            this.headerResponse = www.responseHeaders;
        }
 
        this.text = www.text;
        printData();
    }

    /// <summary>
    /// 
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
    /// 
    /// </summary>
    public void printText()
    {
        if(String.IsNullOrEmpty(text))
        {
            Debug.Log(text);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void printData()
    {
        printHeader();
        printText();
    }
}

