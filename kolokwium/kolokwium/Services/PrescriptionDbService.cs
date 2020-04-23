using kolokwium.DTOs.Requests;
using kolokwium.DTOs.Responses;
using System.Data.SqlClient;

namespace kolokwium.Services
{
    public class PrescriptionDbService : IDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18849;Integrated Security=True";
        public GetPrescriptionResponse GetPrescription(int Id)
        {

            var response = new GetPrescriptionResponse();

            using (var con = new SqlConnection(ConString))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select IdPrescription, Date, DueDate, IdPatient, IdDoctor from Prescription where IdPrescription=@prescription";
                com.Parameters.AddWithValue("prescription", Id);

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

        public InsertPrescriptionResponse InsertPrescription(InsertPrescriptionRequest request)
        {
            InsertPrescriptionResponse response;
            // tutaj if ze sprawdzeniem czy duedate jest starsza od date i ewentualne zwrocenie nulla
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {

                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select count(*)+1 as Id from Prescription";
                    int Id;
                    using(var dr = com.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            return null;
                        }
                        Id = int.Parse(dr["Id"].ToString());
                    }
                    com.Parameters.Clear();
                    com.CommandText = "select * from Patient where IdPatient = @patient";
                    com.Parameters.AddWithValue("patient", request.IdPatient);
                    using (var dr = com.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            return null;
                        }
                       
                    }
                    com.CommandText = "select * from Doctor where IdDoctor = @doctor";
                    com.Parameters.AddWithValue("doctor", request.IdPatient);
                    using (var dr = com.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            return null;
                        }

                    }


                    com.CommandText = "insert into prescription (IdPrescription,Date, DueDate,IdPatient,IdDoctor)" +
                       "values(@IdPrescription,@Date,@DueDate,@IdPatient,@IdDoctor)";

                    com.Parameters.AddWithValue("IdPrescription", Id);
                    com.Parameters.AddWithValue("Date", request.Date);
                    com.Parameters.AddWithValue("DueDate", request.DueDate);
                    com.Parameters.AddWithValue("IdPatient", request.IdPatient);
                    com.Parameters.AddWithValue("IdDoctor", request.IdDoctor);

                    com.ExecuteNonQuery();
                    tran.Commit();

                    response = new InsertPrescriptionResponse();
                    response.IdPrescription = Id;
                    response.Date = request.Date;
                    response.DueDate = request.DueDate;
                    response.IdPatient = request.IdPatient;
                    response.IdDoctor = request.IdPatient;

                    return response;


                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                    return null;

                }
            }
        }
    }
}
