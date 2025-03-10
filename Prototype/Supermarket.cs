namespace Hmw.Prototype;

public class Supermarket : Shop, IMyCloneable<Supermarket>
{
	public Supermarket (string material, decimal square, string purpose, string name, byte openHour, byte closeHour, string[] sections)
	: base (material, square, purpose, name, openHour, closeHour)
	{
		Sections = sections;
	}
	public string[] Sections { get; set; }
	public override Supermarket MyClone ()
	=> new (Material, Square, Purpose, Name, OpenHour, CloseHour, Sections);
}
