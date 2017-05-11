using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour {
  string floatExField = "floatEx";
  float floatEx = 26.4f;

	// Use this for initialization
	void Start () {
    GameObject transmitter = GameObject.Find("DataTransmitter");
    if (transmitter != null)
    {
      PrepareData stuff = transmitter.GetComponent<PrepareData>();
      stuff.addField(floatExField, floatEx);
      Debug.Log(stuff.Forms);
    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
