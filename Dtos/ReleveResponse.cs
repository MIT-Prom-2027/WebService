using System;
using System.Collections.Generic;

namespace UnivManager.Dtos
{
    public class ReleveResponse
    {
        // Informations personnelles
        public string NomPrenom { get; set; } = null!;
        public DateOnly DateNaissance { get; set; }
        public string LieuNaissance { get; set; } = null!;
        
        // Informations du baccalauréat
        public DateOnly AnneeBacc { get; set; }
        public string NumeroBacc { get; set; } = null!;
        public string SerieBacc { get; set; } = null!;
        public string CentreBacc { get; set; } = null!;
        
        // Notes et résultats
        public List<NoteDetail> Notes { get; set; } = new List<NoteDetail>();
        public double TotalNotes { get; set; }
        public double TotalCoefficients { get; set; }
        public double MoyenneBacc { get; set; }
        public bool EstAdmis { get; set; }
        public string Mention { get; set; } = null!;
    }

    public class NoteDetail
    {
        public string Matiere { get; set; } = null!;
        public double Note { get; set; }
        public double Coefficient { get; set; }
        public bool EstOptionnel { get; set; }
        public double NotePondere { get; set; }
    }
}
