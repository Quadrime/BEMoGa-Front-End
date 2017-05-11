using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {
  string floatExField = "floatEx";
  float floatEx = 26.4f;

	// Use this for initialization
	void Start () {
    PrepareData stuff = new PrepareData();
    stuff.addField(floatExField, floatEx);
    Debug.Log(GlobalData.Instance.Forms.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
