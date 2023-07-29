
public class Department
{
    public int id { get; set; }
    public bool canSee { get; set; }
    public int canSeeInt
    {
        get
        {
            if (canSee)
            {
                return 1;
            }
            return 0;
        }
    }
    public string departmentName { get; set; }
}