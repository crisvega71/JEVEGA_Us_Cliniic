
namespace JEVEGA_Us_Cliniic.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class UsDBContext : DbContext
    {

        public virtual DbSet<ExamVideo> ExamVideos { get; set; }

    }
}