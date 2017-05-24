# BEMoGA
![alt text](http://prod3.imt.hig.no/BEMoGa/Front-End/raw/e250c0e3db40dd2532ee8baa6b18aa2218b757cd/bemogaLoga.png)

https://drive.google.com/file/d/0B52ZDxt0gL8jZnlrVG5VRlY3cVk/view?usp=sharing
BEMoGa is a back-end for educational mobile games to store metrics and in-game events about their game. BEMoGa make use of InfluxDB as their database back-end server and a simple lightweight api in unity to commuincate with the database. Additioanlly BEMoGa has been using grafana as the tool to visualize the data metrics from InfluxDB

Tools that BEMoGa requires or are recommended:
  * [InfluxDB](https://www.influxdata.com/) - Is required. Download the localhost if you have no access to already hosted InfluxDB.
  * [Grafana](http://staging.grafana.org/) - Is optional. It is used for visualization for influxDB data instead of command line. Other alternative can of course be used.
  * [Unity](https://madewith.unity.com/) - Is required. All our code to interact with an InfluxDB is done in unity. 

# Code Example
First you can set the Config variables. They are default null, except the server url which default to the localhost of InfluxDB. A example of a config setup is given below:
```csharp
static class Config
{
        public static string dbname = "test_db"; 
        public static string serverURL = "http://localhost:8086";
        public static string username = "writer";
        public static string password = "1234pass"; 
}
```
So when the config is setup. You can use the InfluxDB on Unity very simply by adding the BEMoGa script folder to your project. After that you can write to your influxDB. Remember InfluxDB will always require DB a measurements, and atleast one field. Go to influxDB documentation on their own website to read more about this.
```csharp
    Influx influx = new Influx();
    public void Start()
    {
        // print the InfluxData: As serverURL, host, password and database name.
        InfluxData.Instance.BemogaPrint();

        //Tells the influx class to print out everything it does to console
        influx.usePrint(true);

        //Creates a new point with a preset measurement (database)
        Point p = new Point("dummy_metric");
        p.addTag("name","BEMoGa );
        p.addField("Downloads", downloads.gitlab); //this does not actually exist...
        p.addTimestamp(System.DateTime.UtcNow);
        influx.writeMeasurment(GetComponent<MonoBehaviour>(), p);
    }
```

And to query...
```csharp
    influx.getQuery(GetComponent<MonoBehaviour>(),"SELECT * FROM dummy_metric",true);
```
Result will look like this for example:
```csharp
{
    "results": [
        {
            "statement_id": 0,
            "series": [
                {
                    "name": "dummy_metric",
                    "columns": [
                        "time",
                        "AIname",
                        "age",
                        "host",
                        "isFemale",
                        "valueOne",
                        "valueTwo"
                    ],
                    "values": [
                        [
                            "2017-05-24T10:38:53.1273651Z",
                            "dummy-4",
                            45,
                            "unity5.6",
                            true,
                            28.17514,
                            76.49101
                        ],
                        [
                            "2017-05-24T10:54:31.7643607Z",
                            "dummy-5",
                            81,
                            "unity5.6",
                            true,
                            52.17833,
                            105.8909
                        ]
                    ]
                }
            ]
        }
    ]
}
```