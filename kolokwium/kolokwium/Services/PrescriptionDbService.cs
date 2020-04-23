using kolokwium.DTOs.Responses;
using System.Data.SqlClient;

namespace kolokwium.Services
{
    public class PrescriptionDbService : IDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18849;Integrated Security=True";
        public GetPrescriptionResponse GetPrescription(string Id)
        {
           
                var response = new GetPrescriptionResponse();

                using (var con = new SqlConnection(ConString))
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "select IdPrescription, Date, DueDate, IdPatient, IdDoctor from Prescription where IdPrescription=@prescription";
                    com.Parameters.AddWithValue("prescription", int.Parse(Id.ToString()));

                    con.Open();
                    var dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        response.IdPrescription = int.Parse(dr["IdPrescription"].ToString());
                        response.Date = dr["Date"].ToString();
                        response.DueDate = dr["DueDate"].ToString();
                        response.IdPatient = int.Parse(dr["IdPatient"].ToString());
                        response.IdDoctor = int.Parse(dr["IdDoctor"].ToString());
                        
                    }
                    return response;
                }
            
        }
    }
}
