﻿// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Depra.Loading.Operations;

namespace Depra.Loading.Curtain
{
	public interface ILoadingCurtain
	{
		Task Load(IEnumerable<ILoadingOperation> operations, CancellationToken cancellationToken = default);

		Task Unload(CancellationToken token = default);
	}

	public static class LoadingCurtainExtensions
	{
		public static Task Load(this ILoadingCurtain self, CancellationToken token = default,
			params ILoadingOperation[] operations) => self.Load(operations, token);
	}
}