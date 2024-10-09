using System;
using System.Collections.Generic;

namespace Hmw.Events;
static class Extensions
{
	public static T GetMax<T> (this IEnumerable<T> collection, Func<T, float> convertToNumber)
	where T : class
	{
		float numMax = 0;
		T eMax = null;
		foreach (var e in collection)
		{
			var numCur = convertToNumber (e);
			if (numCur > numMax)
			{ 
				numMax = numCur;
				eMax = e;
			}
		}
		return eMax;
	}
}
