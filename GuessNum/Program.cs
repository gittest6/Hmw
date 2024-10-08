using Microsoft.Extensions.Configuration;
using Hmw.GuessNum;

IConsoleGame cg;
using (var config = (ConfigurationRoot) new ConfigurationBuilder ()
	.AddJsonFile ("appsettings.json")
	.Build ())
	cg = new GuessNumGame (config);

IConsolePlayer cp = new NamedConsolePlayer ();
cp.play (cg);
