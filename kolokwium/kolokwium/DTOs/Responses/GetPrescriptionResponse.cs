using kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium.DTOs.Responses
{
    public class GetPrescriptionResponse
    {
        public int IdPrescription { get; set; }
        public string Date { get; set; }
        public string DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        List<Medicament> medicaments { get; set; }
    }
}
