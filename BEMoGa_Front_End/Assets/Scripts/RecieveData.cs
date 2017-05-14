using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Recieves Json data from the server and translates it
/// </summary>
public class RecieveData {

  /// <summary>
  /// Sends a request to the server to recieve data.
  /// As it may take time to recieve the data from the server, the data is stored in DataStorage, where you must check if it has been updated or not (from an Update() function)
  /// </summary>
  /// <param name="URI">URL adress of table</param>
  public void GetDataFromServer(string URI, MonoBehaviour mono)
  {
    //Create request
    UnityWebRequest www = UnityWebRequest.Get(URI);
    //Get the score
    mono.StartCoroutine(RequestData(www));
  }

  public void GetGameIdFromServer(string URI, MonoBehaviour mono)
  {
    //Create request
    UnityWebRequest www = UnityWebRequest.Get(URI);
    //Get the score
    mono.StartCoroutine(RequestGameId(www));
  }

  /// <summary>
  /// Sends a request to the server to recieve data.
  /// As it may take time to recieve the data from the server, the data is stored in DataStorage, where you must check if it has been updated or not (from an Update() function)
  /// </summary>
  /// <param name="URI"></param>
  /// <param name="variableName"></param>
  /// <param name="valueName"></param>
  public void GetDataFromServer(string URI, string variableName, string valueName, MonoBehaviour mono)
  {
    URI += '?' + variableName + '=' + valueName;
    //Create request
    UnityWebRequest www = UnityWebRequest.Get(URI);
    //Get the score
    mono.StartCoroutine(RequestData(www));
  }

  /// <summary></summary>
  /// <param name="www"></param>
  /// <returns>Returns control to user when done</returns>
  public IEnumerator RequestData(UnityWebRequest www)
  {
    Debug.Log("1");
    //Wait for response
    yield return www.Send();
    Debug.Log("2");
    //Errors
    if (www.error != null)
    {
      Debug.Log("Error while requesting data transfer from server:\n" + www.error);
      yield break;
    }
    else
    {
      DataStoring.Instance.DataContainerStuff = www.downloadHandler.text;

      yield break;
    }
  }

  public IEnumerator RequestGameId(UnityWebRequest www)
  {
    Debug.Log("1");
    //Wait for response
    yield return www.Send();
    Debug.Log("2");
    //Errors
    if (www.error != null)
    {
      Debug.Log("Error while requesting data transfer from server:\n" + www.error);
      yield break;
    }
    else
    {
      DataStoring.Instance.Game.id = www.downloadHandler.text;

      yield break;
    }
  }
}
