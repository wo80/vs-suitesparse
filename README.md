# SuiteSparse for Visual Studio

This is a Visual Studio solution for [SuiteSparse](http://faculty.cse.tamu.edu/davis/suitesparse.html).

## Instructions

The repository does not contain the SuiteSparse source code. It can be obtained from http://faculty.cse.tamu.edu/davis/suitesparse.html. The Visual Studio solution was created for SuiteSparse version ([5.1.2](http://faculty.cse.tamu.edu/davis/SuiteSparse/SuiteSparse-5.1.2.tar.gz)), but should also work with newer versions. Download the latest version and extract it to the `src` folder (a subfolder `SuiteSparse` should be created automatically). To check if everything is in its right place, make sure that the file `src/SuiteSparse/README.txt` exists.

To build the solution, some projects (like Cholmod) require the BLAS and LAPACK. Please read the wiki page [BLAS and LAPACK](https://github.com/wo80/vs-suitesparse/wiki/BLAS-and-LAPACK) for instructions on how to resolve these dependencies.

### Compiler errors

The project files were created using Visual Studio 2017. If you get compiler errors, they might be related to an issue reported at [suitesparse-metis-for-windows](https://github.com/jlblancoc/suitesparse-metis-for-windows/issues/19). Please update the file `gk_arch.h` located in `src/SuiteSparse/metis-5.1.0/GKlib` according to the instructions given there. Then try to recompile.

## Why?

The project was created to maintain SuiteSparse builds matching the [CSparse.Interop](https://github.com/wo80/csparse-interop) bindings for C#.
