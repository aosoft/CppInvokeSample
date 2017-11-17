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
			using (var cpp = new CppSample1())
			{
				for (int i = 0; i < 5; i++)
				{
					cpp.Add(10);
					Console.WriteLine(cpp.GetCurrentValue());
				}
				for (int i = 0; i < 5; i++)
				{
					cpp.Sub(5);
					Console.WriteLine(cpp.GetCurrentValue());
				}
			}

		}
	}
}
