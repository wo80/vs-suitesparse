
#include "exports.h"

int symamd_C(int n, int A[], int p[], int perm[], double knobs[COLAMD_KNOBS], int stats[COLAMD_STATS])
{
	return symamd(n, A, p ,perm, knobs, stats, &calloc, &free);
}

cholmod_common* cholmod_init()
{
	cholmod_common *c;

	c = SuiteSparse_malloc(1, sizeof(cholmod_common));

	if (cholmod_start(c) == 0) return NULL;

	return c;
}