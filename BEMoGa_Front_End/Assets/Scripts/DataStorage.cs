using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DataStorage
{
  public class GlobalData
  {
    //Variables
    LoginContainer loginInfo = null;
    private static GlobalData instance;
    private static string serverPath = "http://localhost:3000/api/";
    private WWWForm forms = null;

    ///<summary>Get a reference to the Global Data object. There exists only one Global Data object and it lasts the entire duration of the application.</summary>
    ///<returns>Returns a reference to the Global Data object, if it doesn't exist already it will be created</returns>
    public static GlobalData Instance
    {
      get
      {
        if (instance == null)
        {
          instance = new GlobalData();
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
  }
}
