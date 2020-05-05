using Domain.Base;

namespace Domain.Entities
{
    public class AbonoCuota : Entity<int>
    {
        public virtual Abono Abono { get; set; }
        public virtual Cuota Cuota { get; set; }
        public int AbonoId { get; set; }
        public int CuotaId { get; set; }
        public AbonoCuota(Abono abono, Cuota cuota)
        {
            this.AbonoId = abono.Id;
            this.CuotaId = cuota.Id;
        }
        public AbonoCuota() { }
    }
}
