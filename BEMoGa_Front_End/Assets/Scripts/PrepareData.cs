using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Prepares data for transfer
/// </summary>
public class PrepareData {

  /// <summary>Creates new form when first created</summary>
  public PrepareData ()
  {
    DataStoring.Instance.Forms = new WWWForm();
  }

  /// <summary>Adds a string value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">String value to be sent</param>
  public void addField(string field, string value)
  {
    DataStoring.Instance.Forms.AddField(field, value);
  }

  /// <summary>Adds an int value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">Integer value to be sent</param>
  public void addField(string field, int value)
  {
    DataStoring.Instance.Forms.AddField(field, value);
  }

  /// <summary>Adds a float value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">Float value to be sent</param>
  public void addField(string field, float value)
  {
    DataStoring.Instance.Forms.AddField(field, value.ToString());
  }

  /// <summary>Adds a double value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">Double value to be sent</param>
  public void addField(string field, double value)
  {
    DataStoring.Instance.Forms.AddField(field, value.ToString());
  }

  /// <summary>Adds a bool value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">Boolean value to be sent</param>
  public void addField(string field, bool value)
  {
    DataStoring.Instance.Forms.AddField(field, value.ToString());
  }

  /// <summary>Adds a bool value and string field to the form to be sent to the server</summary>
  /// <param name="field">String name of database table field</param>
  /// <param name="value">DateTime value to be sent</param>
  public void addField(string field, System.DateTime value)
  {
    DataStoring.Instance.Forms.AddField(field, value.ToString("yyyy-MM-dd HH:mm:ss"));
  }

  /// <summary>Transmits all data stored in forms to the server</summary>
  /// <param name="URI">URL adress of database table/API</param>
  /// <param name="monoBehaviour">MonoBehaviour of parent class to be used for Coroutine</param>
  public void transmitData(string URI, MonoBehaviour monoBehaviour)
  {
    if (DataStoring.Instance.Forms != null)
    {
      UnityWebRequest www = UnityWebRequest.Post(URI, DataStoring.Instance.Forms);
      SendData send = new SendData();
      Debug.Log("Transfer of data started.");
      monoBehaviour.StartCoroutine(send.RequestDataTransfer(www));
      clearData();
    }
    else
    {
      Debug.Log("Error: No data to transfer");
    }
  }

  public void transmitEventData(string URI, MonoBehaviour monoBehaviour)
  {
    if (DataStoring.Instance.Forms != null)
    {
      UnityWebRequest www = UnityWebRequest.Post(URI, DataStoring.Instance.Forms);
      SendData send = new SendData();
      Debug.Log("Transfer of data started.");
      monoBehaviour.StartCoroutine(send.RequestEventDataTransfer(www));
      clearData();
    }
    else
    {
      Debug.Log("Error: No data to transfer");
    }
  }

  public void transmitSessionData(string URI, MonoBehaviour monoBehaviour)
  {
    if (DataStoring.Instance.Forms != null)
    {
      UnityWebRequest www = UnityWebRequest.Post(URI, DataStoring.Instance.Forms);
      SendData send = new SendData();
      Debug.Log("Transfer of data started.");
      monoBehaviour.StartCoroutine(send.RequestSessionDataTransfer(www));
      clearData();
    }
    else
    {
      Debug.Log("Error: No data to transfer");
    }
  }

  public void clearData()
  {
    DataStoring.Instance.Forms = null;
    DataStoring.Instance.Forms = new WWWForm();
  }
}
