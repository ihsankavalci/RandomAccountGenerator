using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class GitHubAccountFactory
    {
        public GitHubAcc CreateRandomGithubAccount()
        {
            var random = new RandomHelper();
            String uname = random.RandomStr();
            var g = new GitHubAcc
            {
                Username = uname,
                Id = random.RandomInt(),
                Email = uname + random.RandomEmail(),
                GitHubUrl = "http://github.com/" + uname

            };
            return g;
        }
    }
}
