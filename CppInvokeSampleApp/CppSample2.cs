using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CppInvokeSampleApp
{
	class CppSample2 : IDisposable
	{
		[DllImport("CppInvokeSampleDLL.dll")]
		static extern int CreateCppSampleInstance2(IntPtr[] buffer, int bufferSize);

		delegate void FnAction(IntPtr self);
		delegate int FnGetValue(IntPtr self);
		delegate void FnCalc(IntPtr self, int value);
		delegate int FnAppendChars(IntPtr self, byte[] chars, int length);

		IntPtr _self;
		FnAction _fnDestroy;
		FnGetValue _fnGetCurrentValue;
		FnCalc _fnAdd;
		FnCalc _fnSub;
		FnAppendChars _fnAppendChars;
		FnAction _fnPrintChars;

		public CppSample2()
		{
			int bufferSize = CreateCppSampleInstance2(null, 0);
			var buffer = new IntPtr[bufferSize];
			if (CreateCppSampleInstance2(buffer, bufferSize) != bufferSize)
			{
				throw new Exception();
			}

			_self = buffer[0];
			_fnDestroy = Marshal.GetDelegateForFunctionPointer<FnAction>(buffer[1]);
			_fnGetCurrentValue = Marshal.GetDelegateForFunctionPointer<FnGetValue>(buffer[2]);
			_fnAdd = Marshal.GetDelegateForFunctionPointer<FnCalc>(buffer[3]);
			_fnSub = Marshal.GetDelegateForFunctionPointer<FnCalc>(buffer[4]);
			_fnAppendChars = Marshal.GetDelegateForFunctionPointer<FnAppendChars>(buffer[5]);
			_fnPrintChars = Marshal.GetDelegateForFunctionPointer<FnAction>(buffer[6]);
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
