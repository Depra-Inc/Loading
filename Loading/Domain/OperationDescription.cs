// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;

namespace Depra.Loading.Domain
{
    [Serializable]
    public readonly struct OperationDescription
    {
        public readonly string Text;

        public OperationDescription(string text) => Text = text;
    }
}