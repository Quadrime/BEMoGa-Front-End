using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace DataStorage
{
  public class GlobalData
  {

    LoginContainer loginInfo = null;
    private static GlobalData instance;

    /**
    * Get a reference to the Global Data object.
    * There exists only one Global Data object and it lasts the entire duration of the application.
    * @returns a reference to the Global Data object, if it doesn't exist already it will be created.
    */
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

    /**
    * Stores loginInfo
    * @param loginData = data returned from server
    */
    public void SetLoginData(LoginContainer loginData)
    {
      loginInfo = loginData;
    }

    /**
    * Check if the user is logged in
    * @returns True if logged in, false if not 
    */
    public bool IsLoggedIn()
    {
      if (loginInfo != null)
      {
        return true;
      }
      return false;
    }

    /**
    * Gets user's access token
    * @returns Access token, if not logged in returns null
    */
    public string GetAccessToken()
    {
      if (IsLoggedIn())
      {
        return loginInfo.id;
      }

      return null;
    }

    /**
    * Gets user's ID
    * @returns UserID, if not logged in returns -1
    */
    public int GetUserID()
    {
      if (IsLoggedIn())
      {
        return loginInfo.userId;
      }

      return -1;
    }

    /**
     * Fully erases currently logged in user (useful for logging out)
     */
    public void EraseUserLog()
    {
      if (IsLoggedIn())
      {
        loginInfo.id = null;
        loginInfo.userId = -1;
        loginInfo.created = null;
      }
      loginInfo = null;
    }
  }
}
