using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataStorage;
using Utilities;
using System;

///<summary>
/// Sends .cs data to server.
///</summary>
public class SendData
{
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
        
        if (www.responseHeaders.Count > 0)
        {
            foreach (KeyValuePair<string, string> entry in www.responseHeaders)
            {
                Debug.Log(entry.Key + "=" + entry.Value);
            }
        }

        Debug.Log(www.text);
    }
}

