using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Utilities;
using UnityEngine;
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
    influxUtil util = new influxUtil();

    /// <summary>
    /// Create an influx instance
    /// </summary>
    public Influx()
    {

    }

    /// <summary>
    /// Create a influx object and set the InfluxData upon creation aswell
    /// Remember these data will be saved and stored during the whole session.
    /// If you are doing multiple sends to different databases. Consider using the custom
    /// Url approach, and add your data in there. Currently support for multiple databases queries
    /// And writes are very limited.
    /// </summary>
    /// <param name="databaseName"></param>
    /// <param name="serverUrl"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public Influx(string databaseName, string serverUrl,string username = null, string password = null)
    {
        InfluxBemoga.InfluxData.Instance.setNameDB(databaseName);
        InfluxBemoga.InfluxData.Instance.setServerURL(serverUrl);
        InfluxBemoga.InfluxData.Instance.setUsername(username);
        InfluxBemoga.InfluxData.Instance.setPassword(password);
    }

    /// <summary>
    ///  Writes a point upon creation. It is necessary that the InfluxData has been set
    ///  before this in the config file or getInfluxData().<setFunctions()> 
    /// </summary>
    /// <param name="mono">Required form couroutine</param>
    /// <param name="point">Required for sending the data</param>
    public Influx(MonoBehaviour mono, Point point)
    {
        writeMeasurment(mono, point);
    }

    /// <summary>
    /// Set if you want to use print or no printing.
    /// Recommended to use during development and authentication test access
    /// Defaults to false on creation
    /// </summary>
    /// <param name="printSet">True if the user can print, false if you want to turn off print</param>
    public void usePrint(bool printSet)
    {
        if (util == null)
            util = new influxUtil();

        util.setUsePrint(printSet);
    }

    /// <summary>
    /// Get a query the default way, and the recommended way
    /// The URL get autp generated and the db get added to the fields.
    /// However it can only do a single query. If you want more advanced queries
    /// With use of epoch and the other alternatives, use the custom WWWform query
    /// Read more about queries for influx: https://docs.influxdata.com/influxdb/v1.2/guides/querying_data/
    /// </summary>
    /// <param name="mono">Required for Coroutine</param>
    /// <param name="query">Required for sending the query</param>
    public void getQuery(MonoBehaviour mono, string query)
    {
        WWWForm form = new WWWForm();
        form.AddField("db", InfluxData.Instance.getNameDB());
        form.AddField("q", query);
        var url = InfluxData.Instance.getQueryURl();
        var bytes = form.data;
        SendData sd = new SendData(util.isPrint());
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }



    /// <summary>
    /// Get a query. Default way bemoga support custom queries.
    /// The Url will be auto generated from InfluxData.
    /// The database need to be provided from the wwwform
    /// Example of the wwwform: addfield("q","\"SELECT * FROM measurements\""
    ///                wwwform: addfield("db", "mydb"); Note how you can add the db as wwwform
    /// Read more about queries for influx: https://docs.influxdata.com/influxdb/v1.2/guides/querying_data/
    /// </summary>
    /// <param name="mono">Required for Coroutine</param>
    /// <param name="form">Required for sending query and parameters</param>
    public void getQuery(MonoBehaviour mono, WWWForm form)
    {
        var url = InfluxData.Instance.getQueryURl();
        SendData sd = new SendData(util.isPrint());
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }


    /// <summary>
    /// Get a query with a custom URL.
    /// This means the url need to handle authentication if possible
    /// Also need to handle the database name if is not provided as wwwform.
    /// The WWWform handle the actual arguments and the query.
    /// Example of custom URL: http://localhost:8086/query?u=reader&p=pass1234
    /// Example of the wwwform: addfield("q","\"SELECT * FROM measurements\""
    ///                wwwform: addfield("db", "mydb"); Note how you can add the db as wwwform
    /// Read more about queries for influx: https://docs.influxdata.com/influxdb/v1.2/guides/querying_data/
    /// </summary>
    /// <param name="mono">Required for Coroutine</param>
    /// <param name="form">Required for sending query and database</param>
    /// <param name="custom_url">Required</param>
    public void getQuery(MonoBehaviour mono, WWWForm form, string custom_url)
    {
        SendData sd = new SendData(util.isPrint());
        var bytes = form.data;
        mono.StartCoroutine(sd.httpRequest(custom_url, bytes));
    }



    /// <summary>
    /// Write a measurement using a point. This is the dafault way of writing to an influx database
    /// This means the point object will generate the data string to be sent.
    /// Also means the url will be auto generated from InfluxData class. With the url,databasename, username, and password
    /// That was provided in the config file or set in runtime "ONCE" before this method was called.
    /// See more information at: https://docs.influxdata.com/influxdb/v1.2/guides/writing_data/ for writing data.
    /// </summary>
    /// <param name="mono">Required for Coroutine</param>
    /// <param name="point">Required for data transmission</param>
    public void writeMeasurment(MonoBehaviour mono, Point point)
    {
        var url = InfluxData.Instance.getWriteURl();
        var bytes = point.toBinary();
        SendData sd = new SendData(util.isPrint());
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// Write a measurment using a string of data instead of a point.
    /// This means the data string not AUTO built as the point would do.
    /// The URL is auto generated from data provided in the InfluxData from
    /// Config or set at runtime
    /// An example of data: "cpu_load_short,host=server02,region=us-west value=0.20 1534055562000000000"
    /// See more information at: https://docs.influxdata.com/influxdb/v1.2/guides/writing_data/ for writing data.
    /// </summary>
    /// <param name="mono">Required for coroutine</param>
    /// <param name="data">Required for sending data measurements,tagkey=tagvalue fieldkey=valuekey,fieldkey=valuekey timestamp</param>
    public void writeMeasurment(MonoBehaviour mono, string data)
    {
        var url = InfluxData.Instance.getWriteURl();
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        SendData sd = new SendData(util.isPrint());
        mono.StartCoroutine(sd.httpRequest(url, bytes));
    }

    /// <summary>
    /// Write a measurement using a string of data and custom url
    /// This means the data string not AUTO built as the point would do.
    /// The url is not auto generated URL from InfluxData either
    /// Note this also means the database MUST be available in the url
    /// Username and passoword also necessary if it is required to authenticate
    /// An example of data: "cpu_load_short,host=server02,region=us-west value=0.20 1534055562000000000"
    /// An example of url : http://localhost:8086/write?db=test_db&u=writer&p=pass1234
    /// See more information at: https://docs.influxdata.com/influxdb/v1.2/guides/writing_data/ for writing data.
    /// </summary>
    /// <param name="mono">Required for coroutine</param>
    /// <param name="data">Required for sending data. "measurements,tagkey=tagvalue,tagkey=value fieldkey=valuekey,fieldkey=valuekey timestamp"</param>
    /// <param name="custom_url">Required for sending to correct database and authenticate</param>
    public void writeMeasurment(MonoBehaviour mono, string data, string custom_url)
    {
        SendData sd = new SendData(util.isPrint());
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        mono.StartCoroutine(sd.httpRequest(custom_url, bytes));
    }

    /// <summary>
    /// Write a measurement with using a Point and custom url.
    /// This means the url is not auto generated URL from InfluxData.
    /// Note this also means the database MUST be available in the url
    /// Username and passoword also necessary if it is required to authenticate
    /// An example: http://localhost:8086/write?db=test_db&u=writer&p=pass1234
    /// See more information at https://docs.influxdata.com/influxdb/v1.2/guides/writing_data/
    /// </summary>
    /// <param name="mono"> Required for coroutine </param>
    /// <param name="point">Required for data transmission</param>
    /// <param name="custom_url"> Required for sending to correct database and authenticate</param>
    public void writeMeasurment(MonoBehaviour mono, Point point, string custom_url)
    {
        SendData sd = new SendData(util.isPrint());
        var bytes = point.toBinary();
        mono.StartCoroutine(sd.httpRequest(custom_url, bytes));
    }




    /// <summary>
    /// Return the instance of the InfluxData
    /// It is a singelton, but it is easier to get it from the Influx object
    /// Which is the main object to interact with.
    /// </summary>
    /// <returns>InfluxData Instance</returns>
    public InfluxData getInfluxData()
    {
        return InfluxBemoga.InfluxData.Instance;
    }
}
