namespace TaranSoft.MyGarage.Contracts
{
    public class ManufacturerDto
    {
        public string ManufacturerName { get; set; }
        public long YearCreation { get; set; }

        public CountryDto ManufacturerCountry { get; set; }
    }
}