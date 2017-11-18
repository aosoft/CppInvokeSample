using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CppInvokeSampleApp
{
	class CppSample1 : IDisposable
	{
		[DllImport("CppInvokeSampleDLL.dll", CallingConvention = CallingConvention.Winapi)]
		static extern IntPtr CreateCppSampleInstance();

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate void FnDestroy(IntPtr self);

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate int FnGetValue(IntPtr self);

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate void FnCalc(IntPtr self, int value);

		private IntPtr _self;
		private FnDestroy _fnDestroy;
		private FnGetValue _fnGetCurrentValue;
		private FnCalc _fnAdd;
		private FnCalc _fnSub;

		public CppSample1()
		{
			_self = CreateCppSampleInstance();
			var funcs = new IntPtr[4];

			Marshal.Copy(Marshal.ReadIntPtr(_self, 0), funcs, 0, funcs.Length);
			_fnDestroy = Marshal.GetDelegateForFunctionPointer<FnDestroy>(funcs[0]);
			_fnGetCurrentValue = Marshal.GetDelegateForFunctionPointer<FnGetValue>(funcs[1]);
			_fnAdd = Marshal.GetDelegateForFunctionPointer<FnCalc>(funcs[2]);
			_fnSub = Marshal.GetDelegateForFunctionPointer<FnCalc>(funcs[3]);
		}

		public void Dispose()
		{
			_fnDestroy?.Invoke(_self);
		}

		public int GetCurrentValue()
		{
			return _fnGetCurrentValue(_self);
		}

		public void Add(int value)
		{
			_fnAdd(_self, value);
		}

		public void Sub(int value)
		{
			_fnSub(_self, value);
		}
	}
}
