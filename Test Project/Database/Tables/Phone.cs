namespace Test_Project.Database.Tables
{
    class Phone
    {
        public string Model { get; set; } = null!;

        public string Manufacturer { get; set; } = null!;
        public Manufacturer ManufacturerProperty { get; set; } = null!;

        public double Price { get; set; }
    }
}