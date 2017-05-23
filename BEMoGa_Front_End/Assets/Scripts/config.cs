using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Set the config values to your liking here
/// Consider moving this to an actualy JSON config
/// or other more standardized config structures
/// </summary>

static class Config
{
    // Keep as null, if you want to use default. A
    // All values can be set runtime.
    // But if you wannt, you can change these, 
    // and upon game start. It will set these values

    public static string dbname = "test_db";                       // REQUIRED: Default NULL. Need to be set before using influx, can be set runtime
    public static string serverURL = "http://localhost:8086";      // REQUIRED: Default: http://localhost:8086
    public static string username = "writer";                      // OPTIONAL: Default NULL
    public static string password = "1234pass";                    // OPTIONAL: Default NULL

    /* 
     * EXAMPLE how it can look
     * -----------------------------------
     * public static string dbname = "test_db"      
     * public static string serverURL ="http://localhost:8086";      
     * public static string username = "writer";      
     * public static string password = "1234Pass;      
     * 
     */


    /*
    Original:

    public static string dbname = null;         // REQUIRED: Default NULL. Need to be set before using influx, can be set runtime
    public static string serverURL = null;      // REQUIRED: Default: http://localhost:8086
    public static string username = null;       // OPTIONAL: Default NULL
    public static string password = null;       // OPTIONAL: Default NULL 

     */

}
