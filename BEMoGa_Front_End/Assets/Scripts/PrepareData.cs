using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using DataStorage;

/// <summary>
/// Prepares data for transfer
/// This is a lightweight wrapper.
/// Covering all the possible query and write functionality
/// for influx db will not be covered. Only the basic forms
/// </summary>
public class PrepareData
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="query"></param>
    public void getQuery(MonoBehaviour mono, string query)
    {
        WWWForm form = new WWWForm();
        form.AddField("db", DataStoring.Instance.getNameDB());
        form.AddField("q", query);

        var url = DataStoring.Instance.getQueryURl();

        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// Request a query with WWWform
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="form"></param>
    public void getQuery(MonoBehaviour mono, WWWForm form)
    {
        var url = DataStoring.Instance.getQueryURl();
        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="mono"></param>
   /// <param name="query"></param>
   /// <param name="url"></param>
    public void getQuery(MonoBehaviour mono, string query, string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("db", DataStoring.Instance.getNameDB());
        form.AddField("q", query);

        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="form"></param>
    /// <param name="url"></param>
    public void getQuery(MonoBehaviour mono, WWWForm form, string url)
    {
        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="data"></param>
    public void writeMeasurment(MonoBehaviour mono, string data)
    {
        var url = DataStoring.Instance.getWriteURl();

        SendData sd = new SendData();
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="p"></param>
    public void writeMeasurment(MonoBehaviour mono, point p)
    {
        var url = DataStoring.Instance.getWriteURl();
        SendData sd = new SendData();
        var bytes = p.toBinary();
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="data"></param>
    /// <param name="url"></param>
    public void writeMeasurment(MonoBehaviour mono, string data, string url)
    {
        SendData sd = new SendData();
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="p"></param>
    /// <param name="url"></param>
    public void writeMeasurment(MonoBehaviour mono, point p, string url)
    {
        SendData sd = new SendData();
        var bytes = p.toBinary();
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }
}
