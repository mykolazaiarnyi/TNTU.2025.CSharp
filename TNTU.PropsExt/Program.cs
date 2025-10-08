


int[] arr = new int[2];

var a = arr.Select(x => x * 2);
var b = Enumerable.Select(arr, x => x * 2);

var c = arr.Square();
var d = Extentions.Square(arr);

IPerson person = new ChildPerson()
{
    Name = "John",
    Age = 42
};


public interface IPerson
{
    string Name { get; }

    int PassportNumber { get; }
}

public abstract class Person : IPerson
{
    private string _name = null;

    public string Name 
    { 
        get
        {
            return _name ?? "very long string";
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _name = value;
            }
        }
    }

    //public string get_Name()
    //{
    //    return Name;
    //}
    //public void set_Name(string value)
    //{
    //    if (!string.IsNullOrEmpty(value))
    //    {
    //        Name = value;
    //    }
    //}

    public int Age { get; set; }

    public virtual int PassportNumber { get; }

    public abstract string Email { get; protected set; }
}

class ChildPerson : Person
{
    public override int PassportNumber 
    { 
        get
        {
            return 0;
        }
    }

    public override string Email { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
}



public static class Extentions
{
    public static IEnumerable<int> Square(this IEnumerable<int> source)
    {
        List<int> result = new List<int>();
        foreach (var item in source)
        {
            result.Add(item * item);
        }
        return result;
    }
}
