Console.WriteLine("Before DB call");
Task<int> resultTask = MakeDbQuery();
Console.WriteLine("DB call is made");

for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"Iteration {i}");
}

int result = await resultTask;
Console.WriteLine($"result: {result}");


var resultTask1 = Task.Run(() =>
{
    Console.WriteLine($"INSIDE TASK RUN: Thread ID: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine(42);
    return 42;
});

Console.WriteLine($"OUTSIDE TASK RUN: Thread ID: {Thread.CurrentThread.ManagedThreadId}");
var result1 = await resultTask;
Console.WriteLine($"OUTSIDE TASK RUN AFTER AWAIT: Thread ID: {Thread.CurrentThread.ManagedThreadId}");
Console.WriteLine(result);



async Task<int> MakeDbQuery()
{
    await Task.Delay(3000);
    Console.WriteLine("Making DB call...");
    return 42;
}

