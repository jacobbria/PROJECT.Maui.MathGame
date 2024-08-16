using Windows.Gaming.Preview.GamesEnumeration;
using MauMathGame.Models;
using System;

namespace MauMathGame;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
    {
        InitializeComponent();
       
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

    private void OnDelete(Object sender, EventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;  

        App.GameRepository.Delete((int)imgBtn.BindingContext);

        gamesList.ItemsSource = App.GameRepository.GetAllGames().ToList();  
    }
}