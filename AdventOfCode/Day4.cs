using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal sealed class Day4 : IDay
    {
        public void Run()
        {
            var input = File.ReadAllText(@"input\day4.txt");
            var clean = Regex.Replace(input, @"(\w)\r\n(\w)", "$1 $2").Split(Environment.NewLine).Where(c => !string.IsNullOrWhiteSpace(c));


            var passports = new List<Passport>();

            var p = @"(?:(\w{3}):(\S*)){1,}";
            var pattern = new Regex(p);
            
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            foreach (var passportRaw in clean)
            {
                var data = pattern.Matches(passportRaw).ToDictionary(key => key.Groups[1].Value, value => value.Groups[2].Value);
                passports.Add(new Passport { Data = data });
            }

            Console.WriteLine(passports.Count(c => c.IsValid));
            timer.Stop();

            Console.WriteLine($"Time taken: {timer.Elapsed}");
        }

        private class Passport
        {
            public bool IsValid { get { return RulePartOne() && RulePartTwo(); } }

            public Dictionary<string, string> Data { get; set; }

            private bool RulePartOne()
            {
                return Data.ContainsKey("byr")
                    && Data.ContainsKey("iyr")
                    && Data.ContainsKey("eyr")
                    && Data.ContainsKey("hgt")
                    && Data.ContainsKey("hcl")
                    && Data.ContainsKey("ecl")
                    && Data.ContainsKey("pid");
            }

            private bool RulePartTwo()
            {
                var birth = int.Parse(Data["byr"]);
                var issue = int.Parse(Data["iyr"]);
                var expire = int.Parse(Data["eyr"]);
                var validHeight = false;
                
                if (Data["hgt"].EndsWith("cm"))
                {
                    var rawHeight = int.Parse(Data["hgt"].Replace("cm", string.Empty));
                    validHeight = rawHeight >= 150 && rawHeight <= 193;
                }
                else if (Data["hgt"].EndsWith("in"))
                {
                    var rawHeight = int.Parse(Data["hgt"].Replace("in", string.Empty));
                    validHeight = rawHeight >= 59 && rawHeight <= 76;
                }

                var hexMatch = @"#[\d|a-f]{6}";
                var validHair = Regex.IsMatch(Data["hcl"], hexMatch);

                var validEye = false;                
                switch (Data["ecl"])
                {
                    case "amb":
                    case "blu":
                    case "brn":
                    case "gry":
                    case "grn":
                    case "hzl":
                    case "oth":
                        validEye = true;
                        break;
                }

                var validPassportId = Data["pid"].Length == 9 && int.TryParse(Data["pid"], out _);

                return birth >= 1920 && birth <= 2002
                    && issue >= 2010 && issue <= 2020
                    && expire >= 2020 && expire <= 2030
                    && validHeight
                    && validHair
                    && validEye
                    && validPassportId;
            }
        }
    }
}
