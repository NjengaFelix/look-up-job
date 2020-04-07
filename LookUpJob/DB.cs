using System.Data.Linq;
using System.Data.Linq.Mapping;

[Table]
public class User_details
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int id
    {
        get;
        set;
    }
    [Column]
    public string first_name
    {
        get;
        set;
    }
    [Column]
    public string last_name
    {
        get;
        set;
    }
    [Column]
    public string email
    {
        get;
        set;
    }
    [Column]
    public bool is_Employee_or_JobSeeker
    {
        get;
        set;
    }
    [Column]
    public string user_name
    {
        get;
        set;
    }
    [Column]
    public string password
    {
        get;
        set;
    }
}

public class UserDataContext : DataContext
{
    public static string DBConnectionString = @"isostore:/LookUpJobDb.sdf";
    public UserDataContext(string connectionString)
        : base(connectionString)
    { }
    public Table<User_details> Users
    {
        get
        {
            return this.GetTable<User_details>();
        }
    }
}