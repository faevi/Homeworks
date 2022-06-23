using System;
namespace ConsoleApp
{
	public class Task_3_2<T>
	{		
		public static List<T> Map(List<T> list, Func<T,T> function)
        {
			for (int elementNumber = 0; elementNumber < list.Count(); elementNumber++)
            {
				list[elementNumber] = function(list[elementNumber]);
            }
				
			return list;
        }

		public static List<T> Filter(List<T> list, Predicate<T> function)
		{
			List<T> outList = new List<T>();

			for (int elementNumber = 0; elementNumber < list.Count(); elementNumber++)
			{
				if (function(list[elementNumber]))
                {
					outList.Add(list[elementNumber]);
                }
			}

			return outList;
		}

		public static double Fold(List<double> list, Func<double, double, double> function)
		{
			double acc = 1;

			for (int elementNumber = 0; elementNumber < list.Count(); elementNumber++)
			{
				acc = function(list[elementNumber], acc);
			}

			return acc;
		}
	}
}

