using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;


namespace InfluxBemoga
{
    public class Point
    {

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
        /// Creates a point with none of the values set
        /// </summary>
        public Point()
        {
            _measurement = null;
            _fields = new Dictionary<string, string>();
            _tags = new Dictionary<string, string>();
            _timestamp = null;
        }

        /// <summary>
        /// Creates a Point with measurement already set
        /// </summary>
        /// <param name="measurement">String measurment, can not be null  or empty</param>
        public Point(string measurement)
        {
            this._measurement = measurement;
            _fields = new Dictionary<string, string>();
            _tags = new Dictionary<string, string>();
            _timestamp = null;
        }

        /// <summary>
        /// Add a measurement
        /// </summary>
        /// <param name="measurement">String measurment, can not be null  or empty</param>
        public void addMeasurement(string measurement)
        {
            this._measurement = measurement;
        }

        /// <summary>
        ///  Add a field with the correct syntax format
        /// </summary>
        /// <typeparam name="T"> Generic</typeparam>
        /// <param name="fieldKey">String key, can not be empty or null</param>
        /// <param name="value"> T Value, can be int, string, float or bool</param>
        public void addField<T>(string fieldKey, T value)
        {
            if (value is int)
            {
                this._fields.Add(fieldKey, value.ToString() + "i");
                return;
            }

            if (value is string)
            {
                this._fields.Add(fieldKey, "\"" + value.ToString() + "\"");
                return;
            }

            this._fields.Add(fieldKey, value.ToString());
        }
        /// <summary>
        ///  Add a tag to the point
        /// </summary>
        /// <param name="tagKey">String, should not be null</param>
        /// <param name="tagValue">string, the value</param>
        public void addTag(string tagKey, string tagValue)
        {
            this._tags.Add(tagKey, tagValue);
        }

        /// <summary>
        /// Add a timestamp, need to be of utc format
        /// </summary>
        /// <param name="time">UTC date time, required. </param>
        public void addTimestamp(DateTime? time)
        {
            this._timestamp = time;
        }

        /// <summary>
        /// Convert a lineProtocol to a string
        /// </summary>
        /// <returns> Byte array for use in transmission </returns>
        public byte[] toBinary()
        {
            var data = buildLineProtocol();
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            return bytes;
        }

        /// <summary>
        /// Get a lineprotocol from a point
        /// </summary>
        /// <returns> Returns a point as a string </returns>
        public string getLineProtocol()
        {
            return buildLineProtocol();
        }

        /// <summary>
        ///  build the line protocol line from the point.
        /// </summary>
        /// <returns>Returns the lineprotocol that is supposed to look for data transmission as a line/string </returns>
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



            if (this._timestamp != null)
            {
                sb.Append(" ");
                this._timestamp.ToString();
                sb.Append(formatTimestamp(this._timestamp.Value));
            }

            return sb.ToString();

        }


        /// <summary>
        /// Check if the format is valid type
        /// Throws an argument exception on fail
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
        /// Set the timeformat to utc
        /// </summary>
        /// <param name="utcTimestamp"> Required that the datetime is of utcTimeStamp</param>
        /// <returns></returns>
        private static string formatTimestamp(DateTime utcTimestamp)
        {
            var t = utcTimestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (t.Ticks * 100L).ToString(CultureInfo.InvariantCulture);
        }
    }
}
