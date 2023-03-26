// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using Depra.Loading.Domain;

namespace Loading.UnitTests;

internal sealed class MockLoadingOperation : ILoadingOperation
{
    private readonly OperationResult _result;

    public MockLoadingOperation(OperationResult result) =>
        _result = result;

    public bool IsStarted { get; private set; }

    public bool IsCompleted => _result == OperationResult.Success;

    public OperationDescription Description => new("Mock loading operation");

    public async Task<OperationResult> Load(Action<float> onProgress, CancellationToken cancellationToken)
    {
        onProgress?.Invoke(0f);
        IsStarted = true;
        
        await Task.Delay(10, cancellationToken);
        
        onProgress?.Invoke(1f);
        
        return _result;
    }
}