using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
var path = Path.Combine(currentPath + @"\JACK8.dat"); // Zmień nazwe pliku, jeśli chcesz inny wynik
var listOfNumbers = new List<Point>();
string line;

using (StreamReader sr = new StreamReader (path))
{
    line = sr.ReadLine();
    while ((line = sr.ReadLine()) is not null)
    {
        var splitLine = line.Split(' ');
        listOfNumbers.Add(new Point
        {
            TerminDostepnosci = int.Parse(splitLine[0]),
            CzasZadania = int.Parse(splitLine[1])
        });

    }
}


//listOfNumbers.ForEach(x => Console.WriteLine($"{x.TerminDostepnosci}, {x.CzasZadania}"));

//podaje do funkcji posortowaną listę po TerminieDostępności
Console.WriteLine($"Wynik: {JacksonRPQ(listOfNumbers.OrderBy(x => x.TerminDostepnosci).ToList())}");

static int JacksonRPQ(List<Point> points)
{
    var cMaxToReturn = 0;
    for (int i = 0; i < points.Count; i++)
    {
        Point point = points[i];
        //Obliczanie Cmax dla każdej iteracji
        cMaxToReturn = Math.Max(point.TerminDostepnosci, cMaxToReturn) + point.CzasZadania;
    }
    //Zwraca ostatnią obliczoną wartość Cmax 
    return cMaxToReturn;  
}

class Point
{
    public int TerminDostepnosci { get; set ; }
    public int CzasZadania { get; set; }

}


