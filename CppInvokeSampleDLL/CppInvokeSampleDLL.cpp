// CppInvokeSampleDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"
#include "CppInvokeSampleDLL.h"



CppSample::CppSample() :
	m_value(0)
{
	printf("CppSample::Create\n");
}

CppSample::~CppSample()
{
}

void CppSample::Destroy()
{
	printf("CppSample::Destroy [%p]\n", this);
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

int32_t CppSample::AppendChars(const char *chars, int32_t length) try
{
	m_str.append(chars, length);
	return static_cast<int32_t>(m_str.length());
} catch(const std::exception&) {
	return 0;
}

void CppSample::PrintChars()
{
	printf("[%p] %s\n", this, m_str.c_str());
}

