namespace api.Services.WestCoast
{
    public class SeattleService
    {
        public int GetTemperature()
        {
            return Random.Shared.Next(40, 90);
        }
    }
}