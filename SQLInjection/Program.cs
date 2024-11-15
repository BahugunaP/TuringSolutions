namespace SQLInjection;
using System.Data.SqlClient;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
    public List<T> GetItemsByProperty<T>(string propertyName, object propertyValue)
    {
        string connectionString = "";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // Build the generic query using string interpolation
            string query = $"SELECT * FROM {typeof(T).Name} WHERE {propertyName} = @{propertyName}";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@" + propertyName, propertyValue);

            using (var reader = command.ExecuteReader())
            {
                var items = new List<T>();
                while (reader.Read())
                {
                    // Use reflection to dynamically access properties
                    T item = Activator.CreateInstance<T>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        PropertyInfo property = typeof(T).GetProperty(columnName);

                        if (property != null)
                        {
                            property.SetValue(item, reader[i]);
                        }
                    }
                    items.Add(item);
                }
                return items;
            }
        }
    }
}
