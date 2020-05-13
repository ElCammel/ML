using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LinearRegression : MonoBehaviour
{
    [DllImport("ML")]
    public extern static IntPtr linear_model_create(int nbDimensions);

    [DllImport("ML")]
    public extern static int linear_model_predict_classification(IntPtr model, int nbDimensions, double[] inputs);

    [DllImport("ML")]
    public extern static void linear_model_train_classification(IntPtr model, int nbDimensions, int epoch, double steps,
                                                                double[] trainingInputs, int trainingInputsNumber, double[] trainingExpectedInputs);

    [DllImport("ML")]
    public extern static int linear_model_predict_regression(IntPtr model, int nbDimensions, double[] inputs);


}
