using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Hmw.Reflection;
using Newtonsoft.Json;

var nTimes = 100000;


Console.WriteLine ($"""
Количество замеров: {nTimes} итераций

Custom:
{Test (nTimes, CustomSerialize, customDeserialize)}

Newtonsoft.Json:
{Test (nTimes, NewtonsoftSerialize, NewtonsoftDeserialize)}
""");

string Test (int nTimes, Func<F, string> funcSer, Func<string, F> funcDeser)
{
	var f = F.Get ();
	string s = null;
	var sw = new Stopwatch ();
	long totalTimeSer = 0, totalTimeDeser = 0;
	
	for (var i = 0; i < nTimes; i++)
	{
		sw.Restart ();
		s = funcSer (f);
		sw.Stop ();
		totalTimeSer += sw.Elapsed.Microseconds;
	}

	for (var i = 0; i < nTimes; i++)
	{
		sw.Restart ();
		f = funcDeser (s);
		sw.Stop ();
		totalTimeDeser += sw.Elapsed.Microseconds;
	}

	float avgSer = (float) totalTimeSer / nTimes, avgDeser = (float) totalTimeDeser / nTimes;
	return $"""
Время на сериализацию = {avgSer:F2} мкс
Время на десериализацию = {avgDeser:F2} мкс
""";
}

string CustomSerialize (F f)
{
	var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
	var arrFieldInfo = typeof (F).GetFields (bindingFlags);
	return String.Join ("\n", arrFieldInfo.Select (fieldInfo => fieldInfo.Name + "," + fieldInfo.GetValue (f)));
}

F customDeserialize (string s)
{
	var dictFields = s.Split ('\n').Select (l => l.Split (',')).ToDictionary (l => l[0], l => l[1]);
	var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
	var f = Activator.CreateInstance<F> ();
	var arrFieldInfo = typeof (F).GetFields (bindingFlags);
	foreach (var fieldInfo in arrFieldInfo)
		if (dictFields.ContainsKey (fieldInfo.Name))
		{
			object field = Convert.ChangeType (dictFields[fieldInfo.Name], fieldInfo.FieldType);
			fieldInfo.SetValue (f, field);
		}
	return f;
}

string NewtonsoftSerialize (F f) => JsonConvert.SerializeObject (f);

F NewtonsoftDeserialize (string s) => JsonConvert.DeserializeObject<F> (s);
