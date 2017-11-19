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

		private IntPtr _self;
		private FnAction _fnDestroy;
		private FnGetValue _fnGetCurrentValue;
		private FnCalc _fnAdd;
		private FnCalc _fnSub;
		private FnAppendChars _fnAppendChars;
		private FnAction _fnPrintChars;

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

		public void AppendChars(string str)
		{
			var chars = Encoding.ASCII.GetBytes(str);
			_fnAppendChars(_self, chars, chars.Length);
		}

		public void PrintChars()
		{
			_fnPrintChars(_self);
		}
	}
}
