// CppInvokeSampleDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"


class ICppSample
{
public:
	virtual void Destroy() = 0;
	virtual int32_t GetCurrentValue() = 0;
	virtual void Add(int32_t value) = 0;
	virtual void Sub(int32_t value) = 0;
};


class CppSample :
	public ICppSample
{
private:
	int32_t m_value;

public:
	CppSample();
	~CppSample();

	virtual void Destroy() override;
	virtual int32_t GetCurrentValue() override;
	virtual void Add(int32_t value) override;
	virtual void Sub(int32_t value) override;
};

