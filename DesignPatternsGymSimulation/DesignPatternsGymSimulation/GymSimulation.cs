using System;
using System.Collections.Generic;

//Тук използвам Singleton - гарантирам че съществува само една фитнес зала
class FitnessGym
{
    private static FitnessGym instance = null;
    public Instructor Coach { get; }

    private FitnessGym()
    {
        Coach = new Instructor();
    }

    public static FitnessGym GetInstance()
    {
        if (instance == null)
        {
            instance = new FitnessGym();
        }
        return instance;
    }
}

//Тук е Strategy + Observer в класът Instructor
//Целта тук е когато инстукторът смени режима, всички трениращи в залата да бъдат уведомявани.
class Instructor
{
    private List<Trainee> trainees = new List<Trainee>();
    private string currentRegime;

    public void Subscribe(Trainee trainee)
    {
        trainees.Add(trainee);
    }

    public void SetRegime(string regime)
    {
        currentRegime = regime;
        Console.WriteLine($"\n[Инструктор] Нов режим е добавен: {regime}");
        NotifyAll();
    }

    private void NotifyAll()
    {
        foreach (var t in trainees)
        {
            t.Update(currentRegime);
        }
    }
}

// Тук използвам observer(за трениращия) и съм сложил опростен Decorator
class Trainee
{
    private string name;
    private string regime;
    private string program = "Базова програма";

    public Trainee(string name)
    {
        this.name = name;
    }

    public void Update(string newRegime)
    {
        regime = newRegime;
        Console.WriteLine($"[{name}] избра и научи за режима: {regime}");
    }

    public void Customize(bool cardio, bool strength, bool stretch)
    {
        if (cardio)
        {
            program += " + Кардио";
        }
        if (strength)
        {
            program += " + Сила";
        }
        if (stretch)
        {
            program += " + Стречинг";
        }

    }

    public void ShowProgram()
    {
        Console.WriteLine($"\n[{name}] Твоята програма: {program}");
    }
}

// Тук е основната програма с входа
class TrainingRegime
{
    static void Main(string[] args)
    {
        Console.Write("Добре дошли в фитенса: Здрав дух в здраво тяло (помпай смело, помпай здраво)!\n");
        var gym = FitnessGym.GetInstance();
        var coach = gym.Coach;

        Console.Write("Въведи име:");
        var name = Console.ReadLine();
        var trainee = new Trainee(name);
        coach.Subscribe(trainee);

        Console.WriteLine("Избери режим:");
        Console.WriteLine("1 - Сила");
        Console.WriteLine("2 - Издръжливост");
        Console.WriteLine("3 - Отслабване");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                coach.SetRegime("Силови тренировки"); break;
            case "2":
                coach.SetRegime("Издръжливост"); break;
            case "3":
                coach.SetRegime("Горене на мазнини"); break;
            default: coach.SetRegime("Силови тренировки"); break;
        }
        
        Console.Write("\nИскаш ли да добавиш кардио? (y/n): ");
        bool cardio = Console.ReadLine().ToLower() == "y";
        Console.Write("Искаш ли да добавиш упражнения за сила? (y/n): ");
        bool strength = Console.ReadLine().ToLower() == "y";
        Console.Write("А какво ще кажеш за стречинг(загрявка)? (y/n): ");
        bool stretch = Console.ReadLine().ToLower() == "y";

        trainee.Customize(cardio, strength, stretch);
        trainee.ShowProgram();
    }
}
//Тук използвах едни от най-популярните шаблони наречени още GoF(Gang of Four)
//Накратко, имаме една фитнес зала за това използваме - Singleton
//Всички които тренират трябва да се уведомяват - Observer
//Всички трениращи би трябвало да могат да променят своята програма за тренировки и да добавят различни упражнения - Decorator
//И последно режимът на тренировките са стратегии които инструкторът избира.

//Мисля че тази програма съм я направил доста опростена, но същевременно могат да се добавят допълнителни функционалности
//Например мисля че може да се добави Factory шаблон за създаване на различните видове тренировки като Крадио,Стречинг,Cила и тн.
