using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OutilAdmin.Models
{
    public class BachelierCsvModel
    {
        public string? NumeroCandidat { get; set; }
        public string? NomPrenom { get; set; }
        public string? DateNaissance { get; set; }
        public string? LieuNaissance { get; set; }
        public string? Sexe { get; set; }
        public string? Option { get; set; }
        public string? Moyenne { get; set; }
        public string? Mention { get; set; }
        public string? Centre { get; set; }
        public string? Etablissement { get; set; }
        public string? Province { get; set; }
        
        // Notes par matière
        public string? FRS { get; set; }
        public string? MAL { get; set; }
        public string? HG { get; set; }
        public string? PHI { get; set; }
        public string? MG { get; set; }
        public string? SN { get; set; }
        public string? PC { get; set; }
        public string? EPS { get; set; }
        public string? EPST { get; set; }
        public string? SES { get; set; }
        public string? LV2 { get; set; }
    }
}