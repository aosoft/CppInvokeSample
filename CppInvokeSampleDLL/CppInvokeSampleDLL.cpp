// CppInvokeSampleDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "CppInvokeSampleDLL.h"



CppSample::CppSample() :
	m_value(0)
{
	printf("CCppSample::Create\n");
}

CppSample::~CppSample()
{
}

void CppSample::Destroy()
{
	printf("CCppSample::Destroy [%p]\n", this);
	delete this;
}

int32_t CppSample::GetCurrentValue()
{
	return m_value;
}

void CppSample::Add(int32_t value)
{
	m_value += value;
}

void CppSample::Sub(int32_t value)
{
	m_value -= value;
}

