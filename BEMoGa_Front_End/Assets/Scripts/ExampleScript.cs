using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {
  string floatExField = "floatEx";
  float floatEx = 26.4f;

	// Use this for initialization
	void Start () {
    PrepareData stuff = new PrepareData();
    stuff.addField(floatExField, floatEx);
    stuff.transmitData("sbfskjfnk", GetComponent<MonoBehaviour>());
    Debug.Log(DataStoring.Instance.Forms.ToString());

    RecieveData getStuff = new RecieveData();
    getStuff.GetDataFromServer("kdrjgbdkrjgnkjrn", GetComponent<MonoBehaviour>());
  }
	
	// Update is called once per frame
	void Update () {
		
	}

}
