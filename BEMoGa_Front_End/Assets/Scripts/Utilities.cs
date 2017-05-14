using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///Various containers and helpers for data transmissions and receptions.
///May need to be updated for each project. Below is merely a suggestion.
///WARNING! DataReceptionContainer (found at the bottom) must be updated in accordance with each and every project. Create more as necessary.
///</summary>
namespace Utilities
{
  ///<summary>JSON container for errors</summary>
  [System.Serializable]
  public class ErrorContainer
  {
    //Error type, example "Error"
    public string name = "Null";

    //code 401 example
    public int status = -1;

    //Error message
    public string message = "Null";

    //code 401 example
    public int statusCode = -1;

    //All caps code
    public string code = "Null";

    //Stack trace to js code line
    public string stack = "Null";
  }

  ///<summary>JSON container for user login</summary>
  [System.Serializable]
  public class LoginContainer
  {
    //Contains token key
    public string username = "Null";

    //User's access token
    public string id = "Null";

    //User's user Id
    public int userId = -1;
  }

  ///<summary>JSON container for newUser</summary>
  [System.Serializable]
  public class NewUserContainer
  {
    //Username recieved
    public string username = "Null";

    //User's user Id
    public int userId = -1;
  }

  public class JsonHelper
  {
    ///<summary>Converts json array into object array. See https://forum.unity3d.com/threads/how-to-load-an-array-with-jsonutility.375735/ for more info.</summary>
    ///<param name="json">JSON string to turn into an array</param>
    ///<returns>Returns an array of the object type</returns>
    public static T[] getJsonArray<T>(string json)
    {

      string newJson = "{ \"array\": " + json + "}";
      Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
      return wrapper.array;
    }

    ///<summary>Serializable wrapper class for getJsonArray function</summary>
    [System.Serializable]
    private class Wrapper<T>
    {
      public T[] array;
    }

    ///<summary>Quick hack for removing object around error object</summary>
    ///<param name="json">JSON object to get subobject from</param>
    ///<returns>Returns JSON string</returns>
    public static string GetSubObject(string json)
    {
      //Remove "{ "error": " at the start of json
      json = json.Remove(0, 9);

      //Remove "}" at end of json
      json = json.Remove(json.Length - 1);

      return json;
    }
  }

  /// <summary>Container for handling responses when posting something to the server</summary>
  [System.Serializable]
  public class ServerRespond
  {
    public string response;
  }

  /// <summary>Session ID, GameID and EventID container for server storage</summary>
  [System.Serializable]
  public class SessionIDContainer
  {
    public string GameID;
    public string SessionID;
    public string EventID;
  }

  //////////////////////////////////////////////DataReceptionContainer/////////////////////////////////////////////////////
  /// <summary>Container for data recieved from server. Create more similar containers if needed, but remember to update RecieveData.cs if this is necessary.</summary>
  [System.Serializable]
  public class DataReceptionContainer
  {
    //Example string
    public string stuff;
  }
}
