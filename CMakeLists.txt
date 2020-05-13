cmake_minimum_required(VERSION 3.16)
project(ML)

set(CMAKE_CXX_STANDARD 14)
add_library(ML SHARED library.cpp library.h)

set(EIGEN_DIR "C:\\Users\\lamyg\\Desktop\\ML\\eigen-3.3.7\\Eigen")
include_directories(${EIGEN_DIR})
target_link_libraries(ML ${EIGEN_DIR})

