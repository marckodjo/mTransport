using mTransport.Methodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport.Models
{
    public partial class HistoriquePanne : ICRUD
    {
        public void delete()
        {
            //using (DB db = new DB())
            //{
            //    var d = db.HistoriquePannes.SingleOrDefault(i => i.Id == Id);
            //    if (d != null)
            //    {
            //        db.HistoriquePannes.Remove(d);
            //        db.SaveChanges();
            //    }
            //}
        }

        public void Insert()
        {
            using (DB db = new DB())
            {
                db.HistoriquePannes.Add(this);
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
        public static List<HistoriquePanne> getAll()
        {
            using (DB db = new DB())
            {
                var l = db.HistoriquePannes.Where(i => i.Supprime == false).ToList();
                return l;
            }               
        }

        public static HistoriquePanne getHistoriquePanne(int Id)
        {
            using (DB db = new DB())
            {
                HistoriquePanne c = new HistoriquePanne();
                c = db.HistoriquePannes.SingleOrDefault(i => i.Id == Id);
                return c;
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
