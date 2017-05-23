using System;
using UnityEngine;

namespace InfluxBemoga
{
    /// <summary>
    /// Class where storage of the influx data is located.
    /// The class use default localhost server url
    /// Have to set the name of the influx database, username and password here.
    /// Can be done in runtime with set methods.
    /// </summary>
    public class InfluxData
    {
        private static InfluxData instance;

        // specific data for onecity. Consider moving to config file for other projects
        // This data need to change to be able to work on your specific project. Consider Influx to have an
        // instance of Influx data. And all of these options be available in there. 
        // And on creation of Influx object, serverurl and database name is required. 

        private string _serverURL = "http://localhost:8086";                    // URL of the server, defaults to localhost
        private string _username  = null;//"writer";                           // username for access the database
        private string _password  = null;//"1234pass";                         // password for access the database
        private string _db        = null;//"test_db";                          // Name of the database


        ///<summary>Get a reference to the Global Data object. There exists only one Global Data object and it lasts the entire duration of the application.</summary>
        ///<returns>Returns a reference to the Global Data object, if it doesn't exist already it will be created</returns>
        public static InfluxData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfluxData();
                    instance.setConfig();
                }
                return instance;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns> Returns a string that contains the server url </returns>
        public string getServerURL()
        {
            return this._serverURL;
        }

        /// <summary>
        /// Set the server url to parameter
        /// </summary>
        /// <param name="url"> Example of a server url: http://localhost:8086 </param>
        public void setServerURL(string url)
        {
             if (String.IsNullOrEmpty(url))
                 throw new ArgumentException("Provide a valid url which is not NULL or empty");
             this._serverURL = url;
        }

        /// <summary>
        /// Gives the query url with username and password if both exist.
        /// Else it will give the standard query url with no username and password
        /// </summary>
        /// <returns>Returns a string that contains query url </returns>
        public string getQueryURl()
        {
            if (hasUserAndPass())
                return this._serverURL + "/query?u="+_username+"&p="+_password;

            return this._serverURL + "/query";              
        }

        /// <summary>
        /// Gives the write url with username and password if both exist.
        /// Else it will give the standard write url with no username and password
        /// Throws argument exception if the database name is null or empty. 
        /// </summary>
        /// <returns>Returns a string that contains write url</returns>
        public string getWriteURl()
        {
            if (String.IsNullOrEmpty(this._db))
                throw new ArgumentException("Database name is required when using write. Set it with the setNameDB method");

            if (hasUserAndPass())
                return this._serverURL+"/write?db="+_db + "&u="+_username+"&p="+_password;

            return this._serverURL + "/write?db=" + _db;
        }

        /// <summary>
        /// Set the name of the database to the parameter string.
        /// </summary>
        /// <param name = "db"> Name of the database. String object </param>
        public void setNameDB(string db)
        {
            if (String.IsNullOrEmpty(db))
                throw new ArgumentException("Provide a valid database name not NULL or empty");
            this._db = db;
        }

        /// <summary>
        /// Gives the database name in form of string.
        /// </summary>
        /// <returns> String that contains the database name </returns>
        public string getNameDB()
        {
            if (String.IsNullOrEmpty(this._db))
                throw new ArgumentException("Provide a valid database name not NULL or empty");
            return this._db;
        }

        /// <summary>
        /// Give the username to be used for authenticate to a influx database
        /// </summary>
        /// <returns>Returns the string of the username. 
        /// if the username is empty or null it will return null 
        /// </returns>
        public string getUsername()
        {
            if (String.IsNullOrEmpty(this._username))
                return null;
            return this._username;
        }

        /// <summary>
        /// Set the username to the parameter.
        /// </summary>
        /// <param name="username">String object</param>
        public void setUsername(string username)
        {
            this._username = username;
        }

        /// <summary>
        /// Get the password to be used for authentication to a influx database
        /// </summary>
        /// <returns> String that contains the password.
        /// Null if does not exist or is empty
        /// </returns>
        public string getPassword()
        {
            if (String.IsNullOrEmpty(this._password))
                return null;
            return this._password;
        }

        /// <summary>
        /// Set password to the parameter
        /// </summary>
        /// <param name="password">String Object</param>
        public void setPassword(string password)
        {
            this._password = password;
        }

        /// <summary>
        /// Gives you all the information about the influx data that is currently being used.
        /// </summary>
        /// <returns> Returns a string</returns>
        public void BemogaPrint()
        {
            Debug.Log("database: " + (String.IsNullOrEmpty(this._db) ? "null" : this._db));
            Debug.Log("serverURL: " + (String.IsNullOrEmpty(this._serverURL)? "null" : this._serverURL));
            Debug.Log("username: "  + (String.IsNullOrEmpty(this._username) ? "null" : this._username));
            Debug.Log("password: "  + (String.IsNullOrEmpty(this._password) ? "null" : this._password));
            Debug.Log("writeURL: "  + (String.IsNullOrEmpty(this._db) ? "null because database is null":this.getWriteURl()));
            Debug.Log("queryURL: "  + (String.IsNullOrEmpty(this.getQueryURl()) ? "null" : this.getQueryURl()));
           
        }

        /// <summary>
        /// Private method to check if username and password is being used
        /// </summary>
        /// <returns>Returns true or false</returns>
        private bool hasUserAndPass()
        {
            if (String.IsNullOrEmpty(this._username) || String.IsNullOrEmpty(this._password))
                return false;

            return true;
        }

        /// <summary>
        /// Set the config values of they are not empty or null
        /// </summary>
        private void  setConfig()
        {
            if(!String.IsNullOrEmpty(Config.serverURL))
            {
                this._serverURL = Config.serverURL;
            }

            if (!String.IsNullOrEmpty(Config.dbname))
            {
                this._db = Config.dbname;
            }

            if (!String.IsNullOrEmpty(Config.username))
            {
                this._username = Config.username;
            }

            if (!String.IsNullOrEmpty(Config.password))
            {
                this._password = Config.password;
            }
        }
    } 
}
