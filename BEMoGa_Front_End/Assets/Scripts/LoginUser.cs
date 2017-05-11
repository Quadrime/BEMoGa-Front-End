using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Utilities;
using DataStorage;

/// <summary>
/// Class for handling user login requests.
/// Each user login requires a userName, and may include a password, as well as the database field and database URI.
/// WARNING!!! Password protection not optimal in this solution.
/// </summary>
public class LoginUser : MonoBehaviour {

   /// <summary>Publicly available login function, send 1 value of []userNameAndPassword and []fields for login without pasword</summary>
   /// <param name="userNameAndPassword">Name of user and password of user, usually obtained from textfields in scene</param>
   /// <param name="fields">Names of field/key for userName and password</param>s
   /// <param name="URI">URL address of API</param>
   /// <param name="nextScene">Name of next scene to be loaded upon successful login. Pass NULL to not load new scene.</param>
  public void Login(string[] userNameAndPassword, string[] fields, string URI, string nextScene)
  {
    //Makes sure input arrays are of equal length
    if (userNameAndPassword.Length != fields.Length)
    {
      Debug.Log("Error: UserNameAndPassword and fields must have an equal length array.\nUserNameAndPassword.Length: " + userNameAndPassword.Length + "\nfields.Length: " + fields.Length);
      return;
    }

    //Creates form to be sent to server
    WWWForm form = new WWWForm();

    //Logs in user either with or without a password
    switch (userNameAndPassword.Length)
    {
      case 1: form.AddField(fields[0], userNameAndPassword[0]); break;
      case 2: form.AddField(fields[0], userNameAndPassword[0]); form.AddField(fields[1], userNameAndPassword[1]); break;
      default: Debug.Log("Error: Login() must be called for at least one user, and only for one user at a time. Length of array must therefore be 1 or 2. Current length of array: " + userNameAndPassword.Length); break;
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
        GlobalData.Instance.SetLoginData = login;

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
