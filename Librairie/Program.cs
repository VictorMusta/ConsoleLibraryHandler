using System;
using System.Collections.Generic;
using System.Xml.Serialization;
    
[Serializable]
class Librairie
{
    static List<Livre> bibliotheque = new List<Livre>();

    static void Main(string[] args)
    {
        ChargerBibliotheque();

        bool continuer = true;
        while (continuer)
        {
            AfficherMenu();
            string choix = Console.ReadLine();
            switch (choix)
            {
                case "1":
                    AjouterLivre();
                    break;
                case "2":
                    AfficherLivres();
                    break;
                case "3":
                    RechercherLivre();
                    break;
                case "4":
                    SauvegarderBibliotheque();
                    continuer = false;
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
    }

    static void AfficherMenu()
    {
        Console.WriteLine("\nGestion de Librairie");
        Console.WriteLine("1. Ajouter un livre");
        Console.WriteLine("2. Afficher tous les livres");
        Console.WriteLine("3. Rechercher un livre");
        Console.WriteLine("4. Quitter");
        Console.Write("Choisissez une option : ");
    }

    static void AjouterLivre()
    {
        Console.Write("Titre : ");
        string titre = Console.ReadLine();
        Console.Write("Auteur : ");
        string auteur = Console.ReadLine();
        Console.Write("ISBN : ");
        string isbn = Console.ReadLine();

        bibliotheque.Add(new Livre(titre, auteur, isbn));
        Console.WriteLine("Livre ajouté avec succès !");
    }

    static void AfficherLivres()
    {
        if (bibliotheque.Count == 0)
        {
            Console.WriteLine("La bibliothèque est vide.");
            return;
        }

        foreach (var livre in bibliotheque)
        {
            Console.WriteLine(livre);
        }
    }

    static void RechercherLivre()
    {
        Console.Write("Entrez le titre ou l'auteur à rechercher : ");
        string recherche = Console.ReadLine().ToLower();

        var resultats = bibliotheque.FindAll(l => 
            l.Titre.ToLower().Contains(recherche) || 
            l.Auteur.ToLower().Contains(recherche));

        if (resultats.Count == 0)
        {
            Console.WriteLine("Aucun livre trouvé.");
        }
        else
        {
            foreach (var livre in resultats)
            {
                Console.WriteLine(livre);
            }
        }
    }
    
    static void SauvegarderBibliotheque()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Livre>));
        using (TextWriter writer = new StreamWriter("bibliotheque.xml"))
        {
            serializer.Serialize(writer, bibliotheque);
        }
        Console.WriteLine("Bibliothèque sauvegardée avec succès.");
        Console.WriteLine($"Le fichier a été sauvegardé à : {Path.GetFullPath("bibliotheque.xml")}");
    }

    static void ChargerBibliotheque()
    {
        if (File.Exists("bibliotheque.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Livre>));
            using (TextReader reader = new StreamReader("bibliotheque.xml"))
            {
                bibliotheque = (List<Livre>)serializer.Deserialize(reader);
            }
            Console.WriteLine("Bibliothèque chargée avec succès.");
        }
    }
}