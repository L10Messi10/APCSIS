using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APCSIS.Includes.PublicVariables;


namespace APCSIS.Models
{
    public class Students
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ConNum { get; set; }

        //public async Task<List<Students>> GetStudents()
        //{
        //    try
        //    {
        //        return (await client
        //            .Child("Students")
        //            .OnceAsync<Students>()).Select(item => new Students
        //            {
        //                ID = item.Object.ID,
        //                FirstName = item.Object.FirstName,
        //                LastName = item.Object.LastName,
        //                Address = item.Object.Address,
        //                ConNum = item.Object.ConNum

        //            }).ToList();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<List<Students>> GetStudents(int limit, int offset)
        {
            try
            {
                // Replace this with your actual Firebase fetching logic
                var students = (await client
                        .Child("Students")
                        .OrderByKey()  // Or any other ordering logic you want
                        .StartAt(offset.ToString()) // Use the offset to paginate
                        .LimitToFirst(limit)
                        .OnceAsync<Students>())
                    .Select(item => new Students
                    {
                        ID = item.Object.ID,
                        FirstName = item.Object.FirstName,
                        LastName = item.Object.LastName,
                        Address = item.Object.Address,
                        ConNum = item.Object.ConNum
                    }).ToList();

                return students;
            }
            catch
            {
                return null;
            }
        }





        public async Task<string> GetStudentKey(string _id)
        {
            try
            {
                var getstudentkey = (await client
                    .Child("Students")
                    .OnceAsync<Students>()).FirstOrDefault
                    (a => a.Object.ID == _id);
                if (getstudentkey != null)
                {
                    client.Dispose();
                    first_name = getstudentkey.Object.FirstName;
                    last_name = getstudentkey.Object.LastName;
                    address = getstudentkey.Object.Address;
                    con_num = getstudentkey.Object.ConNum;
                    student_key = getstudentkey.Key;
                    return getstudentkey.Key;
                }
                client.Dispose();
                return null;
            }
            catch (Exception)
            {
                client.Dispose();
                return null;
            }
        }


        public async Task<bool> SaveEditedStudent(string id, string fname, string lname, string add, string conNum)
        {
            try
            {
                var evaluateID = (await client
                    .Child("Students")
                    .OnceAsync<Students>()).FirstOrDefault
                    (a => a.Object.ID == id);
                if (evaluateID != null)
                {
                    var students = new Students
                    {
                        ID = id,
                        FirstName = fname,
                        LastName = lname,
                        Address = add,
                        ConNum = conNum
                    };
                    await client
                        .Child("Students")
                        .Child(student_key)
                        .PatchAsync(students);
                    client.Dispose();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                client.Dispose();
                return false;
            }
        }

    }
}
