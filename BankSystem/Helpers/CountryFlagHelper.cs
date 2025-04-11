public static class CountryFlagHelper
{
    public static string GetCountryCode(string countryName)
    {
        return countryName switch
        {
            "Sweden" => "se",
            "Norway" => "no",
            "Denmark" => "dk",
            "Finland" => "fi",
            _ => "xx"
        };
    }
}