
#include "exports.h"

int symamd_C(int n, int A[], int p[], int perm[], double knobs[COLAMD_KNOBS], int stats[COLAMD_STATS])
{
	return symamd(n, A, p ,perm, knobs, stats, &calloc, &free);
}