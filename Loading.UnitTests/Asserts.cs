// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Loading.UnitTests;

internal static class Asserts
{
    public static async Task CompletesAsync(Task task, int millisecondsTimeout = 5000)
    {
        var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsTimeout));
        Assert.That(completedTask, Is.EqualTo(task));
    }
}