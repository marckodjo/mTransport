using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class BenneEngin : ICRUD
    {
        public string matriculeMarque
        {
            get
            {
                return Matricule + "/" + Marque;
            }
        }

        //Valeur envoyée à l'état
        public string TypeLibelle
        {
            get
            {
                return TypeBenneEngin.Libelle;
            }
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.BenneEngins.Add(this);
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
        public static List<BenneEngin> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.BenneEngins.Where(i => i.Supprime == false).ToList();
                foreach (var item in l)
                {
                    item.TypeBenneEngin = TypeBenneEngin.getUnTypeBenneEngin(item.IdTypeBenne.Value);
                }
                return l;
            }               
        }

        public static BenneEngin getUnBenneEngin(int Id)
        {
            using (DB db = new DB())
            {
                BenneEngin c = new BenneEngin();
                c = db.BenneEngins.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }


    }
}
