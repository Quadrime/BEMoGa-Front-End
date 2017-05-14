using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {

  // Use this for initialization
  void Start () {

  }
	
	// Update is called once per frame
  // Update is used here to make sure everything is created and sent to the server
	void Update () {

    //Gets the gameId from the server. The game name itself must be created manually on the server, one per project. This if is usually called on void Start() on the first scene.
    if (DataStoring.Instance.Game.id == "-1")                                             //Makes sure its only called once.
    {
      DataStoring.Instance.Game.id = "-2";                                                //Sets the id to a temp value while it retrieves the actual id.
      SendData send = new SendData();                                                     //Sets up the SendData instance.
      send.GetGameID(GetComponent<MonoBehaviour>(), "exampleGame");                       //Unique function only to be used by this if.
      Debug.Log("Sent game Id request");                                                  //Makes sure the developer knows the request has been sent.
    }

    //Creates a new sessionId on the server and retrieves this. This is usally called when the game starts/ when a user logs in (not yet implemented).
    else if (DataStoring.Instance.CheckGameId && DataStoring.Instance.Session.id == -1)   //Makes sure its only called once. Set DataStoring.Instance.Session.id to -1 and call this again to create a new session (usually when the game restarts/ when another user logs in)
    {
      Debug.Log("GameId:" + DataStoring.Instance.Game.id);                                //Provides developer with the gameId, so that they may double check if it corrolates with the server
      DataStoring.Instance.Session.id = -2;                                               //Sets the id to a temp value while it retrieves the actual id.
      SendData send = new SendData();                                                     //Sets up the SendData instance.
      send.SendSessionData(GetComponent<MonoBehaviour>());                                //Unique function only to be used by this if.
      Debug.Log("Sent sessionData");                                                      //Makes sure the developer knows the request has been sent.
    }

    //Creates only one event, remove (&& DataStoring.Instance.Event.id == -1) and (DataStoring.Instance.Event.id = -2;) to create several events
    else if (DataStoring.Instance.CheckSessionId && DataStoring.Instance.Event.id == -1)  //Makes sure its only called once. Remove (&& DataStoring.Instance.Event.id == -1) and call this again to create a new event (usually when the user interacts with something)
    {
      Debug.Log("SessionId:" + DataStoring.Instance.Session.id);                          //Provides developer with the gameId, so that they may double check if it corrolates with the server
      DataStoring.Instance.Event.id = -2;                                                 //Sets the id to a temp value while it retrieves the actual id. Remove to create multiple events.
      SendData send = new SendData();                                                     //Sets up the SendData instance.
      send.SendEventData(GetComponent<MonoBehaviour>(), "EVENTNAME");                     //Unique function only to be used by this if.
      Debug.Log("Sent eventData");                                                        //Makes sure the developer knows the request has been sent.
    }

    //Sends a value to the server
    else if (DataStoring.Instance.CheckEventId)                                           //Makes sure its only called after gameId, sessionId and eventId have been set, as it relies on these to succeed
    {
      Debug.Log("EventId:" + DataStoring.Instance.Event.id);                              //Provides developer with the gameId, so that they may double check if it corrolates with the server
      SendData send = new SendData();                                                     //Sets up the SendData instance.
      send.SendValueData(GetComponent<MonoBehaviour>(), "VALUENAME", 34.4f);              //Unique function only to be used by this if.
      Debug.Log("Sent valueData");                                                        //Makes sure the developer knows the request has been sent.
    }

  }

}
