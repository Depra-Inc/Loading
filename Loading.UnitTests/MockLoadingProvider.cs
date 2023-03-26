// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Loading.Domain;

namespace Loading.UnitTests;

internal sealed class MockLoadingProvider : ILoadingProvider
{
    public async Task Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken)
    {
        while (operations.Count > 0)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var operation = operations.Dequeue();
            await operation.Load(null, cancellationToken);
        }
    }
}