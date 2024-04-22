using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model.Alerting;
using Seq.Api.Model.Indexes;
using Seq.Api.Model.Indexing;
using Seq.Api.Model.Signals;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Statistics about indexes.
    /// </summary>
    public class IndexesResourceGroup : ApiResourceGroup
    {
        internal IndexesResourceGroup(ILoadResourceGroup connection)
            : base("Indexes", connection)
        {
        }
        /// <summary>
        /// Retrieve the index with the given id; throws if the entity does not exist.
        /// </summary>
        /// <param name="id">The id of the index.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The index.</returns>
        public async Task<SignalEntity> FindAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GroupGetAsync<SignalEntity>("Item", new Dictionary<string, object> { { "id", id } }, cancellationToken).ConfigureAwait(false);
        }


        /// <summary>
        /// Retrieve statistics on all indexes.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching expression indexes.</returns>
        public async Task<List<IndexEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<IndexEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }
        
        /// <summary>
        /// Suppress an index. Not all index types can be suppressed: signal indexes do support suppression, in the case
        /// of which the <see cref="SignalEntity.IsIndexSuppressed"/> flag will be set to <c langword="false"/>.
        /// Expression indexes can only be suppressed by deleting the associated <see cref="ExpressionIndexEntity"/>
        /// and alert indexes can only be suppressed by deleting the corresponding <see cref="AlertEntity"/>.
        /// </summary>
        /// <param name="entity">The index to suppress.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task SuppressAsync(IndexEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}
