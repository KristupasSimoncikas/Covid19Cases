using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Teltonika.DataModels
{
    public class Covid19Case
    {
        [Column("object_id")]
        public int Id { get; set; }
        [Column("gender")]
        public string Gender { get; set; }
        [Column("age_bracket")]
        public string AgeBracket { get; set; }
        [Column("municipality_name")]
        public string MunicipalityName { get; set; }
        [Column("municipality_code")]
        public string MunicipalityCode { get; set; }
        [Column("confirmation_date")]
        public DateTimeOffset ConfirmationDate { get; set; }
        [Column("case_code")]
        public string CaseCode { get; set; }
        [Column("X")]
        public float? CoordinateX { get; set; }
        [Column("Y")]
        public float? CoordinateY { get; set; }
    }
}
