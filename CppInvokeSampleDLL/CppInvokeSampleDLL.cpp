// CppInvokeSampleDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "CppInvokeSampleDLL.h"



CCppSample::CCppSample() :
	m_value(0)
{
}

CCppSample::~CCppSample()
{
}

void CCppSample::Destroy()
{
	delete this;
}

int32_t CCppSample::GetCurrentValue()
{
	return m_value;
}

void CCppSample::Add(int32_t value)
{
	m_value += value;
}

void CCppSample::Sub(int32_t value)
{
	m_value -= value;
}

