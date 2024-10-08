namespace Hmw.GuessNum;

class NamedConsolePlayer : IConsolePlayer
{
	public string name { get; set; }
	
	public NamedConsolePlayer ()
	{
		Console.Write ("Введите имя: ");
		name = Console.ReadLine ();		
	}
	
	public virtual void play (IConsoleGame cg)
	{
		Console.WriteLine ($"Игра \"{cg.name}\"");
		Console.WriteLine (cg.start ());
		while (! cg.atTheEnd ())
		{
			var strRet = cg.processInput (Console.ReadLine ());
			Console.WriteLine (strRet);
		}
		Console.WriteLine (cg.getResult ());
	}
}
