using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class LinearClassification : MonoBehaviour
{
    [DllImport("ML_project")]
    public static extern IntPtr create_mlp_model(int[] npl, int npl_size);

    [DllImport("ML_project")]
    public static extern IntPtr mlp_model_predict_classification(IntPtr mlp, double[] inputs, bool isReg);

    [DllImport("ML_project")]
    public static extern void mlp_model_train_classification(IntPtr mlp, double[] dataset_inputs, int dataset_length, int inputs_size, double[] dataset_expected_outputs, int outputs_size, int epoch, double alpha);

    [DllImport("ML_project")]
    public static extern void mlp_model_train_regression(IntPtr mlp, double[] dataset_inputs, int dataset_length, int inputs_size, double[] dataset_expected_outputs, int outputs_size, int epoch, double alpha);

}
