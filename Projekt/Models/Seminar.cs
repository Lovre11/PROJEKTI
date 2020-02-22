using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Seminar
    {
        [Key]
        public int IdSeminar { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Predavač")]
        public string Predavac { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Naziv seminara")]
        public string Naziv { get; set; }
        
        [Required]
        [Display(Name = "Opis seminara")]
        [UIHint("MultilineText")]
        public string Opis { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Datum predaje seminara")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        public bool Popunjen { get; set; }

        [Display(Name = "Broj polaznika")]
        public int? BrojPolaznika { get; set; }

        public Seminar()
        {
            BrojPolaznika = 0;
        }


    }

    
}