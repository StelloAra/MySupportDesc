namespace SupportDesk.Infrastructure.Data;

public static class DataPaths
{
    public static string DataFolder =>
        Path.Combine(AppContext.BaseDirectory, "Data");

    public static string CustomersFile =>   
        Path.Combine(DataFolder, "customers.json");

    public static string TicketsFile =>
        Path.Combine(DataFolder, "tickets.json");
}