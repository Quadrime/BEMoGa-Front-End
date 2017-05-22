using UnityEngine;
using InfluxBemoga;

public class dummy
{
    public string name = null;
    public float [] values = null;
    public bool isFemale = false;
    public int age = 0;

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

public class ExampleScript : MonoBehaviour
{

    // Use this for initialization
    Influx pd = new Influx();
    float period = 0.0f;

    void Start()
    {


        // pd.test1(GetComponent<MonoBehaviour>());

    }

    // Update is called once per frame
    // Update is used here to make sure everything is created and sent to the server
    void Update()
    {
        //if(period > 2.0f)
        //{
        //    createAndDominate();
        //    period = 0;
        //}

        //period += UnityEngine.Time.deltaTime;

    }

    void createAndDominate()
    {
        dummy d = new dummy();
        Point p = new Point("dummy_metric");
        p.addTag("AIname", d.name);
        p.addTag("host", "unity5.6");
        p.addField("valueOne", d.values[0]);
        p.addField("valueTwo", d.values[1]);
        p.addField("age", d.age);
        p.addField("isFemale", d.isFemale);
        p.addTimestamp(System.DateTime.UtcNow);
        pd.writeMeasurment(GetComponent<MonoBehaviour>(), p);
    }


}