using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using static Calculator.QuizPage;
//using TodoRest.models;

namespace Calculator;

public partial class QuizPage : ContentPage
{

    string answer1  = "Demo1";  //maybe make these arrays to store al
    string answer2 = "Demo2";
    string answer3 = "Demo3";
    string operand1 = "2";
    string operand2 = "3";
    string OperatorValue = "+";


   // public List<MathExcerciseQuestions> Items { get; private set; }
    public QuizPage()
	{
		InitializeComponent();

        //grab 10 api values and place it in data
        
      //  RefreshDataAsync();





        QuestionLabel.Text = $"{operand1} {OperatorValue} {operand2} = ?";
        Button1.Text = $"A.) {answer1}";
        Button2.Text = $"B.) {answer2}";
        Button3.Text = $"C.) {answer3}";

    }

    private void Button1_Clicked(object sender, EventArgs e)
    {
        string SelectedValue = Button1.Text;
       // string AnswerValue = answer1; 
        if (SelectedValue.Contains(answer1))
        {
            //display message that currect answer chosen
            NextQuestion(sender,e);
        }
        else
        {
            //display message saying wrong answer chosen
        }
    }

    private void Button2_Clicked(object sender, EventArgs e)
    {

    }

    private void Button3_Clicked(object sender, EventArgs e)
    {

    }

    async void NextQuestion(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert("Correct Answer", "Would you like to go to the next question?", "Yes", "No"))
        {
            //refresh page with next api question set
        }
    }

/*
    public async Task<List<MathExcerciseQuestions>> RefreshDataAsync()
    {
        Items = new List<MathExcerciseQuestions>();

        Uri uri = new Uri("https://localhost:7172/api/mathspractice");
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Items = JsonSerializer.Deserialize<List<MathExcerciseQuestions>>(content, _serializerOptions);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
        }
        return Items;
    }
  */

    public class MathExcerciseQuestions
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public char operation { get; set; }
        public double result { get; set; }
        public double fakeResult1 {get; set;}
        public double fakeResult2 { get; set; }
    }

}