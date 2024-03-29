#include "cs.h"

/* convert from complex to real (int version) */
/* C = real(A) if real is true, imag(A) otherwise */
cs_di *cs_i_real (cs_ci *A, int real)
{
    cs_di *C ;
    int n, triplet, nn, p, nz, *Ap, *Ai, *Cp, *Ci ;
    cs_complex_t *Ax ;
    double *Cx ;
    if (!A || !A->x) return (NULL) ;    /* return if A NULL or pattern-only */
    n = A->n ; Ap = A->p ; Ai = A->i ; Ax = A->x ;
    triplet = (A->nz >= 0) ;            /* true if A is a triplet matrix */
    nz = triplet ? A->nz : Ap [n] ;
    C = cs_di_spalloc (A->m, n, A->nzmax, 1, triplet) ;
    if (!C) return (NULL) ;
    Cp = C->p ; Ci = C->i ; Cx = C->x ;
    nn = triplet ? nz : (n+1) ;
    for (p = 0 ; p < nz ; p++) Ci [p] = Ai [p] ;
    for (p = 0 ; p < nn ; p++) Cp [p] = Ap [p] ;
    for (p = 0 ; p < nz ; p++) Cx [p] = real ? creal (Ax [p]) : cimag (Ax [p]) ;
    if (triplet) C->nz = nz ;
    return (C) ;
}

/* convert from real to complex (int version) */
/* C = A if real is true, or C = i*A otherwise */
cs_ci *cs_i_complex (cs_di *A, int real)
{
    cs_ci *C ;
    int n, triplet, nn, p, nz, *Ap, *Ai, *Cp, *Ci ;
    double *Ax ;
    cs_complex_t *Cx ;
    if (!A || !A->x) return (NULL) ;    /* return if A NULL or pattern-only */
    n = A->n ; Ap = A->p ; Ai = A->i ; Ax = A->x ;
    triplet = (A->nz >= 0) ;            /* true if A is a triplet matrix */
    nz = triplet ? A->nz : Ap [n] ;
    C = cs_ci_spalloc (A->m, n, A->nzmax, 1, triplet) ;
    if (!C) return (NULL) ;
    Cp = C->p ; Ci = C->i ; Cx = C->x ;
    nn = triplet ? nz : (n+1) ;
    for (p = 0 ; p < nz ; p++) Ci [p] = Ai [p] ;
    for (p = 0 ; p < nn ; p++) Cp [p] = Ap [p] ;
    for (p = 0 ; p < nz ; p++) Cx [p] = real ? _DCOMPLEX_(Ax [p], 0) : _DCOMPLEX_(0, Ax [p]) ;
    if (triplet) C->nz = nz ;
    return (C) ;
}

/* convert from complex to real (int64_t version) */
/* C = real(A) if real is true, imag(A) otherwise */
cs_dl *cs_l_real (cs_cl *A, int64_t real)
{
    cs_dl *C ;
    int64_t n, triplet, nn, p, nz, *Ap, *Ai, *Cp, *Ci ;
    cs_complex_t *Ax ;
    double *Cx ;
    if (!A || !A->x) return (NULL) ;    /* return if A NULL or pattern-only */
    n = A->n ; Ap = A->p ; Ai = A->i ; Ax = A->x ;
    triplet = (A->nz >= 0) ;            /* true if A is a triplet matrix */
    nz = triplet ? A->nz : Ap [n] ;
    C = cs_dl_spalloc (A->m, n, A->nzmax, 1, triplet) ;
    if (!C) return (NULL) ;
    Cp = C->p ; Ci = C->i ; Cx = C->x ;
    nn = triplet ? nz : (n+1) ;
    for (p = 0 ; p < nz ; p++) Ci [p] = Ai [p] ;
    for (p = 0 ; p < nn ; p++) Cp [p] = Ap [p] ;
    for (p = 0 ; p < nz ; p++) Cx [p] = real ? creal (Ax [p]) : cimag (Ax [p]) ;
    if (triplet) C->nz = nz ;
    return (C) ;
}

/* convert from real to complex (int64_t version) */
/* C = A if real is true, or C = i*A otherwise */
cs_cl *cs_l_complex (cs_dl *A, int64_t real)
{
    cs_cl *C ;
    int64_t n, triplet, nn, p, nz, *Ap, *Ai, *Cp, *Ci ;
    double *Ax ;
    cs_complex_t *Cx ;
    if (!A || !A->x) return (NULL) ;    /* return if A NULL or pattern-only */
    n = A->n ; Ap = A->p ; Ai = A->i ; Ax = A->x ;
    triplet = (A->nz >= 0) ;            /* true if A is a triplet matrix */
    nz = triplet ? A->nz : Ap [n] ;
    C = cs_cl_spalloc (A->m, n, A->nzmax, 1, triplet) ;
    if (!C) return (NULL) ;
    Cp = C->p ; Ci = C->i ; Cx = C->x ;
    nn = triplet ? nz : (n+1) ;
    for (p = 0 ; p < nz ; p++) Ci [p] = Ai [p] ;
    for (p = 0 ; p < nn ; p++) Cp [p] = Ap [p] ;
    for (p = 0 ; p < nz ; p++) Cx [p] = real ? _DCOMPLEX_(Ax [p], 0) : _DCOMPLEX_(0, Ax [p]);
    if (triplet) C->nz = nz ;
    return (C) ;
}
