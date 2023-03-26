// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Domain
{
    public interface ILoadingProvider
    {
        Task Load(Queue<ILoadingOperation> operations, CancellationToken cancellationToken);
    }
}