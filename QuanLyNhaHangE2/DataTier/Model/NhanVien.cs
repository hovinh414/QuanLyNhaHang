namespace QuanLyNhaHangE2.DataTier.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int MaNhanVien { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(500)]
        public string MatKhau { get; set; }

        [StringLength(100)]
        public string TenDangNhap { get; set; }
        [StringLength(20)]
        public string ChucVu { get; set; }
        [StringLength(100)]
        public string DiaChi { get; set; }

        public bool? GioiTinh { get; set; }
        [StringLength(100)]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
