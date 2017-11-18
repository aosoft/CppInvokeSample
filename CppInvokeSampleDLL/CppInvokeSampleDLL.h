// CppInvokeSampleDLL.cpp : DLL アプリケーション用にエクスポートされる関数を定義します。
//

#include "stdafx.h"


class ICppSample
{
public:
	virtual void STDMETHODCALLTYPE Destroy() = 0;
	virtual int32_t STDMETHODCALLTYPE GetCurrentValue() = 0;
	virtual void STDMETHODCALLTYPE Add(int32_t value) = 0;
	virtual void STDMETHODCALLTYPE Sub(int32_t value) = 0;
};


class CCppSample :
	public ICppSample
{
private:
	int32_t m_value;

public:
	CCppSample();
	~CCppSample();

	virtual void STDMETHODCALLTYPE Destroy() override;
	virtual int32_t STDMETHODCALLTYPE GetCurrentValue() override;
	virtual void STDMETHODCALLTYPE Add(int32_t value) override;
	virtual void STDMETHODCALLTYPE Sub(int32_t value) override;
};

