namespace Hmw.GuessNum;

interface IConsoleGame
{
	string name { get; }
	
	string start ();
	
	string processInput (string strInput);
	
	bool atTheEnd ();
	
	string getResult ();
}
