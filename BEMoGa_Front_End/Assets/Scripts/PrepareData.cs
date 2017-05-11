using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Prepares data for transfer
/// </summary>
public class PrepareData : MonoBehaviour {

  //Locally stored variable
  private WWWForm forms = null;

  /// <summary>
  /// Get/Set function for forms, in case you would want to manually set it or see what it contains
  /// </summary>
  /// <param>Form to replace it with</param>
  /// <returns>Returns WWWForms data of forms</returns>
  public WWWForm Forms
  {
    get { return forms; }
    set { forms = value; }
  }

  void Awake ()
  {
    forms = new WWWForm();
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

  public void clearData()
  {
    forms = null;
  }
}
