// =============================================================================
// === spqr_freesym ============================================================
// =============================================================================

// Frees the contents of the QR Symbolic object

#include "spqr.hpp"

void spqr_freesym
(
    spqr_symbolic **QRsym_handle,

    // workspace and parameters
    cholmod_common *cc
)
{
    spqr_symbolic *QRsym ;
    spqr_gpu *QRgpu ;
    Long m, n, anz, nf, rjsize, ns, ntasks ;

    if (QRsym_handle == NULL || *QRsym_handle == NULL)
    {
        // nothing to do; caller probably ran out of memory
        return ;
    }
    QRsym = *QRsym_handle  ;

    m = QRsym->m ;
    n = QRsym->n ;
    nf = QRsym->nf ;
    anz = QRsym->anz ;
    rjsize = QRsym->rjsize ;

    CHOLMOD(free) (n,      sizeof (Long), QRsym->Qfill, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Super, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Rp, cc) ;
    CHOLMOD(free) (rjsize, sizeof (Long), QRsym->Rj, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Parent, cc) ;
    CHOLMOD(free) (nf+2,   sizeof (Long), QRsym->Childp, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Child, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Post, cc) ;
    CHOLMOD(free) (m,      sizeof (Long), QRsym->PLinv, cc) ;
    CHOLMOD(free) (n+2,    sizeof (Long), QRsym->Sleft, cc) ;
    CHOLMOD(free) (m+1,    sizeof (Long), QRsym->Sp, cc) ;
    CHOLMOD(free) (anz,    sizeof (Long), QRsym->Sj, cc) ;

    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Hip, cc) ;

    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Fm, cc) ;
    CHOLMOD(free) (nf+1,   sizeof (Long), QRsym->Cm, cc) ;

    CHOLMOD(free) (n,      sizeof (Long), QRsym->ColCount, cc) ;

    // gpu metadata
    QRgpu = QRsym->QRgpu;
    if(QRgpu)
    {
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->RimapOffsets, cc) ;
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->RjmapOffsets, cc) ;
        CHOLMOD(free) (nf+2, sizeof (Long),   QRgpu->Stagingp, cc) ;
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->StageMap, cc) ;
        CHOLMOD(free) (nf+1, sizeof (size_t), QRgpu->FSize, cc) ;
        CHOLMOD(free) (nf+1, sizeof (size_t), QRgpu->RSize, cc) ;
        CHOLMOD(free) (nf+1, sizeof (size_t), QRgpu->SSize, cc) ;
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->FOffsets, cc) ;
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->ROffsets, cc) ;
        CHOLMOD(free) (nf,   sizeof (Long),   QRgpu->SOffsets, cc) ;

        CHOLMOD(free) (1, sizeof (spqr_gpu), QRgpu, cc) ;
    }

    // parallel analysis
    ntasks = QRsym->ntasks ;
    CHOLMOD(free) (ntasks+2, sizeof (Long), QRsym->TaskChildp, cc) ;
    CHOLMOD(free) (ntasks+1, sizeof (Long), QRsym->TaskChild, cc) ;
    CHOLMOD(free) (nf+1,     sizeof (Long), QRsym->TaskFront, cc) ;
    CHOLMOD(free) (ntasks+2, sizeof (Long), QRsym->TaskFrontp, cc) ;
    CHOLMOD(free) (ntasks+1, sizeof (Long), QRsym->TaskStack, cc) ;
    CHOLMOD(free) (nf+1,     sizeof (Long), QRsym->On_stack, cc) ;

    ns = QRsym->ns ;
    CHOLMOD(free) (ns+2,     sizeof (Long), QRsym->Stack_maxstack, cc) ;

    CHOLMOD(free) (1, sizeof (spqr_symbolic), QRsym, cc) ;

    *QRsym_handle = NULL ;
}
