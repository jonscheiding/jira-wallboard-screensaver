using System;
using Jira.WallboardScreensaver.Services;
using NSubstitute;

namespace Jira.WallboardScreensaver.Tests {
    public static class TestHelper {
        public static IErrorMessageService LogErrors(IErrorMessageService service) {
            service
                .When(e => e.ShowErrorMessage(Arg.Any<object>(), Arg.Any<string>(), Arg.Any<string>()))
                .Do(c => {
                    Console.WriteLine("Error shown to user.");
                    Console.WriteLine($"\tTitle = '{c[2]}'");
                    Console.WriteLine($"\tMessage = '{c[1]}'");
                });

            return service;
        }
    }
}