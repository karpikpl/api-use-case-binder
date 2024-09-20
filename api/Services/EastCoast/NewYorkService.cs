namespace api.Services.EastCoast
{
    public class NewYorkService
    {
        public int GetTemperature()
        {
            return Random.Shared.Next(-20, 55);
        }
    }
}