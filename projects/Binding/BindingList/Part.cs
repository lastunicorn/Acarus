namespace BindingList
{
    internal class Part
    {
        public string PartName { get; set; }

        public int PartNumber { get; set; }

        public Part(string nameForPart, int numberForPart)
        {
            PartName = nameForPart;
            PartNumber = numberForPart;
        }
    }
}