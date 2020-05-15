using System;
using UnityEngine;

public class TestLinearClassificationSoft : MonoBehaviour
{
    public Transform[] trainingSpheres;
    public Transform[] testSpheres;


    public int epoch = 1000000;
    public IntPtr? model;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateModel(int nbDimension)
    {
        this.model = LinearClassification.linear_model_create(nbDimension);
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


    private Vector3 Transform(Vector3 initial)
    {
        var x = initial.x;
        var z = initial.z;
        if (x <= 5 && x >= 4 && z >= 3 && z <= 4 ||
            x >= -3 && x <= -2 && z >= 9 && z <= 10)
        {
            x += 3;
            z += 3;
        }

        if (x <= 4 && x >= 3 && z >= 6 && z <= 7)
        {
            x -= 3;
            z -= 3;
        }

        return new Vector3(x, initial.y, z);
    }



    public void Train()
    {

        CreateModel(2);

        // Call lib to train on the array
        var trainingSphereNumber = trainingSpheres.Length;
        var trainingInputs = new double[trainingSphereNumber * 2];
        var trainingExpectedOutputs = new double[trainingSphereNumber];

        for (var i = 0; i < trainingSphereNumber; i++)
        {
            trainingInputs[i * 2] = trainingSpheres[i].position.x;
            trainingInputs[i * 2 + 1] = trainingSpheres[i].position.z;
            trainingExpectedOutputs[i] = trainingSpheres[i].position.y;
        }
        LinearClassification.linear_model_train_classification(this.model.Value, 2, epoch, 0.001, trainingInputs, trainingSphereNumber, trainingExpectedOutputs);
        Debug.Log("Model trained !");
    }

    public void TrainSoft()
    {

        CreateModel(2);

        // Call lib to train on the array
        var trainingSphereNumber = trainingSpheres.Length;
        var trainingInputs = new double[trainingSphereNumber * 2];
        var trainingExpectedOutputs = new double[trainingSphereNumber];

        for (var i = 0; i < trainingSphereNumber; i++)
        {
            var position = trainingSpheres[i].position;
            position = Transform(position);
            trainingInputs[i * 2] = position.x;
            trainingInputs[i * 2 + 1] = position.z;
            trainingExpectedOutputs[i] = position.y;
        }
        LinearClassification.linear_model_train_classification(this.model.Value, 2, epoch, 0.001, trainingInputs, trainingSphereNumber, trainingExpectedOutputs);
        Debug.Log("Model trained !");
    }

    public void TrainXor()
    {

        CreateModel(1);

        // Call lib to train on the array
        var trainingSphereNumber = trainingSpheres.Length;
        var trainingInputs = new double[trainingSphereNumber * 2];
        var trainingExpectedOutputs = new double[trainingSphereNumber];

        for (var i = 0; i < trainingSphereNumber; i++)
        {
            trainingInputs[i] = Math.Pow(trainingSpheres[i].position.x + trainingSpheres[i].position.z, 2);
            trainingExpectedOutputs[i] = trainingSpheres[i].position.y;
        }

        LinearClassification.linear_model_train_classification(this.model.Value, 1, epoch, 0.001, trainingInputs, trainingSphereNumber, trainingExpectedOutputs);
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

    public void PredictSoft()
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
            var transformedPosition = Transform(position);
            double[] inputs = { transformedPosition.x, transformedPosition.z };
            var predicted = LinearClassification.linear_model_predict_classification(this.model.Value, 1, inputs);

            position = new Vector3(
                position.x,
                predicted * (float)0.5,
                position.z
            );

            testSphere.position = position;
        }

        Debug.Log("Predicted");
    }

    public void PredictXor()
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
            double[] inputs = { Math.Pow(position.x + position.z, 2) };
            var predicted = LinearClassification.linear_model_predict_classification(this.model.Value, 1, inputs);

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
