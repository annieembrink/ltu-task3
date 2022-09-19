using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RegistrationAppNew
{
    public partial class Form1 : Form
    {

        //Creating a class
        public class Person
        {
            public string firstname;
            public string lastname;
            public string id;
            public string sex;
        }

        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
        }

        static bool InputIsFilled(string firstname, string lastname, string id)
        {
            //Check if fields are filled
            if (firstname == "" || lastname == "" || id == "")
            {
                //If not, tell the user to do so
                MessageBox.Show("All fields have to be filled");
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool CorrectFormatName(string name)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show($"Firstname and lastname: Only letters allowed");
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool CorrectFormatId(string id)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(id, "^[0-9]*$"))
            {
                MessageBox.Show($"ID: Only numbers allowed");
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool AllFormatOk(string firstname, string lastname, string id)
        {
            if (CorrectFormatName(firstname) == false || CorrectFormatName(lastname) == false || CorrectFormatId(id) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool CorrectIdLenght(string id)
        {
            //Check that id consists of ten numbers
            if (id.Length != 10)
            {
                MessageBox.Show("ID number has to be 10 numbers, format YYMMDDXXXX");
                return false;
            }
            else
            {
                return true;
            }
        }

        static string MaleOrFemale(string id)
        {
            string str;

            //Check if its male or female
            if (id[8] % 2 == 0)
            {
                str = "female";
            }
            else
            {
                str = "male";
            }
            return str;
        }

        static void SeparateId(List<int> list, List<int> list2)
        {
            foreach (int nr in list)
            {
                //if nr is more than one nr e.g. 18...
                if (nr.ToString().Length > 1)
                {
                    //Separate it (18 --> 1, 8)
                    for (int i = 0; i < nr.ToString().Length; i++)
                    {
                        char nr2 = nr.ToString()[i];

                        //Add it to the new list
                        list2.Add(int.Parse(nr2.ToString()));
                    }
                }
                else
                {
                    list2.Add(nr);
                }
            }
        }

        static bool TotalOfNumbers(List<int> list2)
        {
            //Sum up all numbers in the list
            var total = 0;
            foreach (int item in list2)
            {
                total += item;
            }

            //Check if total is evenly divisible with 10
            if (total % 10 == 0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("The ID number is not correct, try again");
                return false;
            }
        }
        static bool CorrectId(string id)
        {
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();

            //For every char in the string...
            for (int i = 0; i < id.Length; i++)
            {
                //Convert char into int (so we can count on it later)
                int nr = int.Parse(id[i].ToString());

                //If index in the loop is divisible with two (i = 0, 2, 4 etc) then add to list
                if (i % 2 == 0)
                {
                    nr = nr * 2;
                    list.Add(nr);
                }
                //same but if i is divisible with one
                else
                {
                    nr = nr * 1;
                    list.Add(nr);
                }
            }
            SeparateId(list, list2);

            if (TotalOfNumbers(list2) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            //When submitbutton is clicked
            //Clear resultbox
            resultBox.Clear();

            //Firstname and lastname should have inital letter uppercase and rest of string lowercase
            firstNameInput.Text = char.ToUpper(firstNameInput.Text[0]) + firstNameInput.Text.Substring(1).ToLower();
            lastNameInput.Text = char.ToUpper(lastNameInput.Text[0]) + lastNameInput.Text.Substring(1).ToLower();

            //Check if all input fields are filled in
            if (InputIsFilled(firstNameInput.Text, lastNameInput.Text, idInput.Text))
            {
                //Check if format is ok (name should be letters and id should be numbers)
                if (AllFormatOk(firstNameInput.Text, lastNameInput.Text, idInput.Text))
                {
                    //Check if id is 10 numbers
                    if (CorrectIdLenght(idInput.Text))
                    {
                        //Check if id is correct
                        if (CorrectId(idInput.Text))
                        {
                            //If all fields are filled and have right format, create a new object, a person
                            Person p1 = new Person();
                            p1.firstname = firstNameInput.Text;
                            p1.lastname = lastNameInput.Text;
                            p1.id = idInput.Text;

                            //If everything is fine, write the result in the resultbox
                            resultBox.Text += ($"Name: {p1.firstname} {p1.lastname}\r\n");
                            resultBox.Text += ($"ID: {p1.id}\r\n");
                            resultBox.Text += ($"Sex: {MaleOrFemale(idInput.Text)}");

                            //When result is displayed, clear all input fields
                            firstNameInput.Clear();
                            lastNameInput.Clear();
                            idInput.Clear();
                        }
                    }
                }
            }
        }
        //Menu
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Display all fields
            panel1.Visible = true;

            //Clear all fields
            resultBox.Clear();
            firstNameInput.Clear();
            lastNameInput.Clear();
            idInput.Clear();
        }
    }
}

