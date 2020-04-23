using kolokwium.DTOs.Requests;
using kolokwium.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium.Services
{
    public interface IDbService
    {
        GetPrescriptionResponse GetPrescription(int Id);
        InsertPrescriptionResponse InsertPrescription(InsertPrescriptionRequest request);
    }
}
