﻿using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using YoCli.Commons;
using YoCli.Models;

namespace YoCli.Commands
{
    /// <summary>
    /// Represent the sub command export to the main command yo
    /// </summary>
    [Command(Name = "export", Description = "Export notes to Json file")]
    class Export : ICommand
    {
        private readonly IConsole _console;
        private readonly IContext<Note> _context;

        public Export(IConsole console, IContext<Note> context)
        {
            _console = console;
            _context = context;
        }

        [Argument(0, Description = "The directory where to save the file")]
        public string Path { get; }

        public Task<int> OnExecute()
        {
            if (!Directory.Exists(Path))
            {
                ConsoleMessage.PrintError(_console, "Invalid path!");
                return Task.FromResult(-1);
            }

            var json = JsonSerializer.Serialize(_context.Data);
            File.WriteAllText(System.IO.Path.Combine(Path, "notes.json"), json);

            ConsoleMessage.PrintSuccess(_console, "Notes exported");
            return Task.FromResult(1);
        }
    }
}
