#ifndef EXPORTS_H
#define EXPORTS_H

#define EXPORT __declspec(dllexport)

#include <colamd.h>

/* make it easy for C++ programs to include exported methods */
#ifdef __cplusplus
extern "C" {
#endif


EXPORT int symamd_C  /* return (1) if OK, (0) otherwise */
(
    int n,  /* number of rows and columns of A */
    int A [],  /* row indices of A */
    int p [],  /* column pointers of A */
    int perm [], /* output permutation, size n_col+1 */
    double knobs [COLAMD_KNOBS],  /* parameters (uses defaults if NULL) */
    int stats [COLAMD_STATS]  /* output statistics and error codes */
) ;

 
#ifdef __cplusplus
}
#endif

 
#endif