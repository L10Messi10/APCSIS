using static APCSIS.Includes.PublicVariables;
using APCSIS.Models;


namespace APCSIS.Pages;

public partial class StudentList : ContentPage
{
	private Students _students = new();
	public StudentList()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GetStudentList();
    }


    async void GetStudentList()
	{
        ListStudents.ItemsSource = await _students.GetStudents();

    }

    private async void ItemSelect_OnInvoked(object? sender, EventArgs e)
    {
        var item = sender as SwipeItemView;
        if (!(item?.BindingContext is Students getStudents)) return;
        _id = getStudents.ID;
        //await DisplayAlert("Key", _id, "OK");
        student_key = await _students.GetStudentKey(_id);
        if(student_key == null)
        {
            await DisplayAlert("No Data", "No Data found!", "Got it!");
        }
        else
        {
            await Navigation.PushModalAsync(new EditStudentPage());
        }

    }
}