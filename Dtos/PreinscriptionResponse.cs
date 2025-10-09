namespace UnivManager.Dtos
{
    // class Note {
    //     string matiere {get;set;}
    //     string value {get;set;}
    // }
    public class PreinscriptionResponse
    {
        public string? Nom_prenom { get; set; }
        public DateTime Date_naissance { get; set; }
        public string? Lieu_naissance { get; set; }
        // public string? Sexe { get; set; }
        public string? Mention { get; set; }
        public string? Option { get; set; }
        public string? Num_bacc { get; set; }
        // public List<Note> Note {get;set;}

    }
}
