using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;
using InfluxBemoga;

/// <summary>
/// Influx script takes care of the transfer od data. It uses the
/// InfluxData class to get data as username, password.
/// This is a lightweight wrapper.
/// Covering all the possible query and write functionality
/// for influx db will not be covered. Only the basic forms
/// </summary>
public class Influx
{

    /// <summary>
    /// 
    /// </summary>
    public Influx()
    {

    }

    /// <summary>
    ///  Writes a point upon creation
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="point"></param>
    public Influx(MonoBehaviour mono, Point point)
    {
        writeMeasurment(mono, point);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="query"></param>
    public void getQuery(MonoBehaviour mono, string query)
    {
        WWWForm form = new WWWForm();
        form.AddField("db", InfluxData.Instance.getNameDB());
        form.AddField("q", query);

        var url = InfluxData.Instance.getQueryURl();

        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }
    /* ------------------Remove when time is close.......................................
    public void test(MonoBehaviour mono)
    {
        WWWForm form = new WWWForm();
        string url = InfluxData.Instance.getQueryURl();
        Debug.Log(url);
        form.AddField("db", InfluxData.Instance.getNameDB());
        form.AddField("q", "show series");
        SendData sd = new SendData();
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    public void test1(MonoBehaviour mono)
    {
 
        
            WWWForm form = new WWWForm();
            string url = InfluxData.Instance.getWriteURl();
            Debug.Log(url);
            string data = "cpu_load_short,host=server01,region=us-west value=0.64 1434055562000000000";
            SendData sd = new SendData();
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        mono.StartCoroutine(sd.httpRequest(url, bytes));
        

    }
        .....................................................................
    */
    /// <summary>
    /// Request a query with WWWform
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="form"></param>
    public void getQuery(MonoBehaviour mono, WWWForm form)
    {
        var url = InfluxData.Instance.getQueryURl();
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
        form.AddField("db", InfluxData.Instance.getNameDB());
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
        var url = InfluxData.Instance.getWriteURl();

        SendData sd = new SendData();
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="p"></param>
    public void writeMeasurment(MonoBehaviour mono, Point p)
    {
        var url = InfluxData.Instance.getWriteURl();
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
    public void writeMeasurment(MonoBehaviour mono, Point p, string url)
    {
        SendData sd = new SendData();
        var bytes = p.toBinary();
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }
}
