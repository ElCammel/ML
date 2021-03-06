﻿using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class LinearClassification : MonoBehaviour
{
    [DllImport("ML")]
    public extern static IntPtr linear_model_create(int nbDimensions);

    [DllImport("ML")]
    public extern static int linear_model_predict_classification(IntPtr model, int nbDimensions, double[] inputs);

    [DllImport("ML")]
    public extern static void linear_model_train_classification(IntPtr model, int nbDimensions, int epoch, double steps,
                                                                double[] trainingInputs, int trainingInputsNumber, double[] trainingExpectedOutputs);
}
