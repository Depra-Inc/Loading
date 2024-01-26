// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System;

namespace Depra.Loading.Operations
{
	[Serializable]
	public readonly struct OperationDescription
	{
		private const string DEFAULT_FORMAT = "Loading {0}...";

		public static OperationDescription Default(string parameterName) =>
			new(string.Format(DEFAULT_FORMAT, parameterName));

		public OperationDescription(string text) => Text = text;

		public string Text { get; }
	}
}