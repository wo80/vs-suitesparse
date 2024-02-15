
#include "exports.h"

int symamd_C(int n, int A[], int p[], int perm[], double knobs[COLAMD_KNOBS], int stats[COLAMD_STATS])
{
    return symamd(n, A, p ,perm, knobs, stats, &calloc, &free);
}

int csymamd_C(int n, int A[], int p[], int perm[], double knobs[CCOLAMD_KNOBS], int stats[CCOLAMD_STATS], int cmember[], int stype)
{
    return csymamd(n, A, p, perm, knobs, stats, &calloc, &free, cmember, stype);
}

int symamd_l_C(SuiteSparse_long n, SuiteSparse_long A[], SuiteSparse_long p[], SuiteSparse_long perm[], double knobs[COLAMD_KNOBS], SuiteSparse_long stats[COLAMD_STATS])
{
    return symamd_l(n, A, p, perm, knobs, stats, &calloc, &free);
}

int csymamd_l_C(SuiteSparse_long n, SuiteSparse_long A[], SuiteSparse_long p[], SuiteSparse_long perm[], double knobs[CCOLAMD_KNOBS], SuiteSparse_long stats[CCOLAMD_STATS], SuiteSparse_long cmember[], SuiteSparse_long stype)
{
    return csymamd_l(n, A, p, perm, knobs, stats, &calloc, &free, cmember, stype);
}

cholmod_common* cholmod_init()
{
    cholmod_common *c;

    c = SuiteSparse_malloc(1, sizeof(cholmod_common));

    if (cholmod_start(c) == 0) return NULL;

    return c;
}

cholmod_common* cholmod_l_init()
{
    cholmod_common* c;

    c = SuiteSparse_malloc(1, sizeof(cholmod_common));

    if (cholmod_l_start(c) == 0) return NULL;

    return c;
}