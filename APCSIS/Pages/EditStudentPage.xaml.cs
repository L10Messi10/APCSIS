using static APCSIS.Includes.PublicVariables;
using APCSIS.Models;
namespace APCSIS.Pages;

public partial class EditStudentPage : ContentPage
{
    private Students _students = new();
	public EditStudentPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        txtid.Text = _id;
        txtfname.Text = first_name;
        txtlname.Text = last_name;
        txtaddress.Text = address;
        txtconNum.Text = con_num;
    }

    private async void Btnupdate_OnClicked(object? sender, EventArgs e)
    {
        var updated = await _students.SaveEditedStudent(txtid.Text, txtfname.Text, txtlname.Text, txtaddress.Text,
            txtconNum.Text);
        if (updated == true)
        {
            await DisplayAlert("Update", "Data updated successfully!", "Got it!");
        }
        else
        {
            await DisplayAlert("Error", "An error occurred during the updated process. Please try again later", "Got it!");
        }
    }
}