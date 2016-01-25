using System;

namespace WindowsTime.IntegratedTest
{
    public class FileImport
    {
        public virtual int Id { get; set; }
        public virtual int StoreId { get; set; }
        public virtual int ImportType { get; set; }
        public virtual DateTime ImportDate { get; set; }
        public virtual char Status { get; set; }
        public virtual string ImportedFilePath { get; set; }
        public virtual string UserOwner { get; set; }
        public virtual DateTime? ProcessingStartDate { get; set; }
        public virtual DateTime? ProcessingFinishDate { get; set; }

        public virtual void PrepareToReprocessing()
        {
            ProcessingStartDate = null;
            ProcessingFinishDate = null;
        }
    }
}