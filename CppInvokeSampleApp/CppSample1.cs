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
		delegate void FnAction(IntPtr self);

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate int FnGetValue(IntPtr self);

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate void FnCalc(IntPtr self, int value);

		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		delegate int FnAppendChars(IntPtr self, byte[] chars, int length);

		IntPtr _self;
		FnAction _fnDestroy;
		FnGetValue _fnGetCurrentValue;
		FnCalc _fnAdd;
		FnCalc _fnSub;
		FnAppendChars _fnAppendChars;
		FnAction _fnPrintChars;

		public CppSample1()
		{
			_self = CreateCppSampleInstance();
			var funcs = new IntPtr[6];

			Marshal.Copy(Marshal.ReadIntPtr(_self, 0), funcs, 0, funcs.Length);
			_fnDestroy = Marshal.GetDelegateForFunctionPointer<FnAction>(funcs[0]);
			_fnGetCurrentValue = Marshal.GetDelegateForFunctionPointer<FnGetValue>(funcs[1]);
			_fnAdd = Marshal.GetDelegateForFunctionPointer<FnCalc>(funcs[2]);
			_fnSub = Marshal.GetDelegateForFunctionPointer<FnCalc>(funcs[3]);
			_fnAppendChars = Marshal.GetDelegateForFunctionPointer<FnAppendChars>(funcs[4]);
			_fnPrintChars = Marshal.GetDelegateForFunctionPointer<FnAction>(funcs[5]);
		}

		public void Dispose()
		{
			_fnDestroy?.Invoke(_self);
			_fnDestroy = null;
			_self = IntPtr.Zero;
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

		public int AppendChars(string str)
		{
			var chars = Encoding.ASCII.GetBytes(str);
			return _fnAppendChars(_self, chars, chars.Length);
		}

		public void PrintChars()
		{
			_fnPrintChars(_self);
		}
	}
}
