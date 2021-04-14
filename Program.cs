/*
 * MIT License
 * Copyright (c) 2020 Kazuhiro Kato 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace mini_webserver
{
    public class Program
    {
        public static Options Options;

        public static void Main(string[] args)
        {
            var option = Parser.Default.ParseArguments<Options>(args)
                .WithNotParsed(HandleParseError)
                .WithParsed((option) =>
                {
                    Options = option;

                    if (option.DirName != null && System.IO.Directory.Exists(option.DirName))
                    {
                         Environment.CurrentDirectory = option.DirName;
                    }
                    // 処理実行
                    CreateHostBuilder(args).Build().Run();
                });
        }

        /// <summary>
        /// コマンドライン解析エラー
        /// </summary>
        /// <param name="errs"></param>
        private static void HandleParseError(IEnumerable<Error> errs)
        {
            if (errs.IsVersion())
            {
                Console.WriteLine("Version Request");
                return;
            }

            if (errs.IsHelp())
            {
                Console.WriteLine("Help Request");
                return;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
