// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Loading
{
	[Serializable]
	public readonly struct OperationDescription
	{
		public static OperationDescription Default(string parameterName) => new($"Loading {parameterName}...");

		public readonly string Text;

		public OperationDescription(string text) => Text = text;
	}
}