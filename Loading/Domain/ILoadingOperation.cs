// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Depra.Loading.Domain
{
    public interface ILoadingOperation
    {
        OperationDescription Description { get; }

        Task<OperationResult> Load(Action<float> onProgress, CancellationToken cancellationToken);
    }
}