using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace BindingList
{
    public partial class Form1 : Form
    {
        private BindingList<Part> parts;
        private readonly Random randomNumber = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Part newPart = parts.AddNew();

            if (newPart.PartName.Contains(" "))
            {
                MessageBox.Show("Part names cannot contain spaces.");

                parts.CancelNew(parts.IndexOf(newPart));
            }
            else
            {
                parts.EndNew(parts.IndexOf(newPart));

                textBox2.Text = randomNumber.Next(9999).ToString();
                textBox1.Text = "Enter part name";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeListOfParts();

            listBox1.DataSource = parts;
            listBox1.DisplayMember = "PartName";

            parts.AddingNew += listOfParts_AddingNew;
            parts.ListChanged += listOfParts_ListChanged;
        }

        private void InitializeListOfParts()
        {
            parts = new BindingList<Part>
            {
                AllowNew = true,
                AllowRemove = false,
                RaiseListChangedEvents = true,
                AllowEdit = false
            };


            // Add a couple of parts to the list.
            parts.Add(new Part("Widget", 1234));
            parts.Add(new Part("Gadget", 5647));
        }

        private void listOfParts_ListChanged(object sender, ListChangedEventArgs e)
        {
            MessageBox.Show(e.ListChangedType.ToString());
        }

        private void listOfParts_AddingNew(object sender, AddingNewEventArgs e)
        {
            e.NewObject = new Part(textBox1.Text, int.Parse(textBox2.Text));
        }
    }
}