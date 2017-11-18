// dllmain.cpp : DLL アプリケーションのエントリ ポイントを定義します。
#include "stdafx.h"
#include "CppInvokeSampleDLL.h"


extern "C" __declspec(dllexport) ICppSample *STDMETHODCALLTYPE CreateCppSampleInstance() try
{
	return new CppSample();
} catch(const std::exception&) {
	return nullptr;
}


template<class T, typename RetT, typename ... Args>
struct Proxy
{
	template<typename RetT(T::*func)(Args...)>
	static RetT STDMETHODCALLTYPE Func(T *self, Args... args)
	{
		return (self->*func)(args...);
	}
};

void ProxyTest()
{
	void *p = Proxy<ICppSample, int32_t>::Func<&ICppSample::GetCurrentValue>;
	void *p2 = Proxy<ICppSample, void, int32_t>::Func<&ICppSample::Add>;
}


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

