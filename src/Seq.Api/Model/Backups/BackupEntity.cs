// Copyright 2014-2019 Datalust and contributors. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Seq.Api.Model.Backups
{
    /// <summary>
    /// Seq backups include metadata like users, signals, API keys and other configuration, but do not include
    /// the event stream. Backups are fully encrypted with AES-256 and cannot be restored without the master key
    /// from the originating Seq instance.
    /// </summary>
    public class BackupEntity : Entity
    {
        /// <summary>
        /// The time at which the backup was created (ISO-8601-encoded date/time).
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// The filename (without path information) of the backup, within the server's
        /// configured backup location.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// The size, in bytes, of the backup file.
        /// </summary>
        public long FileSizeBytes { get; set; }
    }
}
