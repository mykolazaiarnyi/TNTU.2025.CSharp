// See https://aka.ms/new-console-template for more information

int[] arr = [1, 5, 1, 34, 6, 2, 4, 65, 345, 534];

var result = from num in arr
             where IsEven(num)
             orderby num descending
             select Square(num);

var result2 = arr.Where(IsEven)
    .OrderByDescending(GetValue)
    .Select(Square)
    .ToArray();

Enumerable.ToArray(Enumerable.Select(Enumerable.OrderByDescending(Enumerable.Where(arr, IsEven), GetValue), Square));

var result21 = arr.Where(num => num % 0 == 0)
    .OrderByDescending(num => num)
    .Select(num => num)
    .ToArray();

static bool IsEven(int num)
{
    return num % 2 == 0;
}

static int Square(int num)
{
    return num * num;
}

static int GetValue(int num)
{
    return num;
}

