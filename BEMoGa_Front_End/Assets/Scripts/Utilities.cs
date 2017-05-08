using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Various containers and helpers for data transmissions and receptions.
 * May need to be updated for each project. Below is merely a suggestion.
 */ 
namespace Utilities{

  /**
    * Container for json error object
    */
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

  /**
    * Container for json login object
    */
  [System.Serializable]
  public class LoginContainer
  {
    //Contains token key
    public string id = "Null";

    //
    public int ttl = -1;

    //Date the message was created, format {yyyy-mm-ddThh:mm:ss.fffZ}
    public string created = "Null";

    //Users user id
    public int userId = -1;
  }

  public class JsonHelper
  {
    /**
    * Converts json array into object array
    * @author ffleurey
    * @see https://forum.unity3d.com/threads/how-to-load-an-array-with-jsonutility.375735/
    * @param json to turn into array
    * @returns array of object type
    */
    public static T[] getJsonArray<T>(string json)
    {
      string newJson = "{ \"array\": " + json + "}";
      Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
      return wrapper.array;
    }

    //serializable wrapper class for getJsonArray function
    [System.Serializable]
    private class Wrapper<T>
    {
      public T[] array;
    }

    /**
    * Quick hack for removing object around error object
    * @param json to get subobject from
    */
    public static string GetSubObject(string json)
    {
      //Remove "{ "error": " at the start of json
      json = json.Remove(0, 9);

      //Remove "}" at end of json
      json = json.Remove(json.Length - 1);

      return json;
    }
  }
}
