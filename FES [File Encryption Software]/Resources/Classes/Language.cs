using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FES__File_Encryption_Software_
{
    class Language
    {
        public string Name { get; }
        public string Code { get; }

        public Language(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public override string ToString() => Name;

        public static Language[] AvailableLanguages = new Language[]
        {
            new Language("English", "en-US"),
            new Language("Čeština", "cs-CZ")
        };
    }
}
