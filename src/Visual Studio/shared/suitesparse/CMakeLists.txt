# This is a simple CMake build script to build the all-in-one shared
# library (suitesparse.dll) from the official SuiteSparse package.

cmake_minimum_required(VERSION 3.22)

project(suitesparse-aio VERSION "7.6.0" LANGUAGES C)

add_library(suitesparse SHARED exports.c exports.h exports.def)

if (MSVC)
  target_link_options(suitesparse PRIVATE /DEF:"exports.def")
else ()
  message(FATAL_ERROR "This project is for Visual Studio only.")
endif ()

# look for SuiteSparse packages:
find_package(SuiteSparse_config 7.6.0 REQUIRED)
find_package(AMD 3.3.1 REQUIRED)
find_package(BTF 2.3.1 REQUIRED)
find_package(CAMD 3.3.1 REQUIRED)
find_package(CCOLAMD 3.3.2 REQUIRED)
find_package(CHOLMOD 5.2.0 REQUIRED)
find_package(COLAMD 3.3.2 REQUIRED)
find_package(CXSparse 4.3.1 REQUIRED)
find_package(KLU 2.3.2 REQUIRED)
find_package(KLU_CHOLMOD 2.3.2 REQUIRED)
find_package(LDL 3.3.1 REQUIRED)
find_package(ParU 0.1.2 REQUIRED)
find_package(RBio 4.3.1 REQUIRED)
find_package(SPQR 4.3.2 REQUIRED)
find_package(UMFPACK 6.3.2 REQUIRED)

target_link_libraries(suitesparse
  PRIVATE
    SuiteSparse::SuiteSparseConfig_static
    SuiteSparse::AMD_static
    SuiteSparse::CAMD_static
    SuiteSparse::COLAMD_static
    SuiteSparse::CCOLAMD_static
    SuiteSparse::BTF_static
    SuiteSparse::KLU_static
    SuiteSparse::KLU_CHOLMOD_static
    SuiteSparse::CXSparse_static
    SuiteSparse::LDL_static
    SuiteSparse::ParU_static
    SuiteSparse::RBio_static
    SuiteSparse::CHOLMOD_static
    SuiteSparse::UMFPACK_static
    SuiteSparse::SPQR_static)
