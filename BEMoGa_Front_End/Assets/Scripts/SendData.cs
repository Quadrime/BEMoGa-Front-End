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
public class SendData {





  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  public IEnumerator InfluxRequest(string url,byte[] data)
  {
        
        UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        UploadHandlerRaw MyUploadHandler = new UploadHandlerRaw(data);
        MyUploadHandler.contentType = "application/x-www-form-urlencoded"; // might work with 'multipart/form-data'
        request.uploadHandler = MyUploadHandler;
        yield return request.Send();


        if (request.isError)
        {
            Debug.Log("Http request error: " + request.error);
            if (request.downloadHandler != null)
            {
                Debug.Log(request.downloadedBytes);
            }
        }
        else
        {
            if (request.responseCode == 204)
            {
                Debug.Log("Post sucessfull" + request.responseCode);
                // no point printing the result, 204comes back with an empty body.
            }
            else if (request.responseCode == 401)
            {
                Debug.Log("Error 401: Unauthorized.");
                if (request.downloadHandler.text != null)
                {
                    Debug.Log(request.downloadHandler.text);
                }

            }

            else
            {
                Debug.Log("Request failed (status:" + request.responseCode + ").");
                if (request.downloadHandler.text != null)
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
    }
}
