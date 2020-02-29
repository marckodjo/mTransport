using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Chauffeur : ICRUD
    {
        public void delete()
        {
            //using (DB db = new DB())
            //{
            //    var d = db.Chauffeurs.SingleOrDefault(i => i.Id == Id);
            //    if (d != null)
            //    {
            //        db.Chauffeurs.Remove(d);
            //        db.SaveChanges();
            //    }
            //}
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Chauffeurs.Add(this);
                db.SaveChanges();
            }          
        }

        public void Update()
        {

            //var C = db.Chauffeurs.SingleOrDefault(i => i.Id == Id);
            //C.Matricule = chauffeur.Matricule;
            //C.NomChauffeur = chauffeur.NomChauffeur;
            //C.PrenomChauffeur = chauffeur.PrenomChauffeur;
            //C.AdresseChauffeur = chauffeur.AdresseChauffeur;
            //C.Telephone1Chauffeur = chauffeur.Telephone1Chauffeur;
            //C.Telephone2Chauffeur = chauffeur.Telephone2Chauffeur;
            //C.DateEmbauche = chauffeur.DateEmbauche;
            //C.PersonneAPrevenir = chauffeur.PersonneAPrevenir;
            //db.SaveChanges();

            using (DB db = new DB())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Méthode ne necessitant pas la création d'un objet est a déclarée static
        public static List<Chauffeur> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.Chauffeurs.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static Chauffeur getUnChauffeur(int Id)
        {
            using (DB db = new DB())
            {
                Chauffeur c = new Chauffeur();
                c = db.Chauffeurs.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
