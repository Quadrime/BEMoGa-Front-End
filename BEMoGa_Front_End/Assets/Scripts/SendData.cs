using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataStorage;
using Utilities;
using System;

///<summary>
/// Sends .cs data to server.
///</summary>
public class SendData {

  public void GetGameID(MonoBehaviour mono, string gameName)
  {
    string url = DataStoring.Instance.ServerPath + "games/getGameIdByName?pName=" + gameName;
    RecieveData rd = new RecieveData();
    rd.GetDataFromServer(url, mono);
  }

  public void SendSessionData(MonoBehaviour mono, string date = null, string sessionLocation = null, string sessionInfo = null)
  {
    string url = DataStoring.Instance.ServerPath + "games/" + DataStoring.Instance.Game.id.ToString() + "/gameSessions";
    PrepareData pd = new PrepareData();
    if (date != null)
    {
      pd.addField("date", date);
    }
    if (sessionLocation != null)
    {
      pd.addField("sessionLocation", sessionLocation);
    }
    if (sessionInfo != null)
    {
      pd.addField("sessionInfo", sessionInfo);
    }
    pd.transmitSessionData(url, mono);
  }

  public void SendEventData(MonoBehaviour mono, string name, string date = null, string description = null)
  {
    string url = DataStoring.Instance.ServerPath + "gameSessions/" + DataStoring.Instance.Session.id.ToString() + "/event";
    PrepareData pd = new PrepareData();
    pd.addField("name", name);
    if (date != null)
    {
      pd.addField("date", date);
    }
    if (description != null)
    {
      pd.addField("description", description);
    }
    pd.transmitEventData(url, mono);
  }

  public void SendValueData(MonoBehaviour mono, string name, float value)
  {
    string url = DataStoring.Instance.ServerPath + "events/" + DataStoring.Instance.Event.id.ToString() + "/value";
    PrepareData pd = new PrepareData();
    pd.addField("name", name);
    pd.addField("value", value);
    pd.transmitData(url, mono);
  }









  public IEnumerator RequestSessionDataTransfer(UnityWebRequest www)
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

    DataStoring.Instance.Session = JsonUtility.FromJson<GameSessionContainer>(www.downloadHandler.text);
  }

    /// <summary>Sends data to server and logs in user locally if succesful</summary>
    /// <param name="www">UnityWebRequest created by functions above</param>
    public IEnumerator RequestEventDataTransfer(UnityWebRequest www)
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

    DataStoring.Instance.Event = JsonUtility.FromJson<EventSessionContainer>(www.downloadHandler.text);
  }

  public IEnumerator RequestDataTransfer(UnityWebRequest www)
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

    Debug.Log(www.downloadHandler.text);
  }
}
