using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Transform[] trainingSpheres;
    public Transform[] testSpheres;

    public int[] npl;

    public double[] classSphere;

    public IntPtr? model;

    // Start is called before the first frame update
    void Start() {
        CreateModel();
    }

    public void Reinitialize() {
        foreach (var testSphere in testSpheres) {
            var position = testSphere.position;
            testSphere.position = new Vector3(position.x, 0, position.z);
        }
    }


    public void Train() {
        if (this.model == null) {
            Debug.Log("Create model before");
            return;
        }
        var Y = new double[trainingSpheres.Length];

        var inputs = new double[trainingSpheres.Length * 2];

        for (int i = 0; i < trainingSpheres.Length; i++) {
            Y[i] = trainingSpheres[i].position.y >= 0 ? 1 : -1;
            inputs[i * 2] = trainingSpheres[i].position.x;
            inputs[(i * 2) + 1] = trainingSpheres[i].position.z;
        }

        IntPtr model = VisualStudioLibWrapper.create_mlp_model(npl, npl.Length);
        VisualStudioLibWrapper.mlp_model_train_classification(model, inputs, trainingSpheres.Length, 2, Y, Y.Length, 1000000, 0.001f);

        Debug.Log("Model trained !");
    }

    public void Predict() {

        if (this.model == null) {
            Debug.Log("Create model before");
            return;
        }

        double max = -2;
        int idmax = 1;

        foreach (var testSpheres in testSpheres) {
            double[] input = { testSpheres.position.x, testSpheres.position.z };
            IntPtr y = VisualStudioLibWrapper.mlp_model_predict_classification(model, input, false);
            double* r = (double*)y.ToPointer();
            max = r[1];
            for (int i = 1; i < classSphere.Length + 1; i++)
            {
                if (r[i] > max)
                {
                    max = r[i];
                    idmax = i;
                }
            }
            if (classSphere.Length == 2) {
                testSpheres.position = new Vector3(
                    testSpheres.position.x,
                    (float)r[1],
                    testSpheres.position.z
                );
            } else {
                testSpheres.position = new Vector3(
                    testSpheres.position.x,
                    (float)classSphere[idmax - 1],
                    testSpheres.position.z
                );
            }
        }

        VisualStudioLibWrapper.clearArray(model);

        Debug.Log("Predicted");
    }
}
