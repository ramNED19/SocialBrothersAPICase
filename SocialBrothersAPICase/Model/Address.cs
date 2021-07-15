using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialBrothersAPICase.Model
{
    public class Address
    {
        public long Id { get; set; }
        private string straat;
        private int huisnummer;
        private string toevoeging;
        private string postcode;
        private string plaats;
        private string land;

        #region Properties
        [Required]
        public string Straat
        {
            get { return straat; }
            set
            {
                Regex regex = new Regex(@"^[\w\s]{1,100}$");
                if (regex.IsMatch(value))
                {
                    straat = value.ToLower();
                }
                else
                {
                    throw new ArgumentException("Not a valid street name!");
                }
            }
        }

        [Required]
        public int Huisnummer
        {
            get { return huisnummer; }
            set
            {
                if (value < 100000 && value >= 0)
                {
                    huisnummer = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Not a valid house number!");
                }
            }
        }

        public string Toevoeging
        {
            get { return toevoeging; }
            set
            {
                if(value == null)
                {
                    value = "";
                }
                Regex regex = new Regex(@"^[a-zA-Z]{0,5}$");
                if (regex.IsMatch(value))
                {
                    toevoeging = value.ToLower();
                }
                else
                {
                    throw new IndexOutOfRangeException("Not a valid addition!");
                }
            }
        }

        public string Postcode
        {
            get { return postcode; }
            set
            {
                Regex regex = new Regex(@"^([A-Za-z0-9 ]{0,10})$");
                if (regex.IsMatch(value))
                {
                    postcode = value.ToLower();
                }
                else
                {
                    throw new ArgumentException("Not a valid postal code!");
                }
            }
        }

        [Required]
        public string Plaats
        {
            get { return plaats; }
            set
            {
                Regex regex = new Regex(@"^[\w\s]{1,100}$");
                if (regex.IsMatch(value))
                {
                    plaats = value.ToLower();
                }
                else
                {
                    throw new ArgumentException("Not a valid location!");
                }
            }
        }

        [Required]
        public string Land
        {
            get { return land; }
            set
            {
                Regex regex = new Regex(@"^[\w\s]{1,60}$");
                if (regex.IsMatch(value))
                {
                    land = value.ToLower();
                }
                else
                {
                    throw new ArgumentException("Not a valid country!");
                }
            }
        }
        #endregion
    }
}
