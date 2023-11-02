namespace Master.Chef.Fiap.CrossCutting.Configurations;

public class AppSettings
{
    public string Secret { get; set; }
    public int ExpireHour { get; set; }
    public string Issuer { get; set; }
    public string ValidIn { get; set; }
}