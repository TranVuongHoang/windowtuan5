namespace Entity_framwork_3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string mssv { get; set; }

        [StringLength(100)]
        public string tenSinhVien { get; set; }

        public int? maLop { get; set; }

        public virtual Class Class { get; set; }
    }
}
