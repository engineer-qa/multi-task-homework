int barberCapacity = 1;
int waitingChairsCapacity = 3;
int totalClients = 10;

Semaphore barber = new Semaphore(barberCapacity, barberCapacity);
Semaphore waitingChairs = new Semaphore(waitingChairsCapacity, waitingChairsCapacity);

for (int i = 1; i < totalClients; i++)
{
    int id = i;
    new Thread(() => GetHaircut(id)).Start();
}

void GetHaircut(int clientId)
{
    Console.WriteLine($"Клиент {clientId} заходит в барбершоп");

    if (!waitingChairs.WaitOne(0))
    {
        Console.WriteLine($"Клиент {clientId} уходит, так как нет свободных мест");
        return;
    }
    Console.WriteLine($"Клиент {clientId} садится в зал ожидания");

    barber.WaitOne();
    waitingChairs.Release();

    Console.WriteLine($"Клиент {clientId} стрижется");
    Thread.Sleep(new Random().Next(1000, 3000));
    Console.WriteLine($"Клиент {clientId} выходит из барбершопа");

    barber.Release();
}
