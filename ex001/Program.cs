int[,] GetPoint2dList(int count, int minValue, int maxValue)
{
  int[,] table = new int[count, 2];

  for (int i = 0; i < count; i++)
  {
    int x = Random.Shared.Next(minValue, maxValue);
    int y = Random.Shared.Next(minValue, maxValue);
    table[i, 0] = x;
    table[i, 1] = y;
  }
  return table;
}

string PrinterList(int[,] points)
{
  string result = String.Empty;

  int row = points.GetLength(0);
  for (int i = 0; i < row; i++)
  {
    result += $"{points[i, 0]};{points[i, 1]}\n";
  }
  return result;
}

void SavePointListTo(string path, string data)
{
  File.WriteAllText(path, data);
}

int[,] LoadPointFromFile(string path)
{
  string[] lines = File.ReadAllLines(path);
  int row = lines.Length;
  int[,] table = new int[row, 2];
  for (int i = 0; i < row; i++)
  {
    string[] point = lines[i].Split(';');
    table[i, 0] = int.Parse(point[0]);
    table[i, 1] = int.Parse(point[1]);
  }
  return table;
}

char[,] GetMap(int mapSize, int[,] points2d)
{
  char[,] map = new char[mapSize, mapSize];
  int count = points2d.GetLength(0);
  for (int i = 0; i < count; i++)
  {
    int x = points2d[i, 0];
    int y = points2d[i, 1];
    map[x, y] = 'x';
  }

  return map;
}

string MapPrinter(char[,] map)
{
  int size = map.GetLength(0);
  string result = String.Empty;

  for (int i = 0; i < size; i++)
  {
    for (int j = 0; j < size; j++)
    {
      if (map[i, j] == 'x') result += $"x";
      else result += $" ";
    }
    result += "\n";
  }
  return result;
}

int mapSize = 20;
int pointCount = 10;   
//int[,] point2dList = GetPoint2dList(pointCount, 0, mapSize);
int[,] point2dList = LoadPointFromFile("points.csv");
//string data = PrinterList(point2dList);
//SavePointListTo("points.csv", data);

char[,] map = GetMap(mapSize, point2dList);
string m = MapPrinter(map);
System.Console.WriteLine(m);

int Input (string msg)
{
  Console.WriteLine(msg);
  return Convert.ToInt32(Console.ReadLine());
}

//AB = √(xb - xa)2 + (yb - ya)2 
int FindMinPair(int[,] point2dList, int indexElem = 0)
{
  double longAtPoint = 0;
  double result = Math.Sqrt(2 * (mapSize * mapSize));
  int rows = point2dList.GetLength(0);
  int columns = point2dList.GetLength(1);
  int minValue = 0;
  for (int i = 0; i < rows; i++)
  {
    for (int j = 1; j < rows; j++)
    {
      longAtPoint = Math.Sqrt(((point2dList[j, 0] - point2dList[i, 0]) 
                              * (point2dList[j, 1] - point2dList[i, 0])) 
                                   + ((point2dList[j, 1] - point2dList[i, 0]) 
                                  * (point2dList[j, 1] - point2dList[i, 0])));
      if (result > longAtPoint)
      {
        result = longAtPoint;
        minValue = j;
      }
    }
  }
  return minValue;
}



void FillFinalMap(int[,] finalMap, int [,]startMap)
{
  int rows = finalMap.GetLength(0);
  int columns = finalMap.GetLength(1);
  for (int i = 0; i < rows; i++)
  {
    for (int j = 0; j < columns; j++)
    {
      if (startMap[i,j]!=0)
      finalMap[i,j] = startMap[i,j];
    }
  }
}

int FinalSummElem = Input("Введите конечное количество магазинов: ");
int [,]finalMap = new int[FinalSummElem,2];

for (int i = 0; i < FinalSummElem-point2dList.GetLength(0); i++)
{
  point2dList[FindMinPair(point2dList),0] = 0;
}
FillFinalMap(finalMap,point2dList);
map = GetMap(mapSize, finalMap);
string n = MapPrinter(map);
System.Console.WriteLine(n);