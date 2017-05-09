using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

///<summary>
/// Sends .cs data to server.
/// All URI's must be supplied manually and sent to each field. Example of URI: http://www.serverplacement.no/projectName/api/field
/// Supports single values and arrays.
/// To send non-string values, use the .ToString() function when sending them to SendData.
///</summary>
public class SendData : MonoBehaviour {


   ///<summary>Sends a single value to the server</summary>
   ///<param name="value">Value to be sent</param>
   ///<param name="field">Name of field/key</param>
   ///<param name="URI">URL address of API</param>
  public void SendSingle(string value, string field, string URI)
  {
    //Creates form to be sent to server
    WWWForm form = new WWWForm();

    //Adds all fields and values
    form.AddField(field, value);

    //Creates web request and sends it to RequestDataTransfer
    UnityWebRequest www = UnityWebRequest.Post(URI, form);
    StartCoroutine(RequestDataTransfer(www));
  }

  ///<summary>Sends a single value to the server</summary>
  ///<param name="value">Value array to be sent</param>
  ///<param name="field">Names of field/key</param>
  ///<param name="URI">URL address of API</param>
  public void SendArray(string[] value, string[] field, string URI)
  {
    //Creates form to be sent to server
    WWWForm form = new WWWForm();

    //Adds all fields and values
    for (int i = 0; i > value.Length; i++)
    {
      form.AddField(field[i], value[i]);
    }

    //Creates web request and sends it to RequestDataTransfer
    UnityWebRequest www = UnityWebRequest.Post(URI, form);
    StartCoroutine(RequestDataTransfer(www));
  }

  /// <summary>Sends data to server and logs in user locally if succesful</summary>
  /// <param name="www">UnityWebRequest created by functions above</param>
  private IEnumerator RequestDataTransfer(UnityWebRequest www)
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

    //Error status code
    int statusCode = 0;

    //Try and convert to error object
    ErrorContainer error = JsonUtility.FromJson<ErrorContainer>(JsonHelper.GetSubObject(www.downloadHandler.text));
    if (error != null)
    {
      //Check that it is not a default object
      if (error.status > 0)
        statusCode = error.statusCode;
    }
  } 
}
