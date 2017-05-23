using UnityEngine;
using InfluxBemoga;

/// <summary>
/// An example class holding arbitrary values
/// </summary>
public class dummy
{
    //BEMoGa supports strings, floats, bools and ints
    public string name = null;
    public float [] values = null;
    public bool isFemale = false;
    public int age = 0;

    /// <summary>
    /// Creates random values
    /// </summary>
    public dummy()
    {
        System.Random rnd = new System.Random();

        name = "dummy-" + rnd.Next(1, 6);
        values = new float[2];
       
        var valueone = 1.0 + rnd.NextDouble() * (99.9-1.0);
        values[0] = (float)valueone;

        var valueTwo = 1.0 + rnd.NextDouble() * (242.2 - 60.0);
        values[1] = (float)valueTwo;

        age = rnd.Next(18, 88);

        var f = rnd.Next(1, 4);
        if(f >= 3 )
        {
            isFemale = false;
        }

        else
        {
            isFemale = true;
        }
    }
}

/// <summary>
/// An arbitrary class showcasing how to use BEMoGa
/// </summary>
public class ExampleScript : MonoBehaviour
{

    // Use this for initialization
    Influx influx = new Influx();
    float period = 0.0f;

    /// <summary>
    /// Simply used show useful Influx and InfluxData functions.
    /// Also calls the ExampleWriter
    /// </summary>
    void Start()
    {
        //Prints every bit of InfluxData currently stored (i.e. url, username, password and database name)
        InfluxData.Instance.BemogaPrint();

        //Tells the influx class to print out everything it does to console
        influx.usePrint(true);

        ExampleWriter();
    }

    /// <summary>
    /// An example of a function that would be placed in an event class
    /// </summary>
    void ExampleWriter()
    {
        //Simply creates the dummy class containing arbitrary values
        dummy d = new dummy();

        ////Creates a new point with a preset measurement (database)
        Point p = new Point("dummy_metric");

        //At least one tag must be used, but you can use as many as you wish
        //Tags are used for easier querying in the Influx database
        p.addTag("AIname", d.name);
        p.addTag("host", "unity5.6");

        //At least one field must be used, but you can use as many as you wish
        //Fields are used to store values that you want to be stored in the Influx database
        p.addField("valueOne", d.values[0]);
        p.addField("valueTwo", d.values[1]);
        p.addField("age", d.age);
        p.addField("isFemale", d.isFemale);

        //Adding timestamps are optional, as the Influx database autogenerates them if they are not given during transmit
        p.addTimestamp(System.DateTime.UtcNow);

        //Transmits all stored data to the Influx database. Stored data are deleted automatically afterwards to avoid duplication
        influx.writeMeasurment(GetComponent<MonoBehaviour>(), p);
    }


}