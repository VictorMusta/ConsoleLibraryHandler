public class Livre
{
    public string Titre { get; set; }
    public string Auteur { get; set; }
    public string ISBN { get; set; }

    public Livre(string titre, string auteur, string isbn)
    {
        Titre = titre;
        Auteur = auteur;
        ISBN = isbn;
    }

    public override string ToString()
    {
        return $"{Titre} par {Auteur} (ISBN: {ISBN})";
    }
}