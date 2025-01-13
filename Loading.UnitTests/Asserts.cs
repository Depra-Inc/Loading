// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Loading.UnitTests;

internal static class Asserts
{
    public static async Task CompletesAsync(Task task, int millisecondsTimeout = 5000)
    {
        var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsTimeout));
        Assert.That(completedTask, Is.EqualTo(task));
    }
}