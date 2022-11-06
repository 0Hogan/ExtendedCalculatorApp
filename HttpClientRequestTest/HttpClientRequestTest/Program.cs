using Newtonsoft.Json;

class Program
{
    public static void Main()
    {
        ExerciseFetcher exerciseFetcher = new();
        MathsExercise[] oldMathExercises = exerciseFetcher.GetMathExercises();
        exerciseFetcher.RefreshExercises();

        MathsExercise[] mathExercises = oldMathExercises;

        // Maybe add a timeout here?
        while (mathExercises == oldMathExercises)
            mathExercises = exerciseFetcher.GetMathExercises();

        foreach (var mathExercise in mathExercises)
            mathExercise.PrintToConsole();
    }
}

/*
        ExerciseFetcher exerciseFetcher = new();
        MathsExercise[] oldMathExercises = exerciseFetcher.GetMathExercises();
        exerciseFetcher.RefreshExercises();

        MathsExercise[] mathExercises = oldMathExercises;

        // Maybe add a timeout here?
        while (mathExercises == oldMathExercises)
            mathExercises = exerciseFetcher.GetMathExercises();
 */

class ExerciseFetcher
{
    MathsExercise[] mathsExercises = new MathsExercise[0];

    public async void RefreshExercises()
    {
        string URL = "https://localhost:7172/api/mathspractice";
        HttpClient client = new();

        HttpResponseMessage response = client.GetAsync(URL).Result;

        if (response.IsSuccessStatusCode)
        {
            HttpContent content = response.Content;
            string jsonString = await content.ReadAsStringAsync();

            mathsExercises = JsonConvert.DeserializeObject<MathsExercise[]>(jsonString);
        }
        else
            throw new Exception("Ack! We failed to get a response!");
    }

    public MathsExercise[] GetMathExercises()
    {
        return mathsExercises;
    }
}

public class MathsExercise
{
    public double operand1;
    public double operand2;
    public char operation;
    public double result;
    public double fakeResult1;
    public double fakeResult2;

    public MathsExercise() { }
    public MathsExercise(double operand1, double operand2, char operation, double result, double fakeResult1, double fakeResult2)
    {
        this.operand1 = operand1;
        this.operand2 = operand2;
        this.operation = operation;
        this.result = result;
        this.fakeResult1 = fakeResult1;
        this.fakeResult2 = fakeResult2;
    }
    public void PrintToConsole()
    {
        Console.WriteLine(operand1 + " " + operation + " " + operand2 + " = (" + result + ", " + fakeResult1 + ", " + fakeResult2 + ")?");
    }
}