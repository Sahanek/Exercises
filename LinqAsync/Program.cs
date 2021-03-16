using System;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

Console.WriteLine("Hello World!");

var fileContent = await File.ReadAllTextAsync("data.json");
var cars = JsonSerializer.Deserialize<CarData[]>(fileContent);

// Print all cars with at least 4 doors

//var carWithAtLeastFourDoor = cars.Where(car => car.NumberOfDoors >= 4);
//foreach (var car in carWithAtLeastFourDoor)
//{
//    Console.WriteLine($"The car {car.Model} has {car.NumberOfDoors} doors.");
//}

//cars.OrderByDescending(car => car.HP).Take(10);

// Display the number of models per make that appeared after 1995
//cars.GroupBy(car => car.Make)
//    .Select(c => new { c.Key, NumberOfModels = c.Count(car => car.Year >= 2008) })
//    .ToList()
//    .ForEach(item => Console.WriteLine($"{item.Key}, {item.NumberOfModels}"));

// Display a list of makes that have at least 2 models with >= 400hp

//cars.Where(car => car.HP >= 400)
//    .GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, NumberOFPowerfulCars = car.Count() })
//    .Where(make => make.NumberOFPowerfulCars >= 2)
//    .ToList()
//    .ForEach(make => Console.WriteLine(make.Make));

//// Display the average hp per make
//cars.GroupBy(car => car.Make)
//    .Select(car => new { Make = car.Key, AverageHP = car.Average(c => c.HP) })
//    .ToList()
//    .ForEach(make => Console.WriteLine($"{make.Make}: {make.AverageHP}"));

// How many makes build cars with hp between 0..100, 101..200, 201...300, 301..400, 401..500

cars.GroupBy(car => car.HP switch
    {
        <= 100 => "0..100",
        <= 200 => "101..200",
        <= 300 => "201..300",
        <= 400 => "301..400",
        _ => "401..500"
    })
    .Select(car => new
    {
        HPCategory = car.Key,
        NumbersOfMake = car.Select(c => c.Make).Distinct().Count()
    })
    .ToList()
    .ForEach(c => Console.WriteLine($"{c.HPCategory}: {c.NumbersOfMake}"));

internal class CarData
{
    [JsonPropertyName("id")]
    public int ID { get; set; }

    [JsonPropertyName("car_make")]
    public string Make { get; set; }

    [JsonPropertyName("car_models")]
    public string Model { get; set; }

    [JsonPropertyName("car_year")]
    public int Year { get; set; }

    [JsonPropertyName("number_of_doors")]
    public int NumberOfDoors { get; set; }

    [JsonPropertyName("hp")]
    public int HP { get; set; }
}