using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestLinearClassification : MonoBehaviour
{

    public Transform[] trainingSpheres;
    public Transform[] testSpheres;


    public int epoch = 1000000;
    public IntPtr? model;

    // Start is called before the first frame update
    void Start()
    {
        CreateModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateModel()
    {
        this.model = LinearClassification.linear_model_create(2);
        Debug.Log("Created model");
    }

    public void Reinitialize()
    {
        // Put test spheres to 0
        foreach (var testSphere in testSpheres)
        {
            var position = testSphere.position;
            testSphere.position = new Vector3(position.x, 0, position.z);
        }
    }


    public void Train()
    {

        if (this.model == null)
        {
            Debug.Log("Create model before");
            return;
        }

        // Call lib to train on the array
        var trainingSphereNumber = trainingSpheres.Length;
        var trainingInputs = new double[trainingSphereNumber * 2];
        var trainingExpectedInputs = new double[trainingSphereNumber];

        for (var i = 0; i < trainingSphereNumber; i++)
        {
            trainingInputs[i * 2] = trainingSpheres[i].position.x;
            trainingInputs[i * 2 + 1] = trainingSpheres[i].position.z;
            trainingExpectedInputs[i] = trainingSpheres[i].position.y;
        }
        LinearClassification.linear_model_train_classification(this.model.Value, 2, epoch, 0.001, trainingInputs, trainingSphereNumber, trainingExpectedInputs);
        Debug.Log("Model trained !");
    }

    public void Predict()
    {

        if (this.model == null)
        {
            Debug.Log("Create model before");
            return;
        }

        // Call lib to predict test spheres
        foreach (var testSphere in testSpheres)
        {
            var position = testSphere.position;
            double[] inputs = { position.x, position.z };
            var predicted = LinearClassification.linear_model_predict_classification(this.model.Value, 2, inputs);

            position = new Vector3(
                position.x,
                predicted * (float)0.5,
                position.z
            );
            testSphere.position = position;
        }

        Debug.Log("Predicted");
    }
}
