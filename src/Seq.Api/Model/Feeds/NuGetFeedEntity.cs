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

namespace Seq.Api.Model.Feeds
{
    /// <summary>
    /// A NuGet feed.
    /// </summary>
    public class NuGetFeedEntity : Entity
    {
        /// <summary>
        /// The feed name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A URI or folder path at which the feed is located.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// If required, a username that will be sent when accessing the feed.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// When <see cref="Username"/> is non-empty, can be used to set an associated
        /// password.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
