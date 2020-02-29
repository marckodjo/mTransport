using FastReport;
using mTransport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mTransport
{
    class Outils
    {
        //Vérifie si un champs numérique ne contien que des numériques
        public static bool CheckNumber(string value)
        {
            try
            {
                if (value != "")
                {
                    int.Parse(value);
                }
               return true;
            }
            catch
            {
                return false;
            }

        }

        //Génère les matricules
        public static string Generator(string maClass)
        {
            string mat,value,increment;
            switch (maClass)
            {
                case "TeteEngins":
                    using (DB db = new DB())
                    {
                        mat = (from i in db.TeteEngins select i.Matricule).Max();
                        if (string.IsNullOrEmpty(mat))
                        {
                            increment = "0";
                        }
                        else
                        {
                            int startIndex = 3;
                            int endIndex = mat.Length - 3;
                            increment = mat.Substring(startIndex, endIndex);
                        }
                        int incr = int.Parse(increment);
                        incr++;
                        increment = incr.ToString();
                        if (increment.Length == 3)
                        {
                            value = "TEG0" + incr;
                        }
                        else if (increment.Length == 2)
                        {
                            value = "TEG00" + incr;
                        }
                        else
                        {
                            value = "TEG000" + incr;
                        }
                        return value;
                    }

                case "BenneEngins":
                    using (DB db = new DB())
                    {
                        mat = (from i in db.BenneEngins select i.Matricule).Max();
                        if (string.IsNullOrEmpty(mat))
                        {
                            increment = "0";
                        }
                        else
                        {
                            int startIndex = 3;
                            int endIndex = mat.Length - 3;
                            increment = mat.Substring(startIndex, endIndex);
                        }
                            int incr = int.Parse(increment);
                            incr++;
                            increment = incr.ToString();
                        if (increment.Length == 3)
                        {
                            value = "BEG0" + incr;
                        }
                        else if (increment.Length == 2)
                        {
                            value = "BEG00" + incr;
                        }
                        else
                        {
                            value = "BEG000" + incr;
                        }
                        
                        return value;
                    }

                case "Chauffeurs":
                    using (DB db = new DB())
                    {
                        mat = (from i in db.Chauffeurs select i.Matricule).Max();
                        if (string.IsNullOrEmpty(mat))
                        {
                            increment = "0";
                        }
                        else
                        {
                            int startIndex = 3;
                            int endIndex = mat.Length - 3;
                            increment = mat.Substring(startIndex, endIndex);
                        }
                        int incr = int.Parse(increment);
                        incr++;
                        increment = incr.ToString();
                        if (increment.Length == 3)
                        {
                            value = "CHF0" + incr;
                        }
                        else if (increment.Length == 2)
                        {
                            value = "CHF00" + incr;
                        }
                        else
                        {
                            value = "CHF000" + incr;
                        }
                        return value;
                    }


                default:
                    return "";
            }
            
        }

        public static bool VerifDate(DateTime date1, DateTime date2, string nomChamp1, string nomChamp2)
        {
            if (date1 > date2)
            {
                MessageBox.Show("Le champs " + nomChamp1 + " doit est antérieur au champs " + nomChamp2 +"!!!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
        }

        public static bool VerifNoChassis(string value)
        {
            try
            {
                using (DB db = new DB())
                {
                    var reponse = db.TeteEngins.Where(a => a.NoChassis == value).Count();
                    if (reponse > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static bool VerifDateExist(int Driver, int vehicule, DateTime dateDepart)
        {
            try
            {
                using (DB db = new DB())
                {
                    var reponse = db.AffectationVehicules.FirstOrDefault(a => a.IdChauffeur <= Driver && a.IdVehicule==vehicule);
                    if (dateDepart > reponse.DateDebutAffectation && dateDepart < reponse.DateFinAffectation)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static void BoxMessage(string mode)
        {
            switch (mode)
            {
                case "A":
                    MessageBox.Show("Enregistrement effectué avec succès!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "M":
                    MessageBox.Show("Modification effectuée avec succès!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "S":
                    MessageBox.Show("Suppression effectuée avec succès!", "mTransport", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "C": //Message d'erreur pour champs non remplis
                    MessageBox.Show("Veuillez remplir tous les champs !", "mTransport", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                default:
                    break;
            }
        }

    }
}
