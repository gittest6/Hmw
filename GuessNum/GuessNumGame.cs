using Microsoft.Extensions.Configuration;

namespace Hmw.GuessNum;

class GuessNumGame : ClassWithConfig, IConsoleGame
{
	public string name { get; }

	protected int numMin, numMax, tryCount, numGuessed, tryNum;
	protected bool guessed;
	
	private Random rnd = new ();
	
	protected virtual int getRandomNumber () => rnd.Next (numMin, numMax + 1);
	
	public GuessNumGame (IConfigurationRoot config) : base (config)
	{
		name = "Угадай число";
		numMin = Convert.ToInt32 (config["numMin"]);
		numMax = Convert.ToInt32 (config["numMax"]);
		tryCount = Convert.ToInt32 (config["tryCount"]);
	}

	public virtual string start ()
	{
		numGuessed = getRandomNumber ();
		tryNum = 0;
		guessed = false;
		return $"Введите число от {numMin} до {numMax}";
	}
	
	public virtual bool atTheEnd () => tryNum == tryCount || guessed;
	
	public virtual string processInput (string strInput)
	{
		if (String.IsNullOrWhiteSpace (strInput)
			|| ! Int32.TryParse (strInput, out int numEntered))
		{
			return "Неправильный ввод";
		}
		tryNum++;
		guessed = numEntered == numGuessed;
		return guessed ? "да" :
			numEntered < numGuessed ? "больше" : "меньше";
	}
	
	public virtual string getResult ()
	=> guessed ? "Угадали." : $"Не угадали, было загадано {numGuessed}.";
}
