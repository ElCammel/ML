using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LinearRegression : MonoBehaviour
{
    [DllImport("ML")]
    public static extern IntPtr linear_model_create(int inDim);

    [DllImport("ML")]
    public static extern double linear_model_predict_regression(IntPtr model, int inDim, double[] paramsDim);

    [DllImport("ML")]
    public static extern void linear_model_train_regression(IntPtr model, int nbDimensions, double steps,
                                                                double[] trainingInputs, int trainingInputsNumber, double[] trainingExpectedOutputs);
}
