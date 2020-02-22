using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Predbiljezba
    {
        [Key]
        public int IdPredbiljezba { get; set; }

        [Display(Name = "Seminar")]
        [ForeignKey("Seminar")]
        public int IdSeminar { get; set; }
        public virtual Seminar Seminar { get; set; }

        [Display(Name = "Datum predbilježbe")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }

        [Required]
        [StringLength(30)]
        public string Ime { get; set; }

        [Required]
        [StringLength(30)]
        public string Prezime { get; set; }

        [Required]
        [StringLength(100)]
        public string Adresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Kontakt broj")]
        public string KontaktBroj { get; set; }

        [StringLength(10)]
        [UIHint("TemplStatusKlijenta")] 
        public string Status { get; set; }

         
        public Predbiljezba()
        {
            Datum = DateTime.Now;
        }


    }
}