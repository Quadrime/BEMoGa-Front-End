using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {

  // Use this for initialization
  void Start () {
    string floatExField = "floatEx";
    float floatEx = 26.4f;
    PrepareData stuff = new PrepareData();
    stuff.addField(floatExField, floatEx);
    stuff.transmitData("sbfskjfnk", GetComponent<MonoBehaviour>());
    Debug.Log(DataStoring.Instance.Forms.ToString());

    RecieveData getStuff = new RecieveData();
    getStuff.GetDataFromServer("kdrjgbdkrjgnkjrn", GetComponent<MonoBehaviour>());
  }
	
	// Update is called once per frame
	void Update () {
    if (DataStoring.Instance.Game.id == -1)
    {
      DataStoring.Instance.Game.id = -2;
      SendData send = new SendData();
      send.GetGameID(GetComponent<MonoBehaviour>(), "GAMENAME");
    }
    else if (DataStoring.Instance.Game.id != -2 && DataStoring.Instance.Session.id == -1)
    {
      DataStoring.Instance.Game.id = -2;
      SendData send = new SendData();
      send.SendSessionData(GetComponent<MonoBehaviour>());
    }
    else if (DataStoring.Instance.Session.id != -2 && DataStoring.Instance.Event.id == -1)
    {
      DataStoring.Instance.Event.id = -2;
      SendData send = new SendData();
      send.SendValueData(GetComponent<MonoBehaviour>(), "VALUENAME", 34.4f);
    }

	}

}
