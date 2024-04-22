using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Seq.Api.Model;
using Seq.Api.Model.Indexing;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Perform operations on expression indexes.
    /// </summary>
    public class ExpressionIndexesResourceGroup: ApiResourceGroup
    {
        internal ExpressionIndexesResourceGroup(ILoadResourceGroup connection) : base("ExpressionIndexes", connection)
        {
        }
        
        /// <summary>
        /// Retrieve expression indexes.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A list containing matching expression indexes.</returns>
        public async Task<List<ExpressionIndexEntity>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await GroupListAsync<ExpressionIndexEntity>("Items", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Construct an expression index with server defaults pre-initialized.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The unsaved expression index.</returns>
        public async Task<ExpressionIndexEntity> TemplateAsync(CancellationToken cancellationToken = default)
        {
            return await GroupGetAsync<ExpressionIndexEntity>("Template", cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Add a new expression index.
        /// </summary>
        /// <param name="entity">The expression index to add.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>The expression index, with server-allocated properties such as <see cref="Entity.Id"/> initialized.</returns>
        public async Task<ExpressionIndexEntity> AddAsync(ExpressionIndexEntity entity, CancellationToken cancellationToken = default)
        {
            return await GroupCreateAsync<ExpressionIndexEntity, ExpressionIndexEntity>(entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Remove an existing expression index.
        /// </summary>
        /// <param name="entity">The expression index to remove.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> allowing the operation to be canceled.</param>
        /// <returns>A task indicating completion.</returns>
        public async Task RemoveAsync(ExpressionIndexEntity entity, CancellationToken cancellationToken = default)
        {
            await Client.DeleteAsync(entity, "Self", entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }
    }
}