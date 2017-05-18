using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Globalization;



public class point {

    /// <summary>
    /// REQUIRED
    /// InfluxDB accepts one measurement per point
    /// String
    /// </summary>
    private string _measurement;

    /// <summary>
    /// REQUIRED
    /// 
    /// </summary>
    private Dictionary<string, string> _fields;

    /// <summary>
    /// OPTIONAL
    /// All tag key-value pairs for the point.
    /// tags<string,string>
    /// </summary>
    private Dictionary<string, string> _tags;

    /// <summary>
    /// OPTIONAL 
    /// InfluxDB uses the server’s local nanosecond timestamp in UTC if the timestamp is not included with the point.
    /// </summary>
    private DateTime? _timestamp;



    /// <summary>
    /// Constructor
    /// </summary>
    public point(){
        _measurement = null;
        _fields = new Dictionary<string, string>();
        _tags = new Dictionary<string, string>();
        _timestamp = null;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="measurement"></param>
    public point(string measurement)
    {
         this._measurement = measurement;
        _fields = new Dictionary<string, string>();
        _tags = new Dictionary<string, string>();
        _timestamp = null;
    }

    /// <summary>
    /// Add a measurement
    /// </summary>
    /// <param name="measurement"></param>
    public void addMeasurement(string measurement)
    {
        this._measurement = measurement;
    }

    /// <summary>
    ///  Add a field with the correct syntax format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fieldKey"></param>
    /// <param name="value"></param>
    public void addField<T>(string fieldKey, T value)
    {
        if(value is int)
        {
            this._fields.Add(fieldKey, value.ToString() + "i");
            return;
        }

        if(value is string)
        {
            this._fields.Add(fieldKey, "\"" + value.ToString() + "\"");
            return;
        }

        this._fields.Add(fieldKey, value.ToString());
    }
    /// <summary>
    ///  Add a tag
    /// </summary>
    /// <param name="tagKey"></param>
    /// <param name="tagValue"></param>
    public void addTag(string tagKey, string tagValue)
    {
        this._tags.Add(tagKey, tagValue);
    }

    /// <summary>
    /// Add a timestamp, need to be of utc format
    /// </summary>
    /// <param name="time"></param>
    public void addTimestamp(DateTime? time)
    {
        this._timestamp = time;
    }

    /// <summary>
    /// Parse the point to binary data
    /// </summary>
    /// <returns></returns>
    public byte[] toBinary()
    {
        var data = buildLineProtocol();
        Debug.Log(data);
        var bytes = System.Text.Encoding.UTF8.GetBytes(data);
        return bytes;
    }


    public string getLineProtocol()
    {
        return buildLineProtocol();
    }
    
    /// <summary>
    ///  build the line protocol line from the point
    /// </summary>
    /// <returns></returns>
    private string buildLineProtocol()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(this._measurement);
       

        foreach (var entry in this._tags)
        {
            sb.Append(",");
            sb.Append(entry.Key);
            sb.Append("=");
            sb.Append(entry.Value);
        }

        var fieldDelim = ' ';

        foreach (var entry in this._fields)
        {
            sb.Append(fieldDelim);
            fieldDelim = ',';
            sb.Append(entry.Key);
            sb.Append("=");
            sb.Append(entry.Value);
        }

       

       if(this._timestamp != null)
       {
            sb.Append(" ");
            this._timestamp.ToString();
            sb.Append(formatTimestamp(this._timestamp.Value));
       }

        return sb.ToString();

    }


    /// <summary>
    /// Check if the format is valid type
    /// </summary>
    private void isValid()
    {
        if (String.IsNullOrEmpty(this._measurement))
            throw new ArgumentException("A measurement name is required");


        if (this._fields == null || this._fields.Count == 0)
            throw new ArgumentException("At least one field must is required");


        foreach (var f in this._fields)
            if (string.IsNullOrEmpty(f.Key)) throw new ArgumentException("Fields must have non-empty names");

        if (this._tags != null)
            foreach (var t in this._tags)
                if (string.IsNullOrEmpty(t.Key)) throw new ArgumentException("Tags must have non-empty names");

        if (this._timestamp != null && this._timestamp.Value.Kind != DateTimeKind.Utc)
            throw new ArgumentException("Timestamps must be specified as UTC");

    }

    /// <summary>
    /// Set correct time format
    /// </summary>
    /// <param name="utcTimestamp"></param>
    /// <returns></returns>
    private static string formatTimestamp(DateTime utcTimestamp)
    {
        var t = utcTimestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (t.Ticks * 100L).ToString(CultureInfo.InvariantCulture);
    }

}
