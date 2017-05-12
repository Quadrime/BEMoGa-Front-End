using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using DataStorage;
using Utilities;

/// <summary>
/// Class for handling new user creation and login requests.
/// Each user login requires a userName, and may include a password and other values such as e-mail, as well as the database field and database URI.
/// WARNING!!! Password protection not optimal in this solution.
/// </summary>
public class NewUser : MonoBehaviour {

  /// <summary>Publicly available newUser function</summary>
  /// <param name="userInfo">Name of user etc., usually obtained from textfields in scene</param>
  /// <param name="fields">Names of field/key for userName etc.</param>s
  /// <param name="URI">URL address of API</param>
  /// <param name="nextScene">Name of next scene to be loaded upon successful login. Pass NULL to not load new scene.</param>
  public void NewUserCreation(string[] userInfo, string[] fields, string URI, string nextScene)
  {
    //Makes sure input arrays are of equal length
    if (userInfo.Length != fields.Length)
    {
      Debug.Log("Error: userInfo and fields must have an equal length array.\nuserInfo.Length: " + userInfo.Length + "\nfields.Length: " + fields.Length);
      return;
    }

    //Creates form to be sent to server
    WWWForm form = new WWWForm();

    //Creates new user info
    if (userInfo.Length > 0)
    {
      for (int i = 0; i < userInfo.Length; i++)
      {
        form.AddField(fields[i], userInfo[i]);
      }
    }
    else
    {
      Debug.Log("Error: NewUser() must be called for at least one user, and only for one user at a time. Length of array must therefore be at least 1. Current length of array: " + userInfo.Length);
    }

    //Creates web request and sends it to RequestDataTransfer
    UnityWebRequest www = UnityWebRequest.Post(URI, form);
    StartCoroutine(LoginRequest(www, nextScene));
  }

  /// <summary>Sends data to server and logs in user locally if succesful</summary>
  /// <param name="www">UnityWebRequest created by functions above</param>
  /// <param name="nextScene">Name of next scene to be loaded upon successful login. Pass NULL to not load new scene.</param>
  private IEnumerator LoginRequest(UnityWebRequest www, string nextScene)
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

    //Try and conver to login object
    LoginContainer login = JsonUtility.FromJson<LoginContainer>(www.downloadHandler.text);
    if (login != null)
    {
      //Check that it is not a default object
      if (login.userId > 0)
      {
        //Store login data
        DataStoring.Instance.SetLoginData = login;

        //Loads next scene
        if (nextScene != null)
        {
          SceneManager.LoadScene(nextScene);
        }

        yield break;
      }
    }

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
