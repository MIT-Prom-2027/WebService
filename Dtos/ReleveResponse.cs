using System;
using System.Collections.Generic;

namespace UnivManager.Dtos
{
    public class ReleveResponse
    {
        // Informations personnelles
        public string? Nom_prenom { get; set; }
        public DateTime Date_naissance { get; set; }
        public string? Lieu_naissance { get; set; }
        
        // Informations du baccalauréat
        public int Annee { get; set; }
        public string? Num_bacc { get; set; }
        public string? Serie_bacc { get; set; }
        public string? Centre_bacc { get; set; }
        
        // Notes et résultats
        public List<NoteDetail> Notes { get; set; } = new List<NoteDetail>();
        public double Total_notes { get; set; }
        public double Total_coefficients { get; set; }
        public double Moyenne_bacc { get; set; }
        public bool Est_admis { get; set; }
        public string? Mention { get; set; }
    }

    public class NoteDetail
    {
        public string? Matiere { get; set; }
        public double Note { get; set; }
        public double Coefficient { get; set; }
        public bool Est_optionnel { get; set; }
        public double Note_ponderee { get; set; }
    }
}
