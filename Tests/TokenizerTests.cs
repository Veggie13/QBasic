using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QBasic.Parsing;


namespace Tests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void Test1()
        {
            var x = Tokenizer.Tokenize(@"
x$ = ""Hello World""
print x$
").ToList();
        }
    }
}
