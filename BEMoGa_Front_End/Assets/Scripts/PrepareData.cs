using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;


public class PrepareData : MonoBehaviour {

  private WWWForm forms;

  public WWWForm Forms
  {
    get { return forms; }
    set { forms = value; }
  }

  public void addField(string field, string value)
  {
    forms.AddField(field, value);
  }

  public void addField(string field, int value)
  {
    forms.AddField(field, value);
  }

  public void addField(string field, float value)
  {
    forms.AddField(field, value.ToString());
  }

  public void addField(string field, double value)
  {
    forms.AddField(field, value.ToString());
  }

  public void addField(string field, bool value)
  {
    forms.AddField(field, value.ToString());
  }
  
  public void transmitData(string URI)
  {
    UnityWebRequest www = UnityWebRequest.Post(URI, forms);
    SendData send = new SendData();
    StartCoroutine(send.RequestDataTransfer(www));
  }
}
