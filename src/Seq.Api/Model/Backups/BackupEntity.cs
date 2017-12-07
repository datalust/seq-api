using System;

namespace Seq.Api.Model.Backups
{
    public class BackupEntity : Entity
    {
        public string CreatedAt { get; set; }
        public string Filename { get; set; }
        public long FileSizeBytes { get; set; }
    }
}
