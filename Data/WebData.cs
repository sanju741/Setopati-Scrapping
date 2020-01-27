using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebScrapping.Data
{
    public class WebData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

                [Required]
        [MaxLength(500)]
        public string Name { get; set; }


        [Required]
        public string Url { get; set; }

        [Required]
        //[MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [MaxLength(250)]
        public string DescriptionExtra { get; set; }

        [Required]
        public float Volume { get; set; }

        [Required]
        public float Count { get; set; }

        [MaxLength(50)]
        public string CodeNr { get; set; }

        [MaxLength(50)]
        public string BarCodeNr { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public string PriceStr { get; set; }

        public float PriceExtra { get; set; }

        public string SourcePictureUrl { get; set; }
        public string PictureExtension { get; set; }
        public byte[] PictureBinary { get; set; }

        public string PictureNameInAzure { get; set; }
        public string PictureUrlInAzure { get; set; }
    }
}
