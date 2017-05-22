using UnityEngine;
using DataStorage;

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

public class ExampleScript : MonoBehaviour {

    // Use this for initialization
    PrepareData pd = new PrepareData();
    float period = 0.0f;

    void Start () {


        pd.test1(GetComponent<MonoBehaviour>());

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
        point p = new point("dummy_metric");
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
//PrepareData pr = new PrepareData();
//string data = "cpu_load_another,host=server02,region=us-east value=0.64 1434055562000000000";
//string test = "dummy_metric,AIname=dummy2,host=eskil valueOne=0.405,valueTwo=232.5,age=23i,isFemale=true, 2434354353554354";
//point p = new point("cpu_load_another");
//p.addTag("host", "triforce");
//p.addTag("region", "norway");
//p.addField<float>("value", 0.64f);
//p.addField<string>("secondValue", "test");
//System.DateTime? time = System.DateTime.UtcNow;
//p.addTimestamp(time);
//pr.writeMeasurment(GetComponent<MonoBehaviour>(), data);
//pr.getQuery(GetComponent<MonoBehaviour>(), "SELECT \"value\" FROM \"cpu_load_short\" WHERE \"region\"='us-west'");
//pr.writeMeasurment(GetComponent<MonoBehaviour>() , p);
//point a = new point("cpu_load_another");
//a.addTag("host", "triforce");
//a.addTag("region", "norway");
//a.addField<float>("value", 0.64f);
//a.addField<bool>("thirdvalue",true);
//a.addField("fourthvalue", 12);
//pr.writeMeasurment(GetComponent<MonoBehaviour>(),a);
