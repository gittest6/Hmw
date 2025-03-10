namespace Hmw.Prototype;

public class Shop : Building, IMyCloneable<Shop>
{
	public Shop (string material, decimal square, string purpose, string name, byte openHour, byte closeHour)
    : base (material, square, purpose)
    {
        Name = name;
        OpenHour = openHour;
        CloseHour = closeHour;
    }
    public string Name { get; set; }
    public byte OpenHour {get; set; }
    public byte CloseHour { get; set; }
    public override Shop MyClone ()
    => new (Material, Square, Purpose, Name, OpenHour, CloseHour);
}
