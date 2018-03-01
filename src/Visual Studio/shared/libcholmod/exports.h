#pragma once
#define EXPORT __declspec(dllexport)

#include "cholmod.h"

/* make it easy for C++ programs to include exported methods */
#ifdef __cplusplus
extern "C" {
#endif

	EXPORT cholmod_common* cholmod_init();

#ifdef __cplusplus
}
#endif