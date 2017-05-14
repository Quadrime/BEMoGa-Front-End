using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DataStorage
{
  /// <summary>
  /// Class for storing data. Data persists troughout scenes.
  /// WARNING! All DataReceptionContainer functions (found at the bottom) must be updated in accordance with each and every project. Create more as necessary.
  /// </summary>
  public class DataStoring
  {
    //Variables that are stored
    LoginContainer loginInfo = null;
    ServerRespond dataRecieved = new ServerRespond();
    DataReceptionContainer dataContainer = new DataReceptionContainer();
    GameContainer gameContainer = new GameContainer();
    GameSessionContainer gameSessionContainer = new GameSessionContainer();
    EventSessionContainer eventSeessionContainer = new EventSessionContainer();
    SessionIDContainer sessionContainer = new SessionIDContainer();
    Dictionary<string, string> dataDictionary = new Dictionary<string, string>();
    private static DataStoring instance;
    private static string serverPath = "http://localhost:3000/api/";
    private WWWForm forms = null;

    ///<summary>Get a reference to the Global Data object. There exists only one Global Data object and it lasts the entire duration of the application.</summary>
    ///<returns>Returns a reference to the Global Data object, if it doesn't exist already it will be created</returns>
    public static DataStoring Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new DataStoring();
        }
        return instance;
      }
    }

    ///<summary>Stores loginInfo</summary>
    ///<param name="loginData">Data returned from server</param>
    public LoginContainer SetLoginData
    {
      set { loginInfo = value; }
    }

    ///<summary>Check if the user is logged in</summary>
    ///<returns>Returns bool true if logged in, false if not</returns>
    public bool IsLoggedIn
    {
      get
      {
        if (loginInfo != null)
        {
          return true;
        }
        return false;
      }
    }

    ///<summary>Gets user's access token</summary>
    ///<returns>Returns user's access token</returns>
    public string GetAccessToken
    { 
      get { return loginInfo.id; }
    }

    ///<summary>Get's user's ID</summary>
    ///<returns>Returns int of UserID</returns>
    public int GetUserID
    {
      get { return loginInfo.userId; }
    }

    ///<summary>Gets user's username</summary>
    ///<returns>Returns string of username</returns>
    public string GetUsername
    {
      get { return loginInfo.username; }
    }

    ///<summary>Fully erases currently logged in user (useful for logging out)</summary>
    public void EraseUserLog()
    {
      if (IsLoggedIn)
      {
        loginInfo.userId = -1;
        loginInfo.id = null;
        loginInfo.username = null;
      }
      loginInfo = null;
    }

    /// <summary>URL path to server</summary>
    /// <param>String to replace current path with</param>
    /// <returns>Returns string of server path</returns>
    public string ServerPath
    {
      get { return serverPath; }
      set { serverPath = value; }
    }

    /// <summary>Globally stored forms to send to server</summary>
    /// <param>Form to replace current form with</param>
    /// <returns>Returns WWWForm of currently stored forms</returns>
    public WWWForm Forms
    {
      get { return forms; }
      set { forms = value; }
    }

    /// <summary>Globally stored reception call from server</summary>
    /// <param>String to replace current response with</param>
    /// <returns>Returns string of currently stored response</returns>
    public string Recieving
    {
      get { return dataRecieved.response; }
      set { dataRecieved.response = value; }
    }

    /// <summary>Globally stored gameID call from server, unique to each project</summary>
    /// <param>String to replace current ID with</param>
    /// <returns>Returns string of currently stored ID</returns>
    public string GameId
    {
      get { return sessionContainer.GameID; }
      set { sessionContainer.GameID = value; }
    }

    /// <summary>Globally stored unique session ID call from server, updated each time the game is started</summary>
    /// <param>String to replace current ID with</param>
    /// <returns>Returns string of currently stored ID</returns>
    public string SessionId
    {
      get { return sessionContainer.SessionID; }
      set { sessionContainer.SessionID = value; }
    }

    /// <summary>Globally stored unique eventID call from server, updated for each event per session</summary>
    /// <param>String to replace current ID with</param>
    /// <returns>Returns string of currently stored ID</returns>
    public string EventId
    {
      get { return sessionContainer.EventID; }
      set { sessionContainer.EventID = value; }
    }

    public Dictionary<string, string> ServerDataDictionary
    {
      get { return dataDictionary; }
      set { dataDictionary = value; }
    }

    public GameContainer Game
    {
      get { return gameContainer; }
      set { gameContainer = value; }
    }

    public GameSessionContainer Session
    {
      get { return gameSessionContainer; }
      set { gameSessionContainer = value; }
    }

    public EventSessionContainer Event
    {
      get { return eventSeessionContainer; }
      set { eventSeessionContainer = value; }
    }

    /////////////////////////////////////////////////DataReceptionContainer functions///////////////////////////////////////////////////////
    /// <summary>Stores data recieved from server</summary>
    public DataReceptionContainer DataContainer
    {
      get { return dataContainer; }
      set { dataContainer = value; }
    }

    /// <summary>Checks if DataContainer has anything useful or not</summary>
    public bool DataContainerPresent
    {
      get
      {
        if (dataContainer != null)
        {
          return true;
        }
        return false;
      }
    }

    /// <summary>Stores and gives the stuff inside the dataContainer</summary>
    public string DataContainerStuff
    {
      get { return dataContainer.stuff; }
      set { dataContainer.stuff = value; }
    }

    /// <summary>Adds more stuff to the stuff in the DataContainer</summary>
    /// <param name="moreStuff">String of stuff to be appended to DataContainer stuff</param>
    public void AddToDataContainerStuff(string moreStuff)
    {
      dataContainer.stuff += moreStuff;
    }

    /// <summary>Converts content of dataContainer to and from JSON</summary>
    public string DataConvertJSON
    {
      get { return JsonUtility.FromJson<string>(dataContainer.stuff); }
      set { dataContainer.stuff = JsonUtility.FromJson<string>(value); }
    }

    public bool CheckGameId
    {
      get { if (Game.id == -1 || Game.id == -2) return false; return true; }
    }

    public bool CheckSessionId
    {
      get { if (!CheckGameId || Session.id == -1 || Session.id == -2) return false; return true; }
    }

    public bool CheckEventId
    {
      get { if (!CheckSessionId || Event.id == -1 || Event.id == -2) return false; return true; }
    }
  }
}
