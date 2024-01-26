// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Loading.UnitTests;

internal static class Asserts
{
    public async static Task CompletesAsync(Task task, int millisecondsTimeout = 5000)
    {
        var completedTask = await Task.WhenAny(task, Task.Delay(millisecondsTimeout));
        Assert.That(completedTask, Is.EqualTo(task));
    }
}