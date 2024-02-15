#include "exports.h"

int symamd_C(int n, int A[], int p[], int perm[], double knobs[COLAMD_KNOBS], int stats[COLAMD_STATS])
{
    return symamd(n, A, p ,perm, knobs, stats, &calloc, &free);
}

int symamd_l_C(SuiteSparse_long n, SuiteSparse_long A[], SuiteSparse_long p[], SuiteSparse_long perm[], double knobs[COLAMD_KNOBS], SuiteSparse_long stats[COLAMD_STATS])
{
        return symamd_l(n, A, p, perm, knobs, stats, &calloc, &free);
}
