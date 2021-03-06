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

extern "C" __declspec(dllexport) int32_t STDMETHODCALLTYPE CreateCppSampleInstance2(
	void ** buffer, int32_t bufferSize) try
{
	typedef void * void_ptr;

	static const void_ptr funcs[] =
	{
		Proxy<ICppSample, void>::Func<&ICppSample::Destroy>,
		Proxy<ICppSample, int32_t>::Func<&ICppSample::GetCurrentValue>,
		Proxy<ICppSample, void, int32_t>::Func<&ICppSample::Add>,
		Proxy<ICppSample, void, int32_t>::Func<&ICppSample::Sub>,
		Proxy<ICppSample, int32_t, const char *, int32_t>::Func<&ICppSample::AppendChars>,
		Proxy<ICppSample, void>::Func<&ICppSample::PrintChars>
	};

	static const int32_t requiredSize = sizeof(funcs) / sizeof(void *) + 1;

	if (buffer == nullptr || bufferSize < 1)
	{
		return requiredSize;
	}

	if (bufferSize < requiredSize)
	{
		return 0;
	}

	buffer[0] = new CppSample();
	int32_t index = 1;

	for (auto it = std::begin(funcs); it != std::end(funcs); it++)
	{
		buffer[index] = *it;
		index++;
	}

	return requiredSize;
} catch(const std::exception&) {
	return 0;
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

