// Copyright © Datalust and contributors. 
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

namespace Seq.Api.Model.Shared;

#nullable enable

/// <summary>
/// The lateral cross join part of a from clause.
/// </summary>
public class JoinPart
{
    /// <summary>
    /// The type of relational join.
    /// </summary>
    public JoinKind Kind { get; set; }
    
    /// <summary>
    /// The set function call used in the lateral join.
    /// </summary>
    public string? SetFunctionCall { get; set; }
        
    /// <summary>
    /// The alias of the set function call. 
    /// </summary>
    public string? Alias { get; set; }
}