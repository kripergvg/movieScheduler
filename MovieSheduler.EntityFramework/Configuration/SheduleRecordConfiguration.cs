using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using MovieSheduler.Domain.SheduleRecord;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieSheduler.EntityFramework.Configuration
{
    public class SheduleRecordConfiguration : EntityTypeConfiguration<SheduleRecord>
    {
        public SheduleRecordConfiguration()
        {
            string sheduleRecordIndex = "IXU_SheduleRecord";
            
            Property(r => r.CinemaId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute(sheduleRecordIndex)
                {
                    IsUnique = true,
                    Order = 1
                }));

            Property(r => r.MovieId)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute(sheduleRecordIndex)
                {
                    IsUnique = true,
                    Order = 2
                }));

            Property(r => r.Date)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute(sheduleRecordIndex)
                {
                    IsUnique = true,
                    Order = 3
                }));
        }
    }
}
