namespace MauMathGame
{
    public partial class MainPage : ContentPage
    {     
      public MainPage()
        {
         InitializeComponent();
        

        }   

        private void OnChosenGame(object sender, EventArgs e)
        {
           Button btn = (Button)sender; // converts anonomous sender object via explicit cast

            Navigation.PushAsync(new GamePage(btn.Text));

        }
        private void OnViewPreviousGameChosen(object sender, EventArgs e)
        {
            Button btn = (Button)sender; // converts anonomous sender object via explicit cast
            

            Navigation.PushAsync(new PreviousGames());

        }
    }
}
