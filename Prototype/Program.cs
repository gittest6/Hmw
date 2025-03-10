using System;
using System.Text.Json;
using Hmw.Prototype;

var building = new Building ("concrete", 100, "residential");

var building2 = building.MyClone ();
building2.Square = 200;
building2.Purpose = "commercial";

var building3 = (Building) building.Clone ();
building3.Square = 150;

Console.WriteLine (nameof (building) + "\n" + JsonSerializer.Serialize (building));
Console.WriteLine (nameof (building2) + "\n" + JsonSerializer.Serialize (building2));
Console.WriteLine (nameof (building3) + "\n" + JsonSerializer.Serialize (building3));


var supermarket = new Supermarket ("brick", 300, "commercial", "CoolStore", 8, 23, ["Meat", "Bread", "Milk", "Hygiene", "Vegetable oils"]);

var supermarket2 = supermarket.MyClone ();
supermarket2.Name = "BigShop";
supermarket2.OpenHour = 9;
supermarket2.CloseHour = 22;
supermarket2.Sections = ["Food", "Car accessories", "Kitchenware"];

var supermarket3 = (Supermarket) supermarket2.Clone ();
supermarket3.Name = "BigShop 2";

Console.WriteLine ();
Console.WriteLine (nameof (supermarket) + "\n" + JsonSerializer.Serialize (supermarket));
Console.WriteLine (nameof (supermarket2) + "\n" + JsonSerializer.Serialize (supermarket2));
Console.WriteLine (nameof (supermarket3) + "\n" + JsonSerializer.Serialize (supermarket3));
