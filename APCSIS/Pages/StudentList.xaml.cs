using System.Collections.ObjectModel;
using APCSIS.Models;
using Firebase.Database;
using Firebase.Database.Query;
using static APCSIS.Includes.PublicVariables;

namespace APCSIS.Pages
{
    public partial class StudentList : ContentPage
    {
        private Students _students = new();
        private FirebaseClient client; // Firebase client initialization
        private bool IsLoading; // Prevents duplicate loading during pagination
        private string lastKey = null; // Track the last loaded item key (for pagination)

        // private const int pageSize = 10;
        public ObservableCollection<Students> StudentsList { get; set; }

        public StudentList()
        {
            InitializeComponent();
            StudentsList = new ObservableCollection<Students>(); // Initialize the ObservableCollection
            ListStudents.ItemsSource = StudentsList; // Bind the collection to the ListView or CollectionView
            client = new FirebaseClient("Your Firebase URL"); // Initialize Firebase client with your URL
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Load initial data if the list is empty
            if (StudentsList.Count == 0)
            {
                await LoadMoreDataAsync();
            }
        }

        // Fetch more students (with pagination)
        public async Task LoadMoreDataAsync()
        {
            if (IsLoading)
                return; // Avoid loading when already fetching

            IsLoading = true;

            try
            {
                // Build the query to fetch students starting from the lastKey
                var query = client.Child("Students").OrderByKey(); // Order by key, which is typically the ID in Firebase

                // If there's a lastKey, use it to fetch data after the last item
                if (!string.IsNullOrEmpty(lastKey))
                {
                    //query = query.StartAt(lastKey); // Start the next fetch after the last key
                }

                // Fetch the students from Firebase (limit results by pageSize)
                //var students = (await query.OnceAsync<Students>())
                //    .Select(item => new Students
                //    {
                //        ID = item.Object.ID,
                //        FirstName = item.Object.FirstName,
                //        LastName = item.Object.LastName,
                //        Address = item.Object.Address,
                //        ConNum = item.Object.ConNum
                //    }).Take(pageSize).ToList(); // Limit the number of results to pageSize

                // Add fetched students to the ObservableCollection
                //foreach (var student in students)
                //{
                //    StudentsList.Add(student);
                //}

                //// If students were fetched, update the last key to the last fetched student's ID
                //if (students.Any())
                //{
                //    lastKey = students.Last().ID; // Update the lastKey to the ID of the last fetched student
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading students: {ex.Message}");
            }
            finally
            {
                IsLoading = false; // Set loading to false once done
            }
        }

        // Event handler for when a student item is selected
        private async void ItemSelect_OnInvoked(object? sender, EventArgs e)
        {
            var item = sender as SwipeItemView;
            if (!(item?.BindingContext is Students getStudents))
                return;

            string _id = getStudents.ID; // Store the student ID
            string student_key = await _students.GetStudentKey(_id); // Assuming GetStudentKey fetches student key from Firebase

            if (student_key == null)
            {
                await DisplayAlert("No Data", "No Data found!", "Got it!");
            }
            else
            {
                await Navigation.PushModalAsync(new EditStudentPage());
            }
        }

        // Event handler for when the user scrolls to the bottom of the list (pagination)
        private async void ListStudents_OnRemainingItemsThresholdReached(
            object? sender,
            EventArgs e
        )
        {
            // Load more data when user scrolls to the bottom
            await LoadMoreDataAsync();
        }
    }
}
