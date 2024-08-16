
using MauMathGame.Models;

namespace MauMathGame;

public partial class GamePage : ContentPage
{
    public string GameType { get; set; }
    int firstNum = 0;
    int secondNum = 0;
    int score = 0;
    const int totalQUestions = 5;
    int gamesLeft = totalQUestions;
    string[] operands = { "+", "-", "x", "÷" };
    bool isRandom = false;


    public GamePage(string gameType)
	{
        InitializeComponent();
        GameType = gameType;

        if (GameType == "?")
        {
            isRandom = true;
        }


        BindingContext = this;

        CreateNewQuestion();
    }

    private void CreateNewQuestion()
    {

        var rand = new Random();

        // test if random was selected
      if (isRandom)
        {
            GameType = operands[rand.Next(0, operands.Length)];
        }


        // Ternary
        firstNum = GameType != "÷" ? rand.Next(1, 9) : rand.Next(1, 99);
        secondNum = GameType != "÷" ? rand.Next(1, 9) : rand.Next(1, 99);

        if (GameType == "÷")
        {
            while (firstNum < secondNum || firstNum % secondNum != 0)
            {
                firstNum = rand.Next(1, 9);
                secondNum = rand.Next(1, 9);
            }
           
        }

        QuestionLabel.Text = $"{firstNum} {GameType} {secondNum}";
    }

    private void OnAnswerSubmitted(object sender, EventArgs e)
    {
       var answer = Int32.Parse(AnswerEntry.Text);
       var isCorrect = false;

        switch (GameType)
        {
            case "+":
                isCorrect = answer == firstNum + secondNum;
                break;
            case "-":
                isCorrect = answer == firstNum - secondNum;
                break;
            case "x":
                isCorrect = answer == firstNum * secondNum;
                break;
            case "÷":
                isCorrect = answer == firstNum / secondNum;
                break;
            default: 
               
                break;

        }

        ProcessAnswer(isCorrect);
        gamesLeft--;
        AnswerEntry.Text = "";

        if (isRandom)
        {
            GameType = "?";
        }

        if (gamesLeft > 0)
        {
            CreateNewQuestion();
        }
        else
        {
            GameOver();
        }
    }
 
    private void ProcessAnswer(bool isCorrect)
    {
        if (isCorrect)
            score += 1;
        AnswerLabel.Text = isCorrect ? "Correct!" : "Incorrect";

    }

    private void GameOver()     
    {
        GameOperation gameOperation = GameType switch
        {
            "+" => GameOperation.Addition,
            "-" => GameOperation.Subtraction,
            "x" => GameOperation.Multiplication,
            "÷" => GameOperation.Division,
            "?" => GameOperation.Random
        };

        QuestionsArea.IsVisible = false;
        BackToMenuBtn.IsVisible = true;

        GameOverLabel.Text = $"GAME OVER! You scored {score} out of {totalQUestions} right.";

        App.GameRepository.Add(new Game
        {
            DatePlayed = DateTime.Now,
            Type = gameOperation,
            Score = score,

        });
    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Button btn = (Button)sender;   
        Navigation.PushAsync(new MainPage());

    }

}