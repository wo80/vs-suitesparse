#include "exports.h"

int csymamd_C(int n, int A[], int p[], int perm[], double knobs[CCOLAMD_KNOBS], int stats[CCOLAMD_STATS], int cmember[], int stype)
{
    return csymamd(n, A, p, perm, knobs, stats, &calloc, &free, cmember, stype);
}

int csymamd_l_C(SuiteSparse_long n, SuiteSparse_long A[], SuiteSparse_long p[], SuiteSparse_long perm[], double knobs[CCOLAMD_KNOBS], SuiteSparse_long stats[CCOLAMD_STATS], SuiteSparse_long cmember[], SuiteSparse_long stype)
{
    return csymamd_l(n, A, p, perm, knobs, stats, &calloc, &free, cmember, stype);
}
