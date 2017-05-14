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

  /// <summary>Container for data recieved from server. Create more similar containers if needed, but remember to update RecieveData.cs if this is necessary.</summary>
  [System.Serializable]
  public class DataReceptionContainer
  {
    //Example string
    public string stuff;
  }

  //[System.Serializable]
  //public class GameContainer
  //{
  //  string id;
  //}

  //[System.Serializable]
  //public class GameSessionContainer
  //{
  //  int id;
  //}

  [System.Serializable]
  public class EventSessionContainer
  {
    int id;
  }
}
