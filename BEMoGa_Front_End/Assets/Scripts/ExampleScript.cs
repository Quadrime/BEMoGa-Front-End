using UnityEngine;
using DataStorage;

public class ExampleScript : MonoBehaviour {

  // Use this for initialization
  void Start () {
        PrepareData pr = new PrepareData();
        string data = "cpu_load_another,host=server02,region=us-east value=0.64 1434055562000000000";
        pr.writeMeasurment(GetComponent<MonoBehaviour>(), data);
  }

    // Update is called once per frame
    // Update is used here to make sure everything is created and sent to the server
    void Update()
    {


    }
}
