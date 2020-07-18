#include "cs.h"
/* create a Householder reflection [v,beta,s]=house(x), overwrite x with v,
 * where (I-beta*v*v')*x = s*e1 and e1 = [1 0 ... 0]'.
 * Note that this CXSparse version is different than CSparse.  See Higham,
 * Accuracy & Stability of Num Algorithms, 2nd ed, 2002, page 357. */
CS_ENTRY cs_house (CS_ENTRY *x, double *beta, CS_INT n)
{
    const CS_ENTRY one = ONE;
    CS_ENTRY u, v, s = ZERO ;
    CS_INT i ;
    double t;
    if (!x || !beta) return (MINUS_ONE) ;          /* check inputs */
    /* s = norm(x) */
    for (i = 0 ; i < n ; i++) MULT_ADD_CONJ(s, x [i], x [i]) ;
    s = CS_SQRT (s) ;
    if (IS_ZERO(s))
    {
        (*beta) = 0 ;
        x [0] = one ;
    }
    else
    {
        /* s = sign(x[0]) * norm (x) ; */
        if (IS_NONZERO(x [0]))
        {
            //s *= x [0] / CS_ABS (x [0]) ;
            u = x[0];
            SCALE_DIV(u, CS_ABS(x[0]));
            v = s;
            MULT(s, v, u);
        }
        //x [0] += s ;
        //(*beta) = 1. / CS_REAL (CS_CONJ (s) * x [0]) ;
        ASSEMBLE(x[0], s);
        MULT(u, CS_CONJ(s), x[0]);
        (*beta) = 1.0 / CS_REAL (u) ;
    }
    SCALE(s, -1.0);
    return s ;
}
