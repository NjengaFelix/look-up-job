using System.Data.Linq;
using System.Data.Linq.Mapping;

[Table]
public class User_details
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int user_id
    {
        get;
        set;
    }
    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string first_name
    {
        get;
        set;
    }
    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string last_name
    {
        get;
        set;
    }
    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string email
    {
        get;
        set;
    }
    [Column(DbType = "bit NOT NULL", CanBeNull = false)]
    public bool is_Employee_or_JobSeeker
    {
        get;
        set;
    }
    [Column(DbType = "NVarChar(2000) NOT NULL", CanBeNull = false)]
    public string user_name
    {
        get;
        set;
    }
    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string password
    {
        get;
        set;
    }

    [Column(DbType = "bit NULL", CanBeNull = true)]
    public bool is_super_user
    {
        get;
        set;
    }

    [Column(DbType = "bit NULL", CanBeNull = true)]
    public bool is_active
    {
        get;
        set;
    }

    [Column(CanBeNull = true)]
    public int company_id;
    private EntityRef<Company> _CompanyUser;
    [Association(Storage = "_CompanyUser", ThisKey = "company_id")]
    public Company Company
    {
        get { return this._CompanyUser.Entity; }
        set { this._CompanyUser.Entity = value; }
    }

}

[Table]
public class Vacancy
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int vacancies_id
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string short_description
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string position
    {
        get;
        set;
    }

    [Column(CanBeNull = true)]
    public int years_of_experience
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(300) NULL", CanBeNull = true)]
    public string highest_level_of_education
    {
        get;
        set;
    }


    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string vacancy_deadline_date
    {
        get;
        set;
    }

    [Column(CanBeNull = false)]
    public int user_id;
    private EntityRef<User_details> _EmployeerVacancy;
    [Association(Storage = "_EmployeerVacancy", ThisKey = "user_id")]
    public User_details User_details
    {
        get { return this._EmployeerVacancy.Entity; }
        set { this._EmployeerVacancy.Entity = value; }
    }

    [Column(DbType = "NVarChar(4000) NULL", CanBeNull = true)]
    public string publish_date
    {
        get;
        set;
    }

}

[Table]
public class Company
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int company_id
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string name
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]

    public string company_type
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string location
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string email
    {
        get;
        set;
    }
}

[Table]
public class CV
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int cv_id
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(4000) NOT NULL", CanBeNull = false)]
    public string short_description
    {
        get;
        set;
    }


    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string occupation
    {
        get;
        set;
    }

    [Column(CanBeNull = false)]
    public int years_of_experience
    {
        get;
        set;
    }

    [Column(DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
    public string education_level
    {
        get;
        set;
    }

    [Column]
    public int user_id;
    private EntityRef<User_details> _EmployeeCV;
    [Association(Storage = "_EmployeeCV", ThisKey = "user_id")]
    public User_details User_details
    {
        get { return this._EmployeeCV.Entity; }
        set { this._EmployeeCV.Entity = value; }

    }

}

[Table]
public class VacancyApplication
{
    [Column(IsDbGenerated = true, IsPrimaryKey = true)]
    public int vacancy_application_id
    {
        set;
        get;
    }

    [Column]
    public int user_id;
    private EntityRef<User_details> _Applicant;
    [Association(Storage = "_Applicant", ThisKey= "user_id")]
    public User_details User_details
    {
        get { return this._Applicant.Entity; }
        set { this._Applicant.Entity = value; }
    }

    [Column]
    public int cv_id;
    private EntityRef<CV> _ApplicantCV;
    [Association(Storage = "_ApplicantCV", ThisKey = "cv_id")]
    public CV CV
    {
        get { return this._ApplicantCV.Entity; }
        set { this._ApplicantCV.Entity = value; }
    }

    [Column]
    public int vacancy_id;
    private EntityRef<Vacancy> _Vacancy;
    [Association(Storage = "_Vacancy", ThisKey = "vacancy_id")]
    public Vacancy Vacancies
    {
        get { return this._Vacancy.Entity; }
        set { this._Vacancy.Entity = value; }
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

    public Table<Vacancy> Vacancies
    {
        get
        {
            return this.GetTable<Vacancy>();
        }
    }

    public Table<Company> Company
    {
        get
        {
            return this.GetTable<Company>();
        }
    }

    public Table<CV> CV
    {
        get
        {
            return this.GetTable<CV>();
        }
    }

    public Table<VacancyApplication> VacancyApplications
    {
        get
        {
            return this.GetTable<VacancyApplication>();
        }
    }

    
}