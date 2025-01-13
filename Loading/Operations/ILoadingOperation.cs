// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;
using System.Threading;
using Depra.Threading;

namespace Depra.Loading
{
	public interface ILoadingOperation
	{
		OperationDescription Description { get; }

		ITask Load(IProgress<float> progress, CancellationToken token);
	}
}