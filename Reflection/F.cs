﻿using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Hmw.Reflection;
class F
{
	[JsonProperty]
	int i1, i2, i3, i4, i5;
	public static F Get () => new F () { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

	public override string ToString () => $"i1 = {i1}, i2 = {i2}, i3 = {i3}, i4 = {i4}, i5 = {i5}";
}
