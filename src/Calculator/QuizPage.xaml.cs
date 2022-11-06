using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using static Calculator.QuizPage;
//using TodoRest.models;

namespace Calculator;

public partial class QuizPage : ContentPage
{

    string[] answer1 = {"Demo1", "2.1", "3.1","4.1"};  //maybe make these arrays to store al
    string[] answer2 = { "Demo2","2.2","3.2","4.2"};
    string[] answer3 = { "Demo3", "2.3","3.3","4.3" };
    string[] operand1 = { "2", "4","6","8" };
    string[] operand2 = { "3","5","7","9" };
    string[] OperatorValue = { "+","-","*","%" };
    int IterationVal = 0;


   // public List<MathExcerciseQuestions> Items { get; private set; }
    public QuizPage()
	{
		InitializeComponent();

        //grab 10 api values and place it in data

        APICall();



        AssignAPIText(0);



    }

    async public void APICall()
    {
        var httpClient = new HttpClient();
        var resultJson = await httpClient.GetStringAsync("https://localhost:7172/api/mathspractice");
        var resultMathExcercise = JsonConvert.DeserializeObject<MathExcercise[]>(resultJson); 

    }
    private void AssignAPIText(int IndexVal)
    {

        //IndexVal is the value of the API object index that will contain the specific question

        //first assign the question text box from hte API

        QuestionLabel.Text = $"{operand1[IndexVal]} {OperatorValue[IndexVal]} {operand2[IndexVal]} = ?";

        //Second assign the buttons randomly
        int val;
        

        
        int i;
        int breakcondition = 0; //will be used in for loop to be incremented and be the ending condition

        Random r = new Random();
        val = r.Next(0, 3);     //grab a random starting point of the array 
        string[] ButtonValues = { answer1[IndexVal], answer2[IndexVal], answer3[IndexVal] };    //assign answers to an array 

        for (i = val; breakcondition < 3; i++)  //iterate through the array from the starting point, using % to not go over the array size limit
        {
            val = i % 3;
           
            ButtonAssignment(val, ButtonValues[breakcondition]); //assign answers to random spot
            breakcondition++;
        }


    }


    private void ButtonAssignment(int ButtonNumber, string GivenValue)
    {
        //This function will recieve a value from 0-2 which will indicate what button will be assigned the givenvalue variable
        if (ButtonNumber == 0)
        {
            Button1.Text = $"A.) {GivenValue}";
        }
        else if (ButtonNumber == 1)
        {
            Button2.Text = $"B.) {GivenValue}";
        }
        else if (ButtonNumber == 2)
        {
            Button3.Text = $"C.) {GivenValue}";
        }

    }


    private void Button1_Clicked(object sender, EventArgs e)
    {
        string SelectedValue = Button1.Text;
       // string AnswerValue = answer1; 
        if (SelectedValue.Contains(answer1[IterationVal]))
        {
            Button1.BackgroundColor = Color.FromRgb(15, 200, 15);
            //display message that currect answer chosen
            NextQuestion(sender,e);
        }
        else
        {
            //display message saying wrong answer chosen
            Button1.BackgroundColor = Color.FromRgb(200, 15, 15);
            lockButtons();
            ButtonTryAgain.IsVisible = true;
            ButtonSkipToNext.IsVisible = true;

        }
    }

    private void Button2_Clicked(object sender, EventArgs e)
    {
        string SelectedValue = Button2.Text;
        // string AnswerValue = answer1; 
        if (SelectedValue.Contains(answer1[IterationVal]))
        {
            Button2.BackgroundColor = Color.FromRgb(15, 200, 15);
            //display message that currect answer chosen
            NextQuestion(sender, e);
        }
        else
        {
            //display message saying wrong answer chosen
            Button2.BackgroundColor = Color.FromRgb(200, 15, 15);
            lockButtons();
            ButtonTryAgain.IsVisible = true;
            ButtonSkipToNext.IsVisible = true;
        }
    }

    private void Button3_Clicked(object sender, EventArgs e)
    {
        string SelectedValue = Button3.Text;
        // string AnswerValue = answer1; 
        if (SelectedValue.Contains(answer1[IterationVal]))
        {
            Button3.BackgroundColor = Color.FromRgb(15, 200, 15);
            //display message that currect answer chosen
            NextQuestion(sender, e);
        }
        else
        {
            //display message saying wrong answer chosen
            Button3.BackgroundColor = Color.FromRgb(200,15,15);
            lockButtons();
            ButtonTryAgain.IsVisible = true;
            ButtonSkipToNext.IsVisible = true;
        }
    }

    async void NextQuestion(object sender, System.EventArgs e)
    {
        lockButtons();

        ButtonSkipToNext.IsVisible = true;

        if (await this.DisplayAlert("Correct Answer", "Would you like to go to the next question?", "Yes", "No"))
        {
            ButtonSkipToNext.IsVisible = false;
            unlockButtons();
            //refresh page with next api question set
            IterationVal++;
            if (IterationVal < 2) {
                AssignAPIText(IterationVal);
            }
            else
            {
                //notify that they have done all the questions available.
                this.DisplayAlert("All Questions Answered", "Congrats, you have finished all of the questions!", "Close");
            }
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
/*
    public class MathExcerciseQuestions
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public char operation { get; set; }
        public double result { get; set; }
        public double fakeResult1 {get; set;}
        public double fakeResult2 { get; set; }
    }
*/
    private void ButtonTryAgain_Clicked(object sender, EventArgs e)
    {
        unlockButtons();
        ButtonSkipToNext.IsVisible = false;
        ButtonTryAgain.IsVisible = false;
    }

    private void ButtonSkipToNext_Clicked(object sender, EventArgs e)
    {
        unlockButtons();
        IterationVal++;
        if (IterationVal < 2)
        {
            AssignAPIText(IterationVal);
        }
        else
        {
            //notify that they have done all the questions available.
            this.DisplayAlert("All Questions Answered", "Congrats, you have finished all of the questions!","Close");
        }
        ButtonSkipToNext.IsVisible = false;
        ButtonTryAgain.IsVisible = false;

    }

    public void lockButtons()
    {
        Button1.IsEnabled=false; Button2.IsEnabled=false; Button3.IsEnabled=false;

    }

    public void unlockButtons()
    {
        Button1.IsEnabled = true; Button2.IsEnabled=true; Button3.IsEnabled=true;
        Button1.BackgroundColor = Color.FromRgb(255, 255, 255); Button2.BackgroundColor = Color.FromRgb(255, 255, 255); Button3.BackgroundColor = Color.FromRgb(255,255,255);

    }
}