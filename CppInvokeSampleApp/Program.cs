using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppInvokeSampleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var cpp1 = new CppSample1())
			using (var cpp2 = new CppSample2())
			{
				for (int i = 0; i < 5; i++)
				{
					cpp1.Add(10);
					Console.WriteLine(cpp1.GetCurrentValue());
				}

				Console.WriteLine("Length = {0}", cpp1.AppendChars("Hello,"));
				Console.WriteLine("Length = {0}", cpp1.AppendChars("CppSample1!"));
				cpp1.PrintChars();

				Console.WriteLine();

				for (int i = 0; i < 5; i++)
				{
					cpp2.Sub(5);
					Console.WriteLine(cpp2.GetCurrentValue());
				}

				Console.WriteLine("Length = {0}", cpp2.AppendChars("Hello,"));
				Console.WriteLine("Length = {0}", cpp2.AppendChars("CppSample2!"));
				cpp2.PrintChars();
			}
		}
	}
}
