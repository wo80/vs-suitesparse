# SuiteSparse for Visual Studio

This is a Visual Studio solution for [SuiteSparse](http://faculty.cse.tamu.edu/davis/suitesparse.html).

## Instructions

The repository does not contain the SuiteSparse source code. It can be obtained from http://faculty.cse.tamu.edu/davis/suitesparse.html. The Visual Studio solution was created with SuiteSparse version 5.1.2, but should also work with newer versions. Download the latest version and extract it to the root folder (a subfolder `SuiteSparse` should be created in the directory where this readme resides).

To build the solution, some projects (like Cholmod) require the BLAS and LAPACK. Please read the wiki page on [BLAS and LAPACK](https://github.com/wo80/vs-suitesparse/wiki/BLAS-and-LAPACK) for instructions on how to resolve these dependencies.

## Why?

The project was created to maintain SuiteSparse builds matching the [CSparse.Interop](https://github.com/wo80/csparse-interop) bindings for C#.
