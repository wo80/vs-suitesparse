#ifndef EXPORTS_H
#define EXPORTS_H

#define EXPORT __declspec(dllexport)

#include <ccolamd.h>

/* make it easy for C++ programs to include exported methods */
#ifdef __cplusplus
extern "C" {
#endif

EXPORT int csymamd_C  /* return (1) if OK, (0) otherwise */
(
    int n,  /* number of rows and columns of A */
    int A[],  /* row indices of A */
    int p[],  /* column pointers of A */
    int perm[],  /* output permutation, size n_col+1 */
    double knobs [CCOLAMD_KNOBS],/* parameters (uses defaults if NULL) */
    int stats [CCOLAMD_STATS], /* output statistics and error codes */
    int cmember[], /* Constraint set of A */
    int stype /* 0: use both parts, >0: upper, <0: lower */
);

EXPORT int csymamd_l_C
(
    SuiteSparse_long n,
    SuiteSparse_long A[],
    SuiteSparse_long p[],
    SuiteSparse_long perm[],
    double knobs[CCOLAMD_KNOBS],
    SuiteSparse_long stats[CCOLAMD_STATS],
    SuiteSparse_long cmember[],
    SuiteSparse_long stype
);

#ifdef __cplusplus
}
#endif


#endif