#include "library.h"

#include <iostream>
#if _WIN32
#define DLLEXPORT __declspec
#else#defined DLLEXPORT
#endif

extern "C"{

    __declspec(dllexport) int my_add(int x, int y){
        return x + y;
    }

    __declspec(dllexport) int my_mult(int x, int y){
        return x * y;
    }
}


