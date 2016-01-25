using FluentNHibernate.Mapping;

namespace WindowsTime.IntegratedTest
{
    public class FileImportMap : ClassMap<FileImport>
    {
        public FileImportMap()
        {
            Table("FileImport");

            Id(x => x.Id).Column("IdFileImport");

            Map(x => x.StoreId).Column("IdStore").Not.Nullable();
            Map(x => x.ImportType).Column("ImportType").Not.Nullable();
            Map(x => x.ImportDate).Column("ImportDate").Not.Nullable().CustomType("Timestamp");
            Map(x => x.Status).Column("Status").Not.Nullable().Length(1);
            Map(x => x.ImportedFilePath).Column("ImportedFilePath").Length(255).Not.Nullable();
            Map(x => x.UserOwner).Column("UserOwner").Length(20).Not.Nullable();
            Map(x => x.ProcessingStartDate).Column("ProcessingStartDate").Nullable().CustomType("Timestamp");
            Map(x => x.ProcessingFinishDate).Column("ProcessingFinishDate").Nullable().CustomType("Timestamp");
        }
    }
}
