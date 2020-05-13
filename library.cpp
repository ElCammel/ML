#include "library.h"
#include <Eigen>
#include <random>
#include <iostream>

#if _WIN32
#define DLLEXPORT __declspec
#else#defined DLLEXPORT
#endif

extern "C"{

    __declspec(dllexport) int sign(double value) {
        if(value == 0){
            return 0;
        }
        else{
            if( value < 0){
                return -1;
            }
            else{
                return 1;
            }
        }
    }

    __declspec(dllexport) double* linear_model_create(int input_dim){
        auto ndArray = new double[input_dim + 1];

        std::random_device rd;
        std::mt19937 mt(rd());
        std::uniform_real_distribution<float> dist(-1, 1);

        for(int i =0; i < input_dim + 1; i++){
            ndArray[i] = dist(mt);
        }

        return ndArray;
    }

    __declspec(dllexport) double linear_model_predict_regression( double* model, int nb_dimension, double* inputs){
        double sum = model[0];

        for(int i = 0; i < nb_dimension; i++) {
            sum += inputs[i] * model[i + 1];
        }

        return sum;
    }

    __declspec(dllexport) double linear_model_predict_classification(double* model, int nb_dimension, double* inputs){
        double sum = model[0];

        for(int i = 0; i < nb_dimension; i++) {
            sum += inputs[i] * model[i + 1];
        }

        int result = sign(sum);

        return result;
    }


    __declspec(dllexport) void linear_model_train_classification(double* model, int nb_dimension, int epoch, double steps,
                                                                double* training_inputs, int training_inputs_number, double* training_expected_inputs){
        std::random_device rd;
        std::mt19937 mt(rd());
        std::uniform_int_distribution<int> dist(0, 1);

        for(int e = 0; e < epoch; e++) {
            int trainingInput = (int)(dist(mt) * training_inputs_number);
            int trainingParamsPosition = nb_dimension * trainingInput;
            double modification = (double)steps * (training_expected_inputs[trainingInput] -
                                    linear_model_predict_classification(model, nb_dimension, &training_inputs[trainingParamsPosition]));

            model[0] += modification;

            for(int j = 0; j < nb_dimension; j++) {
                model[j + 1] += modification * training_inputs[trainingParamsPosition + j];
            }
        }
    }
}


