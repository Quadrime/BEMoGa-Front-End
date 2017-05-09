using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

/* 
 * Sends .cs data to server.
 * All URI's must be supplied manually and sent to each field. Example of URI: http://www.serverplacement.no/projectName/api/field
 * Supports single values and arrays.
 * To send non-string values, use the .ToString() function when sending them to SendData.
 */
public class SendData : MonoBehaviour {

  /*
   * Sends a single value to the server
   * @Value: value = integer value to be sent
   * @Value: field = name of field/key
   * @Value: URI = URL address of API
   */
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

  /*
   * Sends a single integer to the server
   * @Value: value = integer array to be sent
   * @Value: field = name of field/key
   * @Value: URI = URL address of API
   */
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

  /*
   * Sends data to server
   * @Value: www = UnityWebRequest created by functions above
   */ 
  private IEnumerator RequestDataTransfer(UnityWebRequest www)
  {
    yield return www.Send();

    Debug.Log("Answer from server after transmitting data:\n" + www.downloadHandler.text);

    if (www.error != null)
    {
      Debug.Log("Data transmission failed.\nError: " + www.error);
      yield break;
    }

    //Try and convert to error object
    ErrorContainer error = JsonUtility.FromJson<ErrorContainer>(JsonHelper.GetSubObject(www.downloadHandler.text));
    if (error != null)
    {
      //Check that it is not a default object
      if (error.status > 0)
        Debug.Log(error.message);
    }
  } 
}
