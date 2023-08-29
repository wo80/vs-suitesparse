#include "exports.h"

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