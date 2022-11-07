using System.Text.Json;
using Newtonsoft.Json;
using System.Net.Http;
using static Calculator.QuizPage;
using System.Runtime.CompilerServices;
//using TodoRest.models;

namespace Calculator;

public partial class QuizPage : ContentPage
{

    string[] answer1 = new string[10];           // = {"Demo1", "2.1", "3.1","4.1"};  //maybe make these arrays to store al
    string[] answer2 = new string[10];         //= { "Demo2","2.2","3.2","4.2"};
    string[] answer3 = new string[10] ;       // = { "Demo3", "2.3","3.3","4.3" };
    string[] operand1 = new string[10];       // = { "2", "4","6","8" };
    string[] operand2 = new string[10];       // = { "3","5","7","9" };
    string[] OperatorValue = new string[10];  // = { "+","-","*","%" };
    int IterationVal = 0;


   // public List<MathExcerciseQuestions> Items { get; private set; }
    public QuizPage()
	{
		InitializeComponent();

        //grab 10 api values and place it in data

        MathsExercise[] mathExercises = APICall();

        

        for (int i = 0; i < mathExercises.Length; i++)
        {
            answer1[i] = Convert.ToString(mathExercises[i].result);
            answer2[i] = Convert.ToString(mathExercises[i].fakeResult1);
            answer3[i] = Convert.ToString(mathExercises[i].fakeResult2);
            operand1[i] = Convert.ToString(mathExercises[i].operand1);
            operand2[i] = Convert.ToString(mathExercises[i].operand2);
            OperatorValue[i] = Convert.ToString(mathExercises[i].operation);

            
        }



        AssignAPIText(0);
    }

    public MathsExercise[] APICall()
    {
        ExerciseFetcher exerciseFetcher = new();
        MathsExercise[] oldMathExercises = exerciseFetcher.GetMathExercises();
        exerciseFetcher.RefreshExercises();

        MathsExercise[] mathExercises = oldMathExercises;

        // Maybe add a timeout here?
        while (mathExercises == oldMathExercises)
            mathExercises = exerciseFetcher.GetMathExercises();
        return mathExercises;

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

    public void UIquestionsLayoutUpdate(bool answer, bool skipped,bool selected)
    {
        
        if (IterationVal==0)
        {
            LayoutUpdates(UIB1, answer, skipped, selected);

        }else if (IterationVal==1)
        {
            LayoutUpdates(UIB2, answer ,skipped, selected);
        }
        else if (IterationVal==2)
        {
            LayoutUpdates(UIB3, answer,skipped,selected);
        }
        else if (IterationVal == 3)
        {
            LayoutUpdates(UIB4, answer,skipped,selected);
        }
        else if (IterationVal==4)
        {
            LayoutUpdates(UIB5, answer,skipped,selected);
        }
        else if (IterationVal == 5)
        {
            LayoutUpdates(UIB6, answer,skipped,selected);
        }
        else if (IterationVal == 6)
        {
            LayoutUpdates(UIB7, answer,skipped,selected);
        }
        else if (IterationVal == 7)
        {
            LayoutUpdates(UIB8, answer,skipped,selected);
        }
        else if (IterationVal ==8)
        {
            LayoutUpdates(UIB9, answer,skipped,selected);
        }
        else if (IterationVal== 9)
        {
            LayoutUpdates(UIB10, answer ,skipped,selected);
        }

      //  UIquestions.backgroundcolor need to change background color of the button

    }

    public void LayoutUpdates(Button UIButton, bool answer,bool skipped,bool selected)
    {

        if (!skipped && !selected)
        {
            if (answer)
            {
                UIButton.BackgroundColor = Color.FromRgb(15, 200, 15);
                UIButton.HeightRequest = 40;
                UIButton.WidthRequest = 40;
                UIButton.CornerRadius = 15;

            }
            else
            {
                UIButton.BackgroundColor = Color.FromRgb(200, 15, 15);
            }
        }else if (skipped)
        {
            UIButton.HeightRequest = 40;
            UIButton.WidthRequest = 40;
            UIButton.CornerRadius = 15;

        }
        if (selected)
        {
            UIButton.HeightRequest = 50;
            UIButton.WidthRequest = 50;
            UIButton.CornerRadius = 25;
        }
    }
    private void Button1_Clicked(object sender, EventArgs e)
    {
        string SelectedValue = Button1.Text;

        



       // string AnswerValue = answer1; 
        if (IterationVal < 10 && SelectedValue.Contains(answer1[IterationVal]))
        {
            Button1.BackgroundColor = Color.FromRgb(15, 200, 15);
            UIquestionsLayoutUpdate(true, false, false);
            //display message that correct answer chosen
            NextQuestion(sender,e);
        }
        else
        {
            UIquestionsLayoutUpdate(false, false, false);
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
        if (IterationVal < 10 && SelectedValue.Contains(answer1[IterationVal]))  //error on this line!!!!!! overflow from iteration value
        {
            Button2.BackgroundColor = Color.FromRgb(15, 200, 15);
            UIquestionsLayoutUpdate(true, false, false);
            //display message that currect answer chosen
            NextQuestion(sender, e);
        }
        else
        {
            //display message saying wrong answer chosen
            UIquestionsLayoutUpdate(false, false, false);
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
        if (IterationVal < 10 && SelectedValue.Contains(answer1[IterationVal]))
        {
            Button3.BackgroundColor = Color.FromRgb(15, 200, 15);
            UIquestionsLayoutUpdate(true, false, false);
            //display message that currect answer chosen
            NextQuestion(sender, e);
        }
        else
        {
            UIquestionsLayoutUpdate(false, false, false);
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

        if (true)//await this.DisplayAlert("Correct Answer", "Would you like to go to the next question?", "Yes", "No"))
        {
           
            ButtonSkipToNext.IsVisible = false;
            unlockButtons();
            //refresh page with next api question set
          
            IterationVal++;
           
            if (IterationVal < 10) {
                UIquestionsLayoutUpdate(false, false, true);
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
        UIquestionsLayoutUpdate(false, true, false);
        unlockButtons();
      
        IterationVal++;

        
        if (IterationVal < 10)
        {
            UIquestionsLayoutUpdate(false, false, true);
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
       // UIquestionsLayoutUpdate(false, false, false);
    }
}