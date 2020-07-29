// =============================================================================
// === spqr_freefac ============================================================
// =============================================================================

// Frees the contents of the QR factor object.

#include "spqr.hpp"

template <typename Entry> void spqr_freefac
(
    SuiteSparseQR_factorization <Entry> **QR_handle,

    // workspace and parameters
    cholmod_common *cc
)

{
    SuiteSparseQR_factorization <Entry> *QR ;
    Long n, m, bncols, n1rows, r1nz ;

    if (QR_handle == NULL || *QR_handle == NULL)
    {
        // nothing to do; caller probably ran out of memory
        return ;
    }
    QR = *QR_handle ;

    n      = QR->nacols ;
    m      = QR->narows ;
    bncols = QR->bncols ;
    n1rows = QR->n1rows ;
    r1nz   = QR->r1nz ;

    spqr_freenum (& (QR->QRnum), cc) ;
    spqr_freesym (& (QR->QRsym), cc) ;

    CHOLMOD(free) (n+bncols, sizeof (Long),  QR->Q1fill,  cc) ; 
    CHOLMOD(free) (m,        sizeof (Long),  QR->P1inv,   cc) ;
    CHOLMOD(free) (m,        sizeof (Long),  QR->HP1inv,  cc) ;
    CHOLMOD(free) (n1rows+1, sizeof (Long),  QR->R1p,     cc) ;
    CHOLMOD(free) (r1nz,     sizeof (Long),  QR->R1j,     cc) ;
    CHOLMOD(free) (r1nz,     sizeof (Entry), QR->R1x,     cc) ;
    CHOLMOD(free) (n,        sizeof (Long),  QR->Rmap,    cc) ;
    CHOLMOD(free) (n,        sizeof (Long),  QR->RmapInv, cc) ;

    CHOLMOD(free) (1, sizeof (SuiteSparseQR_factorization <Entry>), QR, cc) ;
    *QR_handle = NULL ;
}

// =============================================================================

template void spqr_freefac <double>
(
    SuiteSparseQR_factorization <double> **QR_handle,

    // workspace and parameters
    cholmod_common *cc
) ;

// =============================================================================

template void spqr_freefac <Complex>
(
    SuiteSparseQR_factorization <Complex> **QR_handle,

    // workspace and parameters
    cholmod_common *cc
) ;
