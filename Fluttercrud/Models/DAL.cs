using System.Data;
using System.Data.SqlClient;


namespace Fluttercrud.Models
{
    public class DAL
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Notes", connection);
            DataTable dt = new DataTable();
            List<Employee> lstEmployee = new List<Employee>();
            da.Fill(dt);    
            if(dt.Rows.Count > 0 )
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.id = Convert.ToInt32(dt.Rows[i]["id"]);
                    employee.title = Convert.ToString(dt.Rows[i]["title"]);
                    employee.age = Convert.ToInt32(dt.Rows[i]["age"]);
                    employee.description = Convert.ToString(dt.Rows[i]["description"]);
                    employee.email = Convert.ToString(dt.Rows[i]["email"]);
                    lstEmployee.Add(employee);
                }
            }
            if(lstEmployee.Count>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "OK";
                response.listEmployee = lstEmployee;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "not okay";
                response.listEmployee = lstEmployee;
            }

            return response;
        }

        public Response PostEmployees(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand cmd= new SqlCommand("INSERT INTO Notes(title,age,description,email) VALUES('"+ employee.title +"','"+ employee.age +"','"+ employee.description +"','"+ employee.email +"')", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            { 
                response.StatusCode = 200;
                response.StatusMessage = "Employee Added";              
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "no Data inserted";
            }
            return response;
        }
    }
}
