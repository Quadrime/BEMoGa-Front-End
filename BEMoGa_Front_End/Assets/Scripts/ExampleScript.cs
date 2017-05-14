using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }
	
	// Update is called once per frame
	void Update () {
    if (DataStoring.Instance.Game.id != "-2")
    {
      DataStoring.Instance.Game.id = "-2";
      SendData send = new SendData();
      send.GetGameID(GetComponent<MonoBehaviour>(), "exampleGame");
      Debug.Log("Sent game Id request");
    }
    else if (DataStoring.Instance.CheckGameId && DataStoring.Instance.Session.id != -2)
    {
      Debug.Log("GameId:" + DataStoring.Instance.Game.id);
      DataStoring.Instance.Session.id = -2;
      SendData send = new SendData();
      send.SendSessionData(GetComponent<MonoBehaviour>());
      Debug.Log("Sent sessionData");
    }
    
    else if (DataStoring.Instance.CheckSessionId && DataStoring.Instance.Event.id != -2)
    {
      Debug.Log("SessionId:" + DataStoring.Instance.Session.id);
      DataStoring.Instance.Event.id = -2;
      SendData send = new SendData();
      send.SendEventData(GetComponent<MonoBehaviour>(), "EVENTNAME");
      Debug.Log("Sent eventData");
    }
    else if (DataStoring.Instance.CheckEventId)
    {
      Debug.Log("EventId:" + DataStoring.Instance.Event.id);
      SendData send = new SendData();
      send.SendValueData(GetComponent<MonoBehaviour>(), "VALUENAME", 34.4f);
      Debug.Log("Sent valueData");
    }

  }

}
