using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataStorage;
using Utilities;

///<summary>
/// Sends .cs data to server.
///</summary>
public class SendData : MonoBehaviour {

  /// <summary>Sends data to server and logs in user locally if succesful</summary>
  /// <param name="www">UnityWebRequest created by functions above</param>
  public IEnumerator RequestDataTransfer(UnityWebRequest www)
  {
    //Sends request to server and waits for a response (control given back to Unity in the meantime)
    yield return www.Send();

    Debug.Log("Answer from server after transmitting data:\n" + www.downloadHandler.text);

    //Error in transfer request
    if (www.error != null)
    {
      Debug.Log("Data transmission failed.\nError: " + www.error);
      yield break;
    }

    DataStoring.Instance.Recieving = www.downloadHandler.text;

    ////Error status code
    //int statusCode = 0;

    ////Try and convert to error object
    //ErrorContainer error = JsonUtility.FromJson<ErrorContainer>(JsonHelper.GetSubObject(www.downloadHandler.text));
    //if (error != null)
    //{
    //  //Check that it is not a default object
    //  if (error.status > 0)
    //    statusCode = error.statusCode;
    //}
  } 
}
