using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class Depense: ICRUD
    {
        public string Designation
        {
            get
            {
                return TypeDepense.Designation;
            }
        }
        public string Panne
        {
            get
            {
                return HistoriquePanne.Description;
            }
        }
        public string UnVoyage
        {
            get
            {
                return Voyage.Designation;
            }
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.Depenses.Add(this);
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
        public static List<Depense> getAll(int id, string value)
        {
            switch (value)
            {
               
                case "v": //Voyage
                    using (DB db = new DB())
                    {
                        var l = db.Depenses.Where(i => i.Supprime == false).ToList();
                        //On récupère la liste des dépenses liées au véhicule
                        var m = l.Where(i => i.IdVoyage == id).ToList();
                        foreach (var item in m)
                        {
                            item.TypeDepense = TypeDepense.getTypeDepense(item.IdTypeDepense);
                            item.Voyage = Voyage.getVoyage(item.IdVoyage.Value);
                        }
                        return m;
                    }
                    //break;
                case "h": //Historique panne
                    using (DB db = new DB())
                    {
                        var l = db.Depenses.Where(i => i.Supprime == false).ToList();
                        //On récupère la liste des dépenses liées au matériel
                        var m = l.Where(i => i.IdHistoriquePanne == id).ToList();
                        foreach (var item in m)
                        {
                            item.HistoriquePanne = HistoriquePanne.getHistoriquePanne(item.IdHistoriquePanne.Value);
                            item.TypeDepense = TypeDepense.getTypeDepense(item.IdTypeDepense);
                            item.Voyage = Voyage.getVoyage(item.IdVoyage.Value);
                        }
                        return m;
                    }
                    //break;
                default:
                    break;
            }
            return null;
        }

        public static Depense getDepense(int Id)
        {
            using (DB db = new DB())
            {
                Depense c = new Depense();
                c = db.Depenses.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            Update();
        }
    }
}
