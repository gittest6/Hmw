using System;

namespace Hmw.Prototype;

public class Building : IMyCloneable<Building>, ICloneable
{
	public Building (string material, decimal square, string purpose)
	{
		Material = material;
		Square = square;
		Purpose = purpose;
	}
	public string Material { get; set; }
	public decimal Square { get; set; }
	public string Purpose { get; set; }
	public virtual Building MyClone ()
	{
		return (Building) this.MemberwiseClone ();
	}
	public object Clone () => MyClone ();
}
