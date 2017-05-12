using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Recieves Json data from the server and translates it
/// </summary>
public class RecieveData : MonoBehaviour {

  /// <summary>
  /// Sends a request to the server to recieve data.
  /// As it may take time to recieve the data from the server, the data is stored in DataStorage, where you must check if it has been updated or not (from an Update() function)
  /// </summary>
  /// <param name="URI">URL adress of table</param>
  public void GetDataFromServer(string URI)
  {
    //Create request
    UnityWebRequest www = UnityWebRequest.Get(URI);
    //Get the score
    StartCoroutine(RequestData(www));
  }

  /// <summary></summary>
  /// <param name="www"></param>
  /// <returns>Returns control to user when done</returns>
  IEnumerator RequestData(UnityWebRequest www)
  {
    //Wait for response
    yield return www.Send();

    //Errors
    if (www.error != null)
    {
      Debug.Log("Error while requesting data transfer from server:\n" + www.error);
      yield break;
    }
    else
    {
      DataReceptionContainer[] objects = JsonHelper.getJsonArray<DataReceptionContainer>(www.downloadHandler.text);
      foreach (DataReceptionContainer data in objects)
      {
        DataStoring.Instance.AddToDataContainerStuff(data.stuff);
      }

      yield break;
    }
  }
}
