using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Utilisateur : ICRUD
    {
        public static bool Connecte(string pseudo, string password)
        {
            using(DB db = new DB())
            {
                var user = db.Utilisateurs.FirstOrDefault(u => u.Pseudo == pseudo && u.MotDePasse == password);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static Utilisateur GetIdUserAfterLogin(string pseudo, string password)
        {
            using (DB db = new DB())
            {
                var user = db.Utilisateurs.FirstOrDefault(u => u.Pseudo == pseudo && u.MotDePasse == password);
                if (user != null)
                {
                    return user;
                }
            }
            return null;
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Utilisateurs.Add(this);
                db.SaveChanges();
            }          
        }

        public void Update()
        {
            using (DB db = new DB())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

        }

        //Méthode ne necessitant pas la création d'un objet est a déclarée static
        public static List<Utilisateur> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.Utilisateurs.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static Utilisateur getUtilisateur(int Id)
        {
            using (DB db = new DB())
            {
                Utilisateur c = new Utilisateur();
                c = db.Utilisateurs.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }

        public static bool userExist(string pseudo)
        {
            using (DB db = new DB())
            {
                Utilisateur c = new Utilisateur();
                c = db.Utilisateurs.SingleOrDefault(i => i.Pseudo == pseudo);
                if (c != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
